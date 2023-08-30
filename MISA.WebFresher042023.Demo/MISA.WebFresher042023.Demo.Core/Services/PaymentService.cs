using AutoMapper;
using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Acccounting;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using MISA.WebFresher042023.Demo.Common.DTO.Payments;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Common.Resources;
using MISA.WebFresher042023.Demo.Core.Interface.Excels;
using MISA.WebFresher042023.Demo.Core.Interface.Manager;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Services
{
    /// <summary>
    /// class thực thi payment service
    /// </summary>
    /// created by: vdtien (14/8/2023)
    public class PaymentService : BaseService<Payment, PaymentDTO, PaymentCreateDTO, PaymentUpdateDTO>, IPaymentService
    {
        #region Field
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentManager _paymentManager;
        private readonly IAccountingRepository _accountingRepository;
        private readonly IAccountingManager _accountingManager;
        private readonly IAccountRepository _accountRepository;
        private readonly ISupplierManager _supplierManager;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IExcelInfra _excelInfra;
        #endregion

        #region Constructor
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, IUnitOfWork uow, IPaymentManager paymentManager, IAccountingRepository accountingRepository, IAccountingManager accountingManager, IAccountRepository accountRepository, ISupplierManager supplierManager, ISupplierRepository supplierRepository, IExcelInfra excelInfra) : base(paymentRepository, mapper, uow)
        {
            _paymentRepository = paymentRepository;
            _paymentManager = paymentManager;
            _accountingRepository = accountingRepository;
            _accountingManager = accountingManager;
            _accountRepository = accountRepository;
            _supplierManager = supplierManager;
            _supplierRepository = supplierRepository;
            _excelInfra = excelInfra;
        }
        #endregion

        /// <summary>
        /// them moi 1 phieu chi
        /// </summary>
        /// <param name="paymentCreateDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (15/8/2023)
        public override async Task<PaymentDTO?> InsertAsync(PaymentCreateDTO paymentCreateDTO)
        {
            var payment = _mapper.Map<Payment>(paymentCreateDTO);
            payment.PaymentId = Guid.NewGuid();

            var isSupplierNullOrEmpt = false;
            if (payment.SupplierId == null)
            {
                isSupplierNullOrEmpt = true;
            }

            if (isSupplierNullOrEmpt == false)
                // validate supplier exist
                await _supplierManager.CheckSupplierExistByIdAsync(payment.SupplierId.Value);

            // validate trung so phieu chi
            await _paymentManager.IsDupPaymentCode(payment.PaymentCode, null);

            // validate ngày chứng từ <= ngày hạch toán
            //var accountingDate = payment.AccountingDate?.Date;
            //var paymentDate = payment.PaymentDate?.Date;
            _paymentManager.CheckAccountingDateLessPaymentDate(payment.AccountingDate?.Date, payment.PaymentDate?.Date);

            // validate null or empty của danh sách hạch toán
            _paymentManager.AccountingsNullOrEmpty(payment?.Accountings);

            var numberOrder = await _accountingRepository.GetMaxNumberOrderAccountingAsync();
            // validate null or empty tài khoản nợ và có của danh sách hạch toán
            numberOrder += 1;
            var accountings = payment.Accountings.Select(accounting =>
            {
                // Thực hiện ánh xạ và thay đổi các trường của accounting
                accounting.PaymentId = payment.PaymentId;
                accounting.AccountingId = Guid.NewGuid();
                accounting.CreatedDate = DateTime.Now;
                accounting.NumberOrder = numberOrder;
                numberOrder++;

                return accounting; // Trả về accounting sau khi đã thay đổi
            }).ToList();

            var listAccountId = new List<Guid>();
            foreach (var accounting in accountings)
            {
                listAccountId.Add(accounting.AccountDebtId);
                listAccountId.Add(accounting.AccountBalanceId);
            }
            listAccountId = listAccountId.Distinct().ToList();

            //check id tài khoản rỗng hoặc null
            _paymentManager.HasIdNullOrEmptyInListIds(listAccountId, ResourceVN.UserMsg_InValid);

            // danh sách dối tượng sử dụng
            var userObjects = new List<UserObject>()
            {
                UserObject.All,
                UserObject.Supplier
            };
            // check tai khoan ton tai hay khong
            var listAccountCode = await _accountRepository.GetListAccountCodeInIdsAndInUserObjects(listAccountId, userObjects);
            if (listAccountId.Count != listAccountCode.Count)
                throw new NotFoundException(ResourceVN.UserMsg_InValid);

            // validate ghi sổ

            var userMsgs = _paymentManager.ValidateWrittenPayment(isSupplierNullOrEmpt, listAccountCode);

            // them tong tien
            payment.TotalMoney = accountings.Sum(accounting => accounting?.Money ?? 0);

            if (userMsgs.Count > 0)
            {
                payment.PaymentStatus = PaymentStatus.UnWrited;
            }
            else
            {
                payment.PaymentStatus = PaymentStatus.Writted;
            }

            try
            {
                await _uow.BeginTransactionAsync();

                await _paymentRepository.InsertAsync(payment);
                await _accountingRepository.BulkInsertAsync(accountings);

                await _uow.CommitAsync();

                var res = await _paymentRepository.GetAsync(payment.PaymentId);

                var resDTO = _mapper.Map<PaymentDTO>(res);
                if (userMsgs.Count > 0)
                {
                    resDTO.ListUesrMsg = userMsgs;
                }
                return resDTO;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }

        }

        /// <summary>
        /// cập nhật phiếu chi theo id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="paymentUpdateDTO"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (16/8/2023)
        public override async Task<PaymentDTO?> UpdateAsync(Guid paymentId, PaymentUpdateDTO paymentUpdateDTO)
        {

            // id trên query khác id trên body
            if (paymentId != paymentUpdateDTO.PaymentId)
            {
                throw new ValidateException(ResourceVN.UserMsg_InValid);
            }

            // check phiếu chi tồn tại theo id
            await _paymentManager.CheckPaymentExistByIdAsync(paymentId);

            var payment = _mapper.Map<Payment>(paymentUpdateDTO);

            // validate trung so phieu chi
            await _paymentManager.IsDupPaymentCode(payment.PaymentCode, payment.PaymentId);

            // kiểm tra supplierId có null hoặc rỗng hay không
            var isSupplierNullOrEmpt = false;
            if (payment.SupplierId == null)
            {
                isSupplierNullOrEmpt = true;
            }

            if (isSupplierNullOrEmpt == false)
                // validate supplier exist
                await _supplierManager.CheckSupplierExistByIdAsync(payment.SupplierId.Value);

            // validate ngày chứng từ <= ngày hạch toán
            _paymentManager.CheckAccountingDateLessPaymentDate(payment?.AccountingDate?.Date, payment?.PaymentDate?.Date);

            // tách hạch toán kinh tế thành các danh sách con theo  (StatusAction)
            // Tách danh sách ban đầu thành các danh sách con theo giá trị của trường StatusAction
            var groupedEntities = paymentUpdateDTO.Accountings.GroupBy(e => e.StatusAction)
                                          .ToDictionary(g => g.Key, g => g.ToList());

            // Truy cập danh sách con thông qua giá trị của trường StatusAction
            var listAccountingDelete = groupedEntities.GetValueOrDefault(StatusAction.Delete, new List<AccountingUpdateDTO>());
            var listAccountingNoChange = groupedEntities.GetValueOrDefault(StatusAction.NoChange, new List<AccountingUpdateDTO>());
            var listAccountingCreate = groupedEntities.GetValueOrDefault(StatusAction.Create, new List<AccountingUpdateDTO>());
            var listAccountingEdit = groupedEntities.GetValueOrDefault(StatusAction.Edit, new List<AccountingUpdateDTO>());

            // lấy danh sách id hạch toán từ danh sách xóa, sửa, không thay đổi
            var listAccountingId = paymentUpdateDTO.Accountings.Where(e => e.StatusAction != StatusAction.Create).Select(e => e.AccountingId).ToList();

            // kiểm tra danh sách xóa, không thay đổi, sửa có id rỗng không
            _paymentManager.HasIdNullOrEmptyInListIds(listAccountingId, ResourceVN.UserMsg_InValdAccounting);

            // lấy danh sách tài khoản từ hạch toán kinh tế
            var listAccountDebtId = paymentUpdateDTO.Accountings.Select(e => e.AccountDebtId).ToList();
            var listAccountBalanceId = paymentUpdateDTO.Accountings.Select(e => e.AccountBalanceId).ToList();
            var listAccountId = listAccountDebtId.Concat(listAccountBalanceId).Distinct().ToList();

            // kiểm tra danh sách tài khoản có null hoặc rỗng không
            _paymentManager.HasIdNullOrEmptyInListIds(listAccountId, ResourceVN.UserMsg_InValdAccounting);

            // check exist tồn tại hạch toán từ listAccountingId
            var listCheckAccounting = await _accountingRepository.GetListByListIdAsync(listAccountingId);
            if (listAccountingId.Count != listCheckAccounting.Count)
            {
                throw new NotFoundException(ResourceVN.Usermsg_NotFoundAccounting);
            }
            // danh sách dối tượng sử dụng
            var userObjects = new List<UserObject>()
            {
                UserObject.All,
                UserObject.Supplier
            };
            // check exsit account từ listAccountId
            var listAccountCode = await _accountRepository.GetListAccountCodeInIdsAndInUserObjects(listAccountId, userObjects);
            if (listAccountCode.Count != listAccountId.Count)
            {
                throw new NotFoundException(ResourceVN.UserMsg_NotFoundAccount);
            }

            var accountingListNoDelete = paymentUpdateDTO.Accountings.Where(accounting => accounting.StatusAction != StatusAction.Delete);
            var listAccountDebtIdValidate = accountingListNoDelete.Select(a => a.AccountDebtId);
            var listAccountBalanceIdValidate = accountingListNoDelete.Select(a => a.AccountBalanceId);
            var listAccountIdValidate = listAccountDebtIdValidate.Concat(listAccountBalanceIdValidate).Distinct().ToList();

            var listAccountCodeValidate = await _accountRepository.GetListAccountCodeInIdsAndInUserObjects(listAccountIdValidate, userObjects);

            var userMsgs = _paymentManager.ValidateWrittenPayment(isSupplierNullOrEmpt, listAccountCodeValidate);


            if (userMsgs.Count > 0)
            {
                payment.PaymentStatus = PaymentStatus.UnWrited;
            }
            else
            {
                payment.PaymentStatus = PaymentStatus.Writted;
            }
            // danh sách id hạch toán xóa
            var listAccountingIdDelete = listAccountingDelete.Select(e => e.AccountingId).ToList();

            var numberOrder = await _accountingRepository.GetMaxNumberOrderAccountingAsync();
            numberOrder++;


            // thêm paymentId và id cho list create
            var listAccountingCreateEntity = listAccountingCreate.Select(acc =>
                    {

                        var accounting = _mapper.Map<Accounting>(acc);
                        // Thực hiện ánh xạ và thay đổi các trường của accounting
                        accounting.PaymentId = payment.PaymentId;
                        accounting.AccountingId = Guid.NewGuid();
                        accounting.CreatedDate = DateTime.Now;
                        accounting.NumberOrder = numberOrder;
                        numberOrder++;
                        return accounting; // Trả về accounting sau khi đã thay đổi
                    }).ToList();


            var listAccountingEditEntity = listAccountingEdit.Select(item =>
            {
                var accounting = _mapper.Map<Accounting>(item);
                accounting.PaymentId = payment.PaymentId;
                accounting.ModifiedDate = DateTime.Now;

                return accounting;
            }).ToList();


            try
            {
                await _uow.BeginTransactionAsync();

                await _paymentRepository.UpdateAsync(payment);
                if (listAccountingCreateEntity.Count > 0)
                {
                    await _accountingRepository.BulkInsertAsync(listAccountingCreateEntity);
                }
                if (listAccountingIdDelete.Count > 0)
                {
                    await _accountingRepository.DeleteMultiAsync(listAccountingIdDelete);
                }
                if (listAccountingEditEntity.Count > 0)
                {
                    await _accountingRepository.BulkUpdateAsync(listAccountingEditEntity);
                }

                await _uow.CommitAsync();


                var res = await _paymentRepository.GetAsync(payment.PaymentId);
                var resDTO = _mapper.Map<PaymentDTO>(res);
                if (userMsgs.Count > 0)
                {
                    resDTO.ListUesrMsg = userMsgs;
                }
                return resDTO;
            }
            catch (Exception)
            {
                await _uow.RollbackAsync();
                throw;
            }

        }

        /// <summary>
        /// cập nhật trạng thái của phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// created by: vdtien (16/8/2023)
        public async Task<PaymentDTO> UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus)
        {
            // truyền sai dữ liệu
            if (paymentId == Guid.Empty) throw new ValidateException(ResourceVN.UserMsg_InValid);

            // check tồn tại phiếu chi
            var paymentExist = await _paymentRepository.GetAsync(paymentId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundPayment);

            var payment = _mapper.Map<PaymentDTO>(paymentExist);

            var isSupplierNullOrEmpty = false;
            if (payment.SupplierId == null || payment.SupplierId == Guid.Empty)
            {
                isSupplierNullOrEmpty = true;
            }

            // cập nhật phiếu chi thành không ghi sổ
            if (paymentStatus == PaymentStatus.UnWrited)
            {
                await _paymentRepository.UpdatePaymentStatusAsync(paymentId, paymentStatus);
                payment.PaymentStatus = paymentStatus;
                return payment;
            }

            // cập nhật phiếu chi thành ghi sổ
            else if (paymentStatus == PaymentStatus.Writted)
            {
                var accountings = paymentExist.Accountings;
                var listAccountCode = new List<string>();
                foreach (var accounting in accountings)
                {
                    listAccountCode.Add(accounting.AccountDebtCode);
                    listAccountCode.Add(accounting.AccountBalanceCode);
                }
                listAccountCode = listAccountCode.Distinct().ToList();

                var userMsg = _paymentManager.ValidateWrittenPayment(isSupplierNullOrEmpty, listAccountCode);

                if (userMsg.Count > 0)
                {
                    payment.ListUesrMsg = userMsg;

                    return payment;
                }
                else
                {
                    await _paymentRepository.UpdatePaymentStatusAsync(paymentId, paymentStatus);

                    payment.PaymentStatus = paymentStatus;
                    return payment;
                }
            }

            throw new ValidateException(ResourceVN.UserMsg_InValid);

        }

        /// <summary>
        /// xoa 1 phieu chi theo id
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ValidateException"></exception>
        /// <exception cref="Exception"></exception>
        /// created by: vdtien (15/8/2023)
        public override async Task<int> DeleteAsync(Guid recordId)
        {
            var paymentExsit = await _paymentManager.CheckPaymentExistByIdAsync(recordId);

            if (paymentExsit.PaymentStatus == PaymentStatus.Writted)
            {
                var userMsg = new List<string>()
                {
                   ResourceVN.UserMsg_PaymentWrittenDontDelete
                };
                throw new BadRequestException(userMsg, null);
            }

            try
            {
                await _uow.BeginTransactionAsync();

                await _accountingRepository.DeleteAccoutingByPaymentIdAsync(paymentExsit.PaymentId);
                var result = await _paymentRepository.DeleteAsync(paymentExsit.PaymentId);

                await _uow.CommitAsync();

                return result;
            }
            catch (Exception)
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa nhiều phiếu chi theo danh sách id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (16/8/2023)
        public async Task<ResultBulkHandleRecord<PaymentDTO>> BulkDeletePaymentAsync(List<Guid> listPaymentId)
        {
            // check guid empty
            _paymentManager.HasIdNullOrEmptyInListIds(listPaymentId, ResourceVN.UserMsg_InValid);

            // check tồn tại listPaymentId
            var listPayment = await _paymentRepository.GetListByListIdAsync(listPaymentId);
            if (listPaymentId.Count != listPayment.Count)
            {
                throw new NotFoundException(ResourceVN.UserMsg_NotFound);
            }

            var listPaymentWritten = new List<Payment>();
            var listPaymentUnWrittenId = new List<Guid>();
            var listAccountingId = new List<Guid>();

            foreach (var payment in listPayment)
            {
                if (payment.PaymentStatus == PaymentStatus.UnWrited)
                {
                    listPaymentUnWrittenId.Add(payment.PaymentId);
                    foreach (var accounting in payment.Accountings)
                    {
                        listAccountingId.Add(accounting.AccountingId);
                    }
                }
                else
                {
                    listPaymentWritten.Add(payment);

                }
            }

            var listPaymentWrittenDTO = _mapper.Map<List<PaymentDTO>>(listPaymentWritten);
            listPaymentWrittenDTO = listPaymentWrittenDTO.Select(e =>
            {
                e.ListUesrMsg = new List<string>{
                   ResourceVN.UserMsg_PaymentWritten
                };
                return e;
            }).ToList();

            if (listPaymentUnWrittenId.Count > 0)
            {
                try
                {
                    await _uow.BeginTransactionAsync();

                    await _accountingRepository.DeleteMultiAsync(listAccountingId);
                    await _paymentRepository.DeleteMultiAsync(listPaymentUnWrittenId);

                    await _uow.CommitAsync();
                    return new ResultBulkHandleRecord<PaymentDTO>()
                    {
                        Total = listPaymentId.Count,
                        Success = listPaymentUnWrittenId.Count,
                        Failure = listPaymentId.Count - listPaymentUnWrittenId.Count,
                        ListRecordFailure = listPaymentWrittenDTO

                    };

                }
                catch (Exception)
                {
                    await _uow.RollbackAsync();
                    throw;
                }
            }
            else
            {
                return new ResultBulkHandleRecord<PaymentDTO>()
                {
                    Total = listPaymentId.Count,
                    Success = listPaymentUnWrittenId.Count,
                    Failure = listPaymentId.Count - listPaymentUnWrittenId.Count,
                    ListRecordFailure = listPaymentWrittenDTO

                };
            }
        }

        /// <summary>
        /// cập nhật trạng thái phiếu chi hàng loạt
        /// </summary>
        /// <param name="listPaymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (16/8/2023)
        public async Task<ResultBulkHandleRecord<PaymentDTO>> BulkUpdatePaymentStatusAsync(List<Guid> listPaymentId, PaymentStatus paymentStatus)
        {
            // check guid empty
            _paymentManager.HasIdNullOrEmptyInListIds(listPaymentId, ResourceVN.UserMsg_InValid);
            var hasDuplicates = listPaymentId.Count != listPaymentId.Distinct().Count();
            if (hasDuplicates) throw new ValidateException(ResourceVN.UserMsg_InValid);


            // check tồn tại listPaymentId
            var listPayment = await _paymentRepository.GetListByListIdAsync(listPaymentId);
            if (listPaymentId.Count != listPayment.Count)
            {
                throw new NotFoundException(ResourceVN.UserMsg_NotFound);
            }

            if (paymentStatus == PaymentStatus.UnWrited)
            {
                var listPaymentIdWritten = new List<Guid>();
                foreach (var item in listPayment)
                {
                    if (item.PaymentStatus == PaymentStatus.Writted)
                    {
                        listPaymentIdWritten.Add(item.PaymentId);
                    }
                }

                try
                {
                    await _uow.BeginTransactionAsync();

                    await _paymentRepository.BulkUpdatePaymentStatusAsync(listPaymentIdWritten, paymentStatus);
                    await _uow.CommitAsync();

                    return new ResultBulkHandleRecord<PaymentDTO>()
                    {
                        Total = listPaymentId.Count,
                        Success = listPayment.Count,
                        Failure = 0,
                    };
                }
                catch (Exception)
                {
                    await _uow.RollbackAsync();
                    throw;
                }
            }
            else if (paymentStatus == PaymentStatus.Writted)
            {
                var listPaymentUnWrriten = new List<Payment>();
                var listPaymentIdWrite = new List<Guid>();
                var listPaymentIdWritten = new List<Guid>();

                foreach (var item in listPayment)
                {
                    // phiếu chi có chưa ghi sổ
                    if (item.PaymentStatus == PaymentStatus.UnWrited)
                    {
                        // id nhà cung cấp null ỏ empty
                        if (item.SupplierId == null || item.SupplierId == Guid.Empty)
                        {
                            listPaymentUnWrriten.Add(item);
                        }
                        // thỏa mãn để ghi sổ
                        else
                        {
                            listPaymentIdWrite.Add(item.PaymentId);
                        }

                    }
                    else if (item.PaymentStatus == PaymentStatus.Writted)
                    {
                        listPaymentIdWritten.Add(item.PaymentId);
                    }
                }

                // phiếu chi không thể ghi sổ do có lỗi
                var listPaymentUnWrritenDTO = _mapper.Map<List<PaymentDTO>>(listPaymentUnWrriten);
                if (listPaymentUnWrritenDTO.Count > 0)
                {

                    foreach (var item in listPaymentUnWrritenDTO)
                    {
                        var listAccountCode = new List<string>();
                        var userMsgs = new List<string>();
                        foreach (var accounting in item.Accountings)
                        {
                            listAccountCode.Add(accounting.AccountDebtCode);
                            listAccountCode.Add(accounting.AccountBalanceCode);
                        }
                        listAccountCode = listAccountCode.Distinct().ToList();
                        foreach (var accountCode in listAccountCode)
                        {
                            userMsgs.Add(String.Format(ResourceVN.UserMsg_AccountSupplierAndPaymentLackSupplier, accountCode));
                        }
                        item.ListUesrMsg = userMsgs;
                    }
                }
                // có danh sách id ghi sổ thỏa mãn
                if (listPaymentIdWrite.Count > 0)
                {
                    try
                    {
                        await _uow.BeginTransactionAsync();
                        await _paymentRepository.BulkUpdatePaymentStatusAsync(listPaymentIdWrite, paymentStatus);
                        await _uow.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await _uow.RollbackAsync();
                        throw;
                    }
                }

                return new ResultBulkHandleRecord<PaymentDTO>()
                {
                    Total = listPaymentId.Count,
                    Success = listPaymentIdWritten.Count + listPaymentIdWrite.Count,
                    Failure = listPaymentId.Count - (listPaymentIdWritten.Count + listPaymentIdWrite.Count),
                    ListRecordFailure = listPaymentUnWrritenDTO
                };

            }
            else
            {
                throw new ValidateException(ResourceVN.UserMsg_InValid);
            }
        }

        /// <summary>
        /// xuất excel danh sách phiếu chi theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: vdtien (20/8/2023)
        public async Task<byte[]> ExportPaymentsToExcel(string keySearch)
        {
            // Lấy danh sách nhân viên từ database

            var payments = await _paymentRepository.GetAllPaymentExcelAsync(keySearch);
            var optionsRow = new List<OptionsExcel>();
            var optionsCol = new List<OptionsExcel>();
            var optionsCell = new List<OptionsExcel>();


            // Tạo DataTable với các cột tương ứng
            var data = new DataTable();

            // Lấy danh sách trường của đối tượng account
            var properties = typeof(PaymentExcelDTO).GetProperties();

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
                else if (properties[i].PropertyType == typeof(DateTime?) || properties[i].PropertyType == typeof(DateTime))
                {
                    var optionColExcel = new OptionsExcel
                    {
                        Col = i + 1,
                        Horizontal = StyleAlignment.Center
                    };
                    optionsCol.Add(optionColExcel);

                }
                else if (properties[i].PropertyType == typeof(decimal?) || properties[i].PropertyType == typeof(decimal))
                {
                    var optionColExcel = new OptionsExcel
                    {
                        Col = i + 1,
                        Horizontal = StyleAlignment.Right
                    };
                    optionsCol.Add(optionColExcel);

                }
                data.Columns.Add(columnName);
            }
            // Đổ dữ liệu từ danh sách nhân viên vào DataTable
            decimal totalMoney = 0;
            for (var rowIndex = 0; rowIndex < payments.Count; rowIndex++)
            {
                var row = data.NewRow();
                payments[rowIndex].PaymentType = ResourceVN.Payment;


                // Đặt giá trị của từng trường vào các cột tương ứng
                for (var colIndex = 0; colIndex < properties.Length; colIndex++)
                {
                    var displayNameAttribute = properties[colIndex].GetCustomAttribute<DisplayAttribute>();
                    var columnName = displayNameAttribute != null ? displayNameAttribute.GetName() : properties[colIndex].Name;
                    if (properties[colIndex].GetCustomAttribute<IndexProperty>() != null)
                    {
                        row[columnName] = rowIndex + 1;
                    }
                    else if (properties[colIndex].PropertyType == typeof(DateTime?) || properties[colIndex].PropertyType == typeof(DateTime))
                    {
                        var dateTimeValue = (DateTime?)properties[colIndex].GetValue(payments[rowIndex]);
                        row[columnName] = Helper.ConvertDateTimeToDDMMYYYY(dateTimeValue);
                    }
                    else if (properties[colIndex].PropertyType == typeof(decimal?) || properties[colIndex].PropertyType == typeof(decimal))
                    {
                        var num = (decimal?)properties[colIndex].GetValue(payments[rowIndex]);
                        totalMoney += num ?? 0;

                        var processedGender = Helper.FormatDecimal(num);
                        row[columnName] = processedGender;
                        if (num < 0)
                        {
                            var optionCell = new OptionsExcel()
                            {
                                Row = rowIndex,
                                Col = colIndex + 1,
                                ColorHex = "#FF0000"
                            };
                            optionsCell.Add(optionCell);
                        }
                    }
                    else if (properties[colIndex].PropertyType == typeof(ReasonType))
                    {
                        var value = (ReasonType)properties[colIndex].GetValue(payments[rowIndex]);
                        row[columnName] = Helper.ConvertReasonType(value);
                    }
                    else
                    {
                        row[columnName] = properties[colIndex].GetValue(payments[rowIndex]);
                    }

                }
                // thêm hàng vào dataTable
                data.Rows.Add(row);
            }
            var rowEnd = data.NewRow();
            for (var colIndex = 0; colIndex < properties.Length; colIndex++)
            {
                if (properties[colIndex].Name == "AccountingDate")
                {
                    var displayNameAttribute = properties[colIndex].GetCustomAttribute<DisplayAttribute>();
                    var columnName = displayNameAttribute != null ? displayNameAttribute.GetName() : properties[colIndex].Name;
                    rowEnd[columnName] = ResourceVN.Sum;
                }
                else if (properties[colIndex].Name == "TotalMoney")
                {
                    var displayNameAttribute = properties[colIndex].GetCustomAttribute<DisplayAttribute>();
                    var columnName = displayNameAttribute != null ? displayNameAttribute.GetName() : properties[colIndex].Name;
                    var sumStr = Helper.FormatDecimal(totalMoney);
                    rowEnd[columnName] = sumStr;
                    if (totalMoney < 0)
                    {
                        optionsCell.Add(new OptionsExcel()
                        {
                            Row = payments.Count,
                            Col = colIndex + 1,
                            ColorHex = "#FF0000"

                        });
                    }
                }

            }
            data.Rows.Add(rowEnd);
            optionsRow.Add(new OptionsExcel()
            {
                Row = payments.Count,
                FontBold = true,
                BackgroundColorHex = "#FFD3D3D3"
            });
            // tao tile cho file
            var title = "Danh sách phiếu chi";

            // Sử dụng _excelService để xuất dữ liệu ra file Excel
            var excelData = await _excelInfra.ExportToExcelAsync(data, title, optionsRow, optionsCol, optionsCell);

            return excelData;

        }

    }
}
