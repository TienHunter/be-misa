using AutoMapper;
using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Common.DTO.Supplier;
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
    /// class SupplierService
    /// </summary>
    /// created by: vdtien (25/7/2023)
    public class SupplierService : BaseService<Supplier, SupplierDTO, SupplierCreateDTO, SupplierUpdateDTO>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IGroupSupplierRepository _groundSupplierRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IExcelInfra _excelInfra;
        private readonly ISupplier_GroupSupplierRepository _supplier_GroupSupplierRepository;
        private readonly ISupplierManager _supplierManager;
        private readonly IEmployeeManger _employeeManger;
        private readonly ITermPaymentManager _termPaymentManager;
        private readonly IPaymentRepository _paymentRepository;

        public SupplierService(ISupplierRepository supplierRepository, IGroupSupplierRepository groundSupplierRepository, IEmployeeRepository employeeRepository, IExcelInfra excelInfra, ISupplier_GroupSupplierRepository supplier_GroupSupplierRepository, IUnitOfWork uow, IMapper mapper, ISupplierManager supplierManager, IEmployeeManger employeeManger, ITermPaymentManager termPaymentManager, IPaymentRepository paymentRepository) : base(supplierRepository, mapper, uow)
        {
            _supplierRepository = supplierRepository;
            _groundSupplierRepository = groundSupplierRepository;
            _employeeRepository = employeeRepository;
            _excelInfra = excelInfra;
            _supplier_GroupSupplierRepository = supplier_GroupSupplierRepository;
            _supplierManager = supplierManager;
            _employeeManger = employeeManger;
            _termPaymentManager = termPaymentManager;
            _paymentRepository = paymentRepository;
        }



        /// <summary>
        /// them moi 1 nhan cung cap
        /// </summary>
        /// <param name="supplierCreateDTO"></param>
        /// <returns>nha cung cap duoc them</returns>
        /// <exception cref="DupCodeException"></exception>
        /// <exception cref="ValidateException"></exception>
        /// <exception cref="Exception"></exception>
        /// created by: vdtien (26/7/2023)
        public override async Task<SupplierDTO?> InsertAsync(SupplierCreateDTO supplierCreateDTO)
        {
            // check trung code
            await _supplierManager.IsDupSupplierCodeAsync(supplierCreateDTO.SupplierCode, null);

            // check danh sach nhom nha cung cap 
            await _supplierManager.CheckListGroupSupplierExistByListIdAsync(supplierCreateDTO.GroupSuppliersId);

            // check trùng số tài khoản
            _supplierManager.CheckDuplicateBankAccountNumber(supplierCreateDTO.BanksAccount);

            // check ton tai nhan vien mua hang
            if (supplierCreateDTO.EmployeeId != null)
            {
                await _employeeManger.CheckEmployeeExsitByIdAsync(supplierCreateDTO.EmployeeId.Value);

            }

            // check dieu khoan thanh toan
            if (supplierCreateDTO.TermPaymentId != null)
            {
                await _termPaymentManager.CheckTermPaymentExsitByIdAsync(supplierCreateDTO.TermPaymentId.Value);
            }

            // check tài khoản trả

            await _supplierManager.CheckAccountSupplierExistByAccountIdAsync(supplierCreateDTO.AccountPayableId, ResourceVN.UserMsg_NotFoundAccountPayable);


            // check tài khoản thu nếu là khách hàng
            if (supplierCreateDTO.IsCustomer == 1)
            {
                await _supplierManager.CheckAccountSupplierExistByAccountIdAsync(supplierCreateDTO.AccountReceivableId, ResourceVN.UserMsg_NotFoundAccountReceivable);
            }
            else
            {
                supplierCreateDTO.AccountReceivableId = null;
            }

            // insert
            var supplierIns = _mapper.Map<Supplier>(supplierCreateDTO);
            supplierIns.Status = 1;
            supplierIns.SupplierId = Guid.NewGuid();

            var supplier_GroupSuppliers = new List<Supplier_GroupSupplier>();

            foreach (var groupSupplierId in supplierCreateDTO.GroupSuppliersId)
            {
                var newSupplier_GroupSupplier = new Supplier_GroupSupplier()
                {
                    SupplierId = supplierIns.SupplierId,
                    GroupSupplierId = groupSupplierId
                };
                supplier_GroupSuppliers.Add(newSupplier_GroupSupplier);
            }
            try
            {
                await _uow.BeginTransactionAsync();

                await _supplierRepository.InsertAsync(supplierIns);
                if (supplier_GroupSuppliers.Count > 0)
                {
                    await _supplier_GroupSupplierRepository.BulkInsertAsync(supplier_GroupSuppliers);
                }

                await _uow.CommitAsync();

                var res = await _supplierRepository.GetAsync(supplierIns.SupplierId);
                var supplier = _mapper.Map<SupplierDTO>(res);

                return supplier;
            }
            catch (Exception)
            {
                await _uow.RollbackAsync();
                throw;
            }
        }


        /// <summary>
        /// cập nhật 1 nhan cung cap
        /// </summary>
        /// <param name="supplierUpdateDTO"></param>
        /// <returns>nha cung cap duoc them</returns>
        /// <exception cref="DupCodeException"></exception>
        /// <exception cref="ValidateException"></exception>
        /// <exception cref="Exception"></exception>
        /// created by: vdtien (26/7/2023)
        public override async Task<SupplierDTO?> UpdateAsync(Guid supplierId, SupplierUpdateDTO supplierUpdateDTO)
        {
            // check id route != id body
            if (supplierId != supplierUpdateDTO.SupplierId)
            {
                throw new ValidateException(ResourceVN.UserMsg_InValid);
            }

            // check ton tai
            var supplierExsit = await _supplierManager.CheckSupplierExistByIdAsync(supplierId);

            // check trùng số tài khoản
            _supplierManager.CheckDuplicateBankAccountNumber(supplierUpdateDTO.BanksAccount);

            // check hạch toán kinh tế khi thay đổi mã nhà cung cấp
            if (supplierExsit.SupplierCode != supplierUpdateDTO.SupplierCode)
            {
                // check nhà cung cấp phát sinh chứng từ thì không thể sửa mã nhà cung cấp
                var isPaymented = await _supplierManager.CheckSupplierHasPaymentAsync(supplierId);

                if (isPaymented)
                {
                    var userMsg = new List<string>(){
                    ResourceVN.UserMsg_CantChangeSupplierCode
                };
                    var errMore = new Dictionary<string, List<string>>()
                {
                    {"SupplierCode",userMsg }
                };
                    throw new BadRequestException(userMsg, errMore);
                }
            }


            // check trung code
            await _supplierManager.IsDupSupplierCodeAsync(supplierUpdateDTO.SupplierCode, supplierUpdateDTO.SupplierId);

            // check danh sach nhom nha cung cap 
            await _supplierManager.CheckListGroupSupplierExistByListIdAsync(supplierUpdateDTO.GroupSuppliersId);

            // check ton tai nhan vien mua hang
            if (supplierUpdateDTO.EmployeeId != null)
            {
                await _employeeManger.CheckEmployeeExsitByIdAsync(supplierUpdateDTO.EmployeeId.Value);

            }

            // check dieu khoan thanh toan
            if (supplierUpdateDTO.TermPaymentId != null)
            {
                await _termPaymentManager.CheckTermPaymentExsitByIdAsync(supplierUpdateDTO.TermPaymentId.Value);
            }

            // check tài khoản trả

            await _supplierManager.CheckAccountSupplierExistByAccountIdAsync(supplierUpdateDTO.AccountPayableId, ResourceVN.UserMsg_NotFoundAccountPayable);


            // check tài khoản thu nếu là khách hàng
            if (supplierUpdateDTO.IsCustomer == 1)
            {
                await _supplierManager.CheckAccountSupplierExistByAccountIdAsync(supplierUpdateDTO.AccountReceivableId, ResourceVN.UserMsg_NotFoundAccountReceivable);
            }
            else
            {
                supplierUpdateDTO.AccountReceivableId = null;
            }

            // update
            var supplierUp = _mapper.Map<Supplier>(supplierUpdateDTO);
            supplierUp.Status = 1;
            supplierUp.CreatedDate = supplierExsit.CreatedDate;
            supplierUp.ModifiedDate = DateTime.Now;

            List<Supplier_GroupSupplier> supplier_GroupSuppliers = new List<Supplier_GroupSupplier>();
            foreach (var groupSupplierId in supplierUpdateDTO?.GroupSuppliersId)
            {
                var newSupplier_GroupSupplier = new Supplier_GroupSupplier()
                {
                    SupplierId = supplierUp.SupplierId,
                    GroupSupplierId = groupSupplierId
                };
                supplier_GroupSuppliers.Add(newSupplier_GroupSupplier);
            }
            try
            {
                await _uow.BeginTransactionAsync();

                await _supplierRepository.UpdateAsync(supplierUp);

                await _supplier_GroupSupplierRepository.DeleteBySupplierIdDifferentGroupSupplierId(supplierUp.SupplierId, supplierUpdateDTO.GroupSuppliersId);
                if (supplier_GroupSuppliers.Count > 0)
                {
                    await _supplier_GroupSupplierRepository.InsertIgnoreAsync(supplier_GroupSuppliers);
                }

                await _uow.CommitAsync();

                var res = await _supplierRepository.GetAsync(supplierUp.SupplierId);
                var supplier = _mapper.Map<SupplierDTO>(res);
                return supplier;
            }
            catch (Exception)
            {
                await _uow.RollbackAsync();
                throw;
            }

        }

        /// <summary>
        /// xóa 1 nhà cung cấp
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (26/8/2023)
        public override async Task<int> DeleteAsync(Guid recordId)
        {
            // check tồn tại
            await _supplierManager.CheckSupplierExistByIdAsync(recordId);

            // check nhà cung cấp phát sinh chứng từ
            var isPaymented = await _supplierManager.CheckSupplierHasPaymentAsync(recordId);
            if (isPaymented)
            {
                throw new BadRequestException(ResourceVN.UserMsg_CantDeleteSupplier);
            }


            try
            {
                await _uow.BeginTransactionAsync();

                await _supplier_GroupSupplierRepository.DeleteBySupplierIdAsync(recordId);
                await _supplierRepository.DeleteAsync(recordId);

                await _uow.CommitAsync();

                return 1;
            }
            catch (Exception)
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// xóa nhiều nhà cung cấp
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (26/7/2023)
        public async Task<ResultBulkHandleRecord<SupplierDTO>> DeleteMultiSupplierAsync(List<Guid> listId)
        {
            if (listId == null || listId.Count == 0)
            {
                throw new ValidateException(ResourceVN.UserMsg_InValid);
            }
            var listSupplier = await _supplierRepository.GetListByListIdAsync(listId);
            if (listSupplier.Count < listId.Count)
            {
                throw new NotFoundException(ResourceVN.UserMsg_NotFoundSupplier);
            }
            // check nhà cung cấp đã phát sinh chứng từ
            var listSupplerIdDontDelete = await _paymentRepository.GetAllSupplierIdBySupplierIdAsync(listId);

            var listSupplierDontDelete = listSupplier.Where(e => listSupplerIdDontDelete.Contains(e.SupplierId)).ToList();
            var listSupplierDontDeleteDTO = _mapper.Map<List<SupplierDTO>>(listSupplierDontDelete);
            listSupplierDontDeleteDTO = listSupplierDontDeleteDTO.Select(e =>
            {
                e.ListUesrMsg = new List<string>{
                   ResourceVN.UserMsg_SupplierHasArisen
                };
                return e;
            }).ToList();

            // danh sách id nhà cung cấp có thể xóa
            var listSupplierIdDelete = listId.Except(listSupplerIdDontDelete).ToList();


            try
            {
                await _uow.BeginTransactionAsync();

                await _supplier_GroupSupplierRepository.DeleteByListSupplierIdAsync(listSupplierIdDelete);
                await _supplierRepository.DeleteMultiAsync(listSupplierIdDelete);
                await _uow.CommitAsync();

                return new ResultBulkHandleRecord<SupplierDTO>()
                {
                    Total = listId.Count,
                    Success = listSupplierIdDelete.Count,
                    Failure = listSupplierDontDelete.Count,
                    ListRecordFailure = listSupplierDontDeleteDTO
                };

            }
            catch (Exception)
            {
                await _uow.RollbackAsync();
                throw;
            }


        }

        /// <summary>
        /// xuat file excel danh sach nha cung cap
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// Created by: vdtien (27/7/2023)

        public async Task<byte[]> ExportSuppliersToExcel(string keySearch)
        {
            // Lấy danh sách nhân viên từ database

            var suppliersEntity = await _supplierRepository.GetListByFilterAsync(keySearch);
            var optionsRow = new List<OptionsExcel>();
            var optionsCol = new List<OptionsExcel>();
            var optionsCell = new List<OptionsExcel>();

            var suppliers = _mapper.Map<List<SupplierExcelDTO>>(suppliersEntity);

            // Tạo DataTable với các cột tương ứng
            var data = new DataTable();

            // Lấy danh sách trường của đối tượng account
            var properties = typeof(SupplierExcelDTO).GetProperties();

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
                else if (properties[i].PropertyType == typeof(decimal?))
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
            //var index = 1;
            var footSupplierExcel = new SupplierExcelDTO()
            {
                SupplierCode = "Tổng",
                MaxAccountOfDebt = 0
            };
            for (var rowIndex = 0; rowIndex < suppliers.Count; rowIndex++)
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
                    else if (properties[colIndex].PropertyType == typeof(decimal?))
                    {
                        var decimalNum = (decimal?)properties[colIndex].GetValue(suppliers[rowIndex]);
                        row[columnName] = Helper.FormatDecimal(decimalNum);
                        footSupplierExcel.MaxAccountOfDebt += decimalNum ?? 0;
                    }
                    else
                    {
                        if (properties[colIndex].GetValue(suppliers[rowIndex]) != null)
                            row[columnName] = properties[colIndex].GetValue(suppliers[rowIndex]);
                    }

                }
                // thêm hàng vào dataTable
                data.Rows.Add(row);
            }
            var rowEnd = data.NewRow();
            for (var colIndex = 0; colIndex < properties.Length; colIndex++)
            {
                var displayNameAttribute = properties[colIndex].GetCustomAttribute<DisplayAttribute>();
                var columnName = displayNameAttribute != null ? displayNameAttribute.GetName() : properties[colIndex].Name;
                if (properties[colIndex].GetCustomAttribute<IndexProperty>() != null)
                {
                    rowEnd[columnName] = "";
                }
                else if (properties[colIndex].PropertyType == typeof(decimal?))
                {
                    var decimalNum = (decimal?)properties[colIndex].GetValue(footSupplierExcel);
                    rowEnd[columnName] = Helper.FormatDecimal(decimalNum);
                }
                else
                {
                    if (properties[colIndex].GetValue(footSupplierExcel) != null)
                        rowEnd[columnName] = properties[colIndex].GetValue(footSupplierExcel);
                }
            }
            data.Rows.Add(rowEnd);
            optionsRow.Add(new OptionsExcel()
            {
                Row = suppliers.Count,
                FontBold = true,
                BackgroundColorHex = "#FFD3D3D3"
            });


            // tao tile cho file
            var title = "Danh sách nhà cung cấp";

            // Sử dụng _excelService để xuất dữ liệu ra file Excel
            var excelData = await _excelInfra.ExportToExcelAsync(data, title, optionsRow, optionsCol, null);

            return excelData;

            // Lấy danh sách nhân viên từ database
        }

    }
}
