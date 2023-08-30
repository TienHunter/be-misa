using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Account;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Common.Resources;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Core.Interface.Excels;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Core.Interface.Manager;

namespace MISA.WebFresher042023.Demo.Core.Services
{
    /// <summary>
    /// class thức thi business logic tài khoản
    /// </summary>
    /// created by: vdtien (17/7/2023)
    public class AccountService : BaseService<Account, AccountDTO, AccountCreatedDTO, AccountUpdateDTO>, IAccountService
    {
        #region Field
        private readonly IAccountRepository _accountRepository;
        private readonly IExcelInfra _excelInfra;
        private readonly IAccountManager _accountManager;
       
        #endregion

        #region Constructor
        public AccountService(IAccountRepository accountRepository, IExcelInfra excelInfra, IMapper mapper, IUnitOfWork uow, IAccountManager accountManager, IAccountingRepository accountingRepository) : base(accountRepository, mapper, uow)
        {
            _accountRepository = accountRepository;
            _excelInfra = excelInfra;
            _accountManager = accountManager;
          
        }
        #endregion


        #region Methods

        /// <summary>
        /// Lay danh sach tai khoan con theo danh sach tai khoan cha
        /// </summary>
        /// <param name="listParentId"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        public async Task<List<AccountDTO>> GetListAccountByListParentId(List<Guid> listParentId)
        {
            if (listParentId.Count == 0)
            {
                listParentId.Add(Guid.NewGuid());
            }


            string listIdStr = string.Join(",", listParentId.Select(id => $"'{id.ToString()}'"));

            var records = await _accountRepository.GetListAccountByListParentId(listIdStr);

            //if (records == null) return new List<AccountDTO>();
            var recordsDTO = _mapper.Map<List<AccountDTO>>(records);

            return recordsDTO;
        }

        /// <summary>
        /// lay danh sach tai khoan theo id tai khoan cha
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        public async Task<List<AccountDTO>> GetListAccountByParentId(Guid parentId)
        {
            var result = await _accountRepository.GetListAccountByParentId(parentId);
            var accountsDTO = _mapper.Map<List<AccountDTO>>(result);
            return accountsDTO;
        }

        /// <summary>
        /// them tai khoan
        /// </summary>
        /// <returns>them tai khoan</returns>
        /// Created by: vdtien(17/7/2023)
        public override async Task<AccountDTO?> InsertAsync(AccountCreatedDTO accountCreateDTO)
        {
            var account = _mapper.Map<Account>(accountCreateDTO);

            // check dup accountcode
            await _accountManager.IsDupAccountCodeAsync(account.AccountCode, null);

            // check số tài khoản con có length > 3 thì cần có tài khoản tổng hợp
            _accountManager.CheckNeedAccountParent(account.AccountCode, account.ParentId);


            // gán giá trị ban đầu cho tài khoản
            account.AccountId = Guid.NewGuid();
            account.Grade = 0;
            account.IsParent = 0;
            account.Status = Status.Using;
            account.KeyCode = account.AccountCode + "-";
            account.KeyRoot = account.AccountCode;
            account.NumberChilds = 0;
            account.CreatedBy = Guid.NewGuid();
            account.CreatedDate = DateTime.Now;

            if (account.ParentId != null)
            {
                var parentNode = await _accountRepository.GetAsync(account.ParentId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFound);

                // check số tài khoản con là tiền tố của tài khoản cha
                _accountManager.CheckAccountCodeIsPrefixAccountParentCode(account.AccountCode, parentNode.AccountCode);

                account.Status = parentNode.Status;
                account.Grade = parentNode.Grade + 1;
                account.KeyCode = parentNode.KeyCode + account.AccountCode + "-";
                account.KeyRoot = parentNode.KeyRoot;
                parentNode.IsParent = 1;
                parentNode.NumberChilds += 1;
                await _uow.BeginTransactionAsync();
                try
                {
                    // insert tai khoan
                    await _accountRepository.InsertAsync(account);
                    await _accountRepository.UpdateAsync(parentNode);
                    await _uow.CommitAsync();
                    var res = await _accountRepository.GetAsync(account.AccountId);
                    var accountDTO = _mapper.Map<AccountDTO>(res);
                    return accountDTO;
                }
                catch (Exception)
                {
                    await _uow.RollbackAsync();
                    throw;
                }
            }
            else
            {
                var res = await _accountRepository.InsertAsync(account);
                var accountDTO = _mapper.Map<AccountDTO>(res);

                return accountDTO;
            }

        }

