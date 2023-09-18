using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Common.Resources;
using MySqlConnector;
using System.Text.RegularExpressions;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using Microsoft.AspNetCore.Authorization;

namespace MISA.WebFresher042023.Demo.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<EmployeeDTO, EmployeeCreateDTO, EmployeeUpdateDTO>
    {
        #region Field
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lay ma nhan vien moi
        /// </summary>
        /// <returns>ma nhan vien moi</returns>
        /// Created by: vdtien (17/6/2023)
        [HttpGet("newEmployeeCode")]
        public async Task<IActionResult> GetNewEmployeCodeAsync()
        {

            var result = await _employeeService.GetNewEmployeeCodeSerivceAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// xuất excel danh sách nhân viên theo từ khóa
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] string? keySearch = "")
        {
            var excelData = await _employeeService.ExportEmployeesToExcel(keySearch ?? "");
            DateTime currentTime = DateTime.UtcNow;
            long timestampInMilliseconds = currentTime.Ticks / TimeSpan.TicksPerMillisecond;
            var fileName = $"Danh_sach_nhan_vien_{timestampInMilliseconds}.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        #endregion
    }
}
