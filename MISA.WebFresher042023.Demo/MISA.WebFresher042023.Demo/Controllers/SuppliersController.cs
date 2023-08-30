using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Common.Resources;
using MySqlConnector;
using System.Text.RegularExpressions;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Department;
using MISA.WebFresher042023.Demo.Common.DTO.Supplier;
using MISA.WebFresher042023.Demo.Core.Services;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuppliersController : BasesController<SupplierDTO, SupplierCreateDTO, SupplierUpdateDTO>
    {

        #region Field
        private readonly ISupplierService _supplierService;
        #endregion

        #region Constructor
        public SuppliersController(ISupplierService supplierService) : base(supplierService)
        {
            _supplierService = supplierService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// xuất excel danh sách nhà cung cấp theo từ khóa
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: 25/7/2023
        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] string? keySearch = "")
        {
            var excelData = await _supplierService.ExportSuppliersToExcel(keySearch ?? "");
            DateTime currentTime = DateTime.UtcNow;
            long timestampInMilliseconds = currentTime.Ticks / TimeSpan.TicksPerMillisecond;
            var fileName = $"Danh_sach_nha_cung_cap_{timestampInMilliseconds}.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// xóa hàng loạt nhà cung cấp theo danh sách id
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// created by: 25/7/2023
        [HttpPost("delete-multi")]
        public override async Task<IActionResult> DeleteMultiAsync([FromBody] List<Guid> listId)
        {
            var results = await _supplierService.DeleteMultiSupplierAsync(listId);
            return StatusCode(StatusCodes.Status200OK, results);
        }
        #endregion
    }
}