        /// <summary>
        /// cap nhap tai khoan
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="accountUpdateDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="DupCodeException"></exception>
        /// <exception cref="Exception"></exception>
        /// created by: vdtien (17/7/2023)
        public override async Task<AccountDTO?> UpdateAsync(Guid recordId, AccountUpdateDTO accountUpdateDTO)
        {
            // id body != id route
            if (recordId != accountUpdateDTO.AccountId || recordId == Guid.Empty)
            {
                throw new ValidateException(ResourceVN.UserMsg_InValid);
            }

            // check exsit
            var accountExist = await _accountRepository.GetAsync(accountUpdateDTO.AccountId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundAccount);

            var account = _mapper.Map<Account>(accountUpdateDTO);

            // check dup accountcode
            await _accountManager.IsDupAccountCodeAsync(account.AccountCode, account.AccountId);

            // check số tài khoản con có length > 3 thì cần có tài khoản tổng hợp
            _accountManager.CheckNeedAccountParent(account.AccountCode, account.ParentId);

            // validate thay đổi số tài khoản

            // check số tài khoản liên quan đến hạch toán kinh tế
            await _accountManager.ValidateAccountCodeChangeAsync(accountExist.AccountCode, account.AccountCode, account.AccountId);

            // check không được thay đổi số tài khoản khi nó là tài khoản tổng hợp
            if (accountExist.IsParent == 1 && account.AccountCode != accountExist.AccountCode)
            {
                var errsMsgs = new List<string>
                    {
                        ResourceVN.UserMsg_NotUpdateAccountWhenParent
                    };

                var errsMore = new Dictionary<string, List<string>>
                    {
                        { "AccountCode", errsMsgs }
                    };

                throw new ValidateException(errsMsgs, errsMore);
            }
            account.Grade = accountExist.Grade;
            account.IsParent = accountExist.IsParent;
            account.NumberChilds = accountExist.NumberChilds;
            account.Status = accountExist.Status;
            account.KeyRoot = accountExist.KeyRoot;
            account.KeyCode = accountExist.KeyCode;
            account.CreatedDate = accountExist.CreatedDate;
            account.ModifiedDate = DateTime.Now;

            // parentId cũ  = parentId mới
            if (account.ParentId == accountExist.ParentId)
            {
                if (account.ParentId != null)
                {
                    var parentNode = await _accountRepository.GetAsync(account.ParentId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundAccountParent);

                    // check số tài khoản con là tiền tố của tài khoản cha
                    _accountManager.CheckAccountCodeIsPrefixAccountParentCode(account.AccountCode, parentNode.AccountCode);
                }
                await _accountRepository.UpdateAsync(account);
                var res = await _accountRepository.GetAsync(account.AccountId);

                return _mapper.Map<AccountDTO>(res);
            }

            // parent cũ khác parent mới
            // không thay đổi parent khi node hiện tại cũng là parent
            if(account.IsParent == 1)
            {
                var userMsg = new List<string>()
                {
                    ResourceVN.UserMsg_NotChangeParentWhenAccountIsParent
                };
                var errMore = new Dictionary<string, List<string>>()
                {
                    {"ParentId",userMsg }
                };
                throw new ValidateException(userMsg,errMore);
            }

            // parent mới khác null
            if (account.ParentId != null)
            {
                var parentNode = await _accountRepository.GetAsync(account.ParentId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundAccountParent);

                // check số tài khoản con là tiền tố của tài khoản cha
                _accountManager.CheckAccountCodeIsPrefixAccountParentCode(account.AccountCode, parentNode.AccountCode);

                // check các anh em co là tiền tố của nhau không

                // check trang thai su dung
                if (parentNode.Status == Status.StopUsing && account.Status == Status.Using)
                {


                    var userMsg = new List<string>()
                        {
                            ResourceVN.UserMsg_AccountUsingAndParentNotUsing
                        };
                    var errMore = new Dictionary<string, List<string>>()
                    {
                        {"ParentId",userMsg }
                    };
                    throw new ValidateException(userMsg, errMore);
                }

                parentNode.NumberChilds += 1;
                parentNode.IsParent = parentNode.NumberChilds > 0 ? 1 : 0;

                account.KeyRoot = parentNode.KeyRoot;
                account.KeyCode = $"{parentNode.KeyCode}{account.AccountCode}-";
                account.Grade = parentNode.Grade + 1;

                if (accountExist.ParentId != null)
                {
                    var parentNodeOld = await _accountRepository.GetAsync(accountExist.ParentId) ?? throw new Exception(ResourceVN.UserMsg_Exception);

                    parentNodeOld.NumberChilds -= 1;
                    parentNodeOld.IsParent = parentNodeOld.NumberChilds > 0 ? 1 : 0;
                    parentNodeOld.ModifiedDate = DateTime.Now;

                    // cập nhật 3 node
                    try
                    {
                        await _uow.BeginTransactionAsync();

                        await _accountRepository.UpdateAsync(account);
                        await _accountRepository.UpdateAsync(parentNode);
                        await _accountRepository.UpdateAsync(parentNodeOld);

                        await _uow.CommitAsync();
                        var res = await _accountRepository.GetAsync(account.AccountId);
                        return _mapper.Map<AccountDTO>(res);
                    }
                    catch (Exception)
                    {
                        await _uow.RollbackAsync();
                        throw;
                    }
                }

                // cap nhat 2 node vi parentOld la null
                try
                {
                    await _uow.BeginTransactionAsync();
                    await _accountRepository.UpdateAsync(account);
                    await _accountRepository.UpdateAsync(parentNode);
                    await _uow.CommitAsync();

                    var res = await _accountRepository.GetAsync(account.AccountId);
                    return _mapper.Map<AccountDTO>(res);
                }
                catch (Exception)
                {
                    await _uow.RollbackAsync();
                    throw;
                }
            }


            // parentId moi bang null -> parent id cu khac null
            var parentOld = await _accountRepository.GetAsync(accountExist.AccountId) ?? throw new Exception(ResourceVN.UserMsg_Exception);

            parentOld.NumberChilds -= 0;
            parentOld.IsParent = parentOld.NumberChilds > 0 ? 1 : 0;

            account.Grade = 0;
            account.KeyRoot = account.AccountCode;
            account.KeyCode = $"{account.AccountCode}-";

            try
            {
                await _uow.BeginTransactionAsync();
                await _accountRepository.UpdateAsync(parentOld);
                await _accountRepository.UpdateAsync(account);
                await _uow.CommitAsync();

                var res = await _accountRepository.GetAsync(account.AccountId);
                return _mapper.Map<AccountDTO>(res);
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }

        }



