using AutoMapper;
using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.DTO.Account;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Services
{
    public class EmployeeService : BaseService<Employee,EmployeeDTO,EmployeeCreateDTO,EmployeeUpdateDTO> ,IEmployeeService
    {
        protected readonly IEmployeeRepository _employeeRepository;
        protected readonly IDepartmentRepository _departmentRepository;
        private readonly IExcelInfra _excelInfra;
        private readonly IEmployeeManger _employeeManger;


        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,IExcelInfra excelInfra, IEmployeeManger employeeManger, IMapper mapper,IUnitOfWork uow) : base(employeeRepository, mapper,uow)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _excelInfra = excelInfra;
            _employeeManger = employeeManger;
        }

        /// <summary>
        /// them nhan vien
        /// </summary>
        /// <returns>nhan vien</returns>
        /// Created by: vdtien(17/6/2023)
        public override async Task<EmployeeDTO?> InsertAsync(EmployeeCreateDTO employeeCreateDTO)
        {
            // check dup employeecode
            await _employeeManger.IsDupEmployeeCode(employeeCreateDTO.EmployeeCode, null);
            // check department exsit
            var department = await _departmentRepository.GetAsync(employeeCreateDTO.DepartmentId);
            if(department == null)
            {
                throw new NotFoundException("Không tìm thấy phòng ban.");
            }


            var result = await base.InsertAsync(employeeCreateDTO);
            return result;
        }

        /// <summary>
        /// cap nhat nhan vien
        /// </summary>
        /// <returns> nhan vien</returns>
        /// Created by: vdtien(17/6/2023)
        public override async Task<EmployeeDTO?> UpdateAsync(Guid employeeId, EmployeeUpdateDTO employeeUpdateDTO)
        {

            var employee = _mapper.Map<Employee>(employeeUpdateDTO);

            employee.EmployeeId = employeeId;


            // validate input

            var employeeExist = await _employeeRepository.GetAsync(employee.EmployeeId);
            if(employeeExist == null)
            {
                throw new NotFoundException();
            }
            employee.CreatedBy = employeeExist.CreatedBy;
            employee.CreatedDate = employeeExist.CreatedDate;
            employee.ModifiedBy = Guid.NewGuid();
            employee.ModifiedDate = new DateTime();

            // check dup employeecode
            await _employeeManger.IsDupEmployeeCode(employee.EmployeeCode, employee.EmployeeId);
            // check department exsit
            var department = await _departmentRepository.GetAsync(employee.DepartmentId);
            if (department == null)
            {
                throw new NotFoundException("Không tìm thấy phòng ban.");
            }

            var result = await _employeeRepository.UpdateAsync(employee);
            if (result == null)
            {
                // throw loi server
                throw new Exception();
            }
            var employeeDTO = _mapper.Map<EmployeeDTO>(result);
            return employeeDTO;
        }

        /// <summary>
        /// lay ma moi
        /// </summary>
        /// <returns>ma moi</returns>
        /// Created by: vdtien(17/6/2023)
        public async Task<string> GetNewEmployeeCodeSerivceAsync()
        {
            var result = await _employeeRepository.GetNewCode();
            return result;
        }

        /// <summary>
        /// Xuất excel danh sách nhân viên
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportEmployeesToExcel(string keySearch)
        {
            // Lấy danh sách nhân viên từ database

            var employeesEntity = await _employeeRepository.GetListByFilterAsync(keySearch);
            var optionsRow = new List<OptionsExcel>();
            var optionsCol = new List<OptionsExcel>();
            var optionsCell = new List<OptionsExcel>();

            var employees = _mapper.Map<List<EmployeeExcelDTO>>(employeesEntity);

            // Tạo DataTable với các cột tương ứng
            var data = new DataTable();

            // Lấy danh sách trường của đối tượng account
            var properties = typeof(EmployeeExcelDTO).GetProperties();

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
                } else if(properties[i].PropertyType == typeof(DateTime?))
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
            for (var rowIndex = 0; rowIndex < employees.Count; rowIndex++)
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
                    else if (properties[colIndex].PropertyType == typeof(DateTime?))
                    {
                        var dateTimeValue = (DateTime?)properties[colIndex].GetValue(employees[rowIndex]);
                        if (dateTimeValue.HasValue)
                        {
                            var processedDateTime = ConvertDateTimeToString(dateTimeValue.Value);
                            row[columnName] = processedDateTime;
                        }
                        else
                        {
                            row[columnName] = DBNull.Value;
                        }
                    }
                    else if (properties[colIndex].PropertyType == typeof(Gender?))
                    {
                        var gender = (Gender?)properties[colIndex].GetValue(employees[rowIndex]);
                        var processedGender = ConvertGender(gender);
                        row[columnName] = processedGender;
                    }
                    else
                    {
                        row[columnName] = properties[colIndex].GetValue(employees[rowIndex]);
                    }

                }
                // thêm hàng vào dataTable
                data.Rows.Add(row);
            }


            // tao tile cho file
            var title = "Danh sách nhân viên";

            // Sử dụng _excelService để xuất dữ liệu ra file Excel
            var excelData = await _excelInfra.ExportToExcelAsync(data, title, null, optionsCol, null);

            return excelData;

        }

        /// <summary>
        /// convert datetime về dang dd/mm/yyyy
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        /// Created by: vdtien (27/6/2023)
        private string ConvertDateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// convert Gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        /// Created by: vdtien (27/6/2023)
        private string ConvertGender(Gender? gender)
        {
            return gender switch
            {
                Gender.Male => ResourceVN.Male,
                Gender.Female => ResourceVN.FeMale,
                Gender.Other => ResourceVN.Other,
                _ => "",
            };
        }

    }
}