        /// <summary>
        /// xoa 1 tai khoan theo id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="DupCodeException"></exception>
        /// created by: vdtien (17/7/2023)
        public override async Task<int> DeleteAsync(Guid accountId)
        {
            // check tai khoan co phai la parent nodee hay khong
            var account = await _accountRepository.GetAsync(accountId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundAccount);

            // check tài khoản có là khóa ngoại của supplier hoặc payment không
            await _accountManager.ValidateDeleteAccountForeignKeyAsync(account.AccountId);

            // check tai khoan co la tai khoan tong hop khong
            if (account.IsParent == null || account.IsParent == 0)
            {

                if(account.ParentId != null)
                {

                    var parentNode = await _accountRepository.GetAsync(account.ParentId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundAccountParent);

                    parentNode.NumberChilds -= 1;
                    parentNode.IsParent = parentNode.NumberChilds > 0 ? 1 : 0;
                    try
                    {


                        await _uow.BeginTransactionAsync();

                        await _accountRepository.UpdateAsync(parentNode);
                        var res = await _accountRepository.DeleteAsync(accountId);

                        await _uow.CommitAsync();
                        return res;
                    }catch(Exception)
                    {
                        await _uow.RollbackAsync();
                        throw;
                    }
                }else
                {
                    var res = await _accountRepository.DeleteAsync(accountId);
                    return res;
                }

              
            }
            else
            {
                var errsMsgs = new List<string>()
                {
                    ResourceVN.UserMsg_DeleteAccountHasChildren
                };

                var errsMore = new Dictionary<string, List<string>>
                {
                    { "AccountCode", errsMsgs }
                };
                throw new ValidateException(errsMsgs, errsMore);
            }
        }

        /// <summary>
        /// lay danh sach ban ghi theo cay
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="keySearch"></param>
        /// <param name="isRoot"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        public async Task<ListRecords<AccountDTO>?> GetListTreeAsync(int pageSize, int pageNumber, string keySearch)
        {

            int linit = pageSize <= 0 ? 10 : pageSize;
            int offset = pageNumber <= 0 ? 0 : (pageNumber - 1) * linit;

            var results = await _accountRepository.GetListTreeAsync(linit, offset, keySearch);
            var records = results?.Data ?? new List<Account>();
            var recordsDTO = _mapper.Map<List<AccountDTO>>(records);
            var res = new ListRecords<AccountDTO>()
            {
                TotalRecord = results?.TotalRecord ?? 0,
                TotalRoot = results?.TotalRoot ?? 0,
                Data = recordsDTO ?? new List<AccountDTO>()
            };

            return res;
        }


        /// <summary>
        /// xuat excel
        /// </summary>
        /// <returns>byte[]</returns>
        /// created by: vdtien (17/7/2023)
        public async Task<byte[]> ExportAccountsToExcel(string keySearch)
        {
            // Lấy danh sách nhân viên từ database

            var accountEntity = await _accountRepository.GetListByFilterAsync(keySearch);
            var optionsRow = new List<OptionsExcel>();
            var optionsCol = new List<OptionsExcel>();
            var optionsCell = new List<OptionsExcel>();

            for (var i = 0; i < accountEntity.Count; i++)
            {

                if (accountEntity[i].Grade != null && accountEntity[i].AccountCode != null)

                {

                    int numberOfSpaces = accountEntity[i].Grade.Value * 4;
                    accountEntity[i].AccountCode = accountEntity[i].AccountCode.PadLeft(accountEntity[i].AccountCode.Length + numberOfSpaces, ' ');
                    if (accountEntity[i].IsParent != null && accountEntity[i].IsParent == 1)
                    {
                        var optionRow = new OptionsExcel
                        {
                            FontBold = true,
                            Row = i
                        };
                        optionsRow.Add(optionRow);
                    }


                }
            }
            var accounts = _mapper.Map<List<AccountExcelDTO>>(accountEntity);

            // Tạo DataTable với các cột tương ứng
            var data = new DataTable();

            // Lấy danh sách trường của đối tượng account
            var properties = typeof(AccountExcelDTO).GetProperties();

            // thêm các cột vào datatable dựa trên danh sách trường
            for (var i = 0; i < properties.Length; i++)
            {

                var displayNameAttribute = properties[i].GetCustomAttribute<DisplayAttribute>();
                var columnName = displayNameAttribute != null ? displayNameAttribute.GetName() : properties[i].Name;
                if (properties[i].GetCustomAttribute<IndexProperty>() != null)
                {
                    var optionColExcel = new OptionsExcel
                    {
                        Col = i + 1,
                        Horizontal = StyleAlignment.Center
                    };
                    optionsCol.Add(optionColExcel);
                }
                data.Columns.Add(columnName);
            }
            // Đổ dữ liệu từ danh sách nhân viên vào DataTable
            //var index = 1;
            for (var rowIndex = 0; rowIndex < accounts.Count; rowIndex++)
            {
                var row = data.NewRow();
                //row["STT"] = index;
                //index++;
                // Đặt giá trị của từng trường vào các cột tương ứng
                for (var colIndex = 0; colIndex < properties.Length; colIndex++)
                {
                    var displayNameAttribute = properties[colIndex].GetCustomAttribute<DisplayAttribute>();
                    var columnName = displayNameAttribute != null ? displayNameAttribute.GetName() : properties[colIndex].Name;
                    if (properties[colIndex].GetCustomAttribute<IndexProperty>() != null)
                    {
                        row[columnName] = rowIndex + 1;
                    }
                    else if (properties[colIndex].PropertyType == typeof(AccountFeature?))
                    {
                        var accountFeature = (AccountFeature?)properties[colIndex].GetValue(accounts[rowIndex]);
                        var processedGender = ConverAccountFeature(accountFeature);
                        row[columnName] = processedGender;
                    }
                    else if (properties[colIndex].PropertyType == typeof(Status?))
                    {
                        var status = (Status?)properties[colIndex].GetValue(accounts[rowIndex]);
                        var processedGender = ConverStatus(status);
                        row[columnName] = processedGender;
                    }
                    else
                    {
                        row[columnName] = properties[colIndex].GetValue(accounts[rowIndex]);
                    }

                }
                // thêm hàng vào dataTable
                data.Rows.Add(row);
            }


            // tao tile cho file
            var title = "Danh sách tài khoản";

            // Sử dụng _excelService để xuất dữ liệu ra file Excel
            var excelData = await _excelInfra.ExportToExcelAsync(data, title, optionsRow, optionsCol, null);

            return excelData;

        }

        /// <summary>
        /// convert tính chất tài khoản
        /// </summary>
        /// <param name="accountFeature"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        private static string ConverAccountFeature(AccountFeature? accountFeature)
        {
            switch (accountFeature)
            {
                case AccountFeature.Debt:
                    return "Dư nợ";
                case AccountFeature.Redundant:
                    return "Dư có";
                case AccountFeature.Combine:
                    return "Lưỡng tính";
                case AccountFeature.NoBalance:
                    return "Không có số dư";
                default:
                    return "";
            }
        }

        /// <summary>
        /// convert trạng thái
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        private static string ConverStatus(Status? status)
        {
            switch (status)
            {
                case Status.StopUsing:
                    return "Ngưng sử dụng";
                case Status.Using:
                    return "Đang sử dụng";

                default:
                    return "";
            }
        }

        /// <summary>
        /// cap nhat trang thai cho tai khoan
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        public async Task<int> UpdateStatusAsync(Guid accountId, Status status, TypeUpdate type)
        {
            // check ton tai
            var account = await _accountRepository.GetAsync(accountId);
            if (account == null)
            {
                throw new NotFoundException();
            }
            if (status == Status.Using)
            {
                if (account.ParentId != null)
                {
                    var parent = await _accountRepository.GetAsync(account.ParentId) ?? throw new Exception();
                    if (parent.Status == Status.StopUsing)
                    {
                        string errMsg = "Tài khoản cha đang ở trạng thái \"Ngừng sử dụng\". Bạn không thể thiết lập trạng thái \"Sử dụng\" cho Tài khoản con.";

                        var userMsg = new List<string>()
                        {
                            errMsg
                        };
                        //  "Tài khoản cha đang ở trạng thái "Ngừng sử dụng".Bạn không thể thiết lập trạng thái "Sử dụng" cho Tài khoản con."
                        throw new ValidateException(userMsg, null);
                    }
                }
                // type = 1 : update 1, type  = 2 update all

                var res = await _accountRepository.UpdateStatusAsync(account.KeyCode, status, type);
                return res;

            }
            else
            {

                var res = await _accountRepository.UpdateStatusAsync(account.KeyCode, Status.StopUsing, TypeUpdate.UpdateAll);
                return res;
            }
        }

        /// <summary>
        /// lấy danh sách tài khoản theo query
        /// </summary>
        /// <param name="accountFeatures"></param>
        /// <param name="userObjects"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        public async Task<List<AccountDTO>> GetAllQueryAsync(List<AccountFeature>? accountFeatures, List<UserObject>? userObjects)
        {
            var res = await _accountRepository.GetAllQueryAsync(accountFeatures, userObjects);
            var accounts = _mapper.Map<List<AccountDTO>>(res);
            return accounts.ToList();
        }

        #endregion
    }
}
