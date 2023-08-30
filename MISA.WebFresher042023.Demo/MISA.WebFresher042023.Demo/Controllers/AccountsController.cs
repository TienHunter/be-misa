using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.DTO.Account;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Services;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : BasesController<AccountDTO, AccountCreatedDTO, AccountUpdateDTO>
    {
        #region Field
        private readonly IAccountService _accountService;
        #endregion
        #region Constructor
        public AccountsController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// lấy danh sách phân trang tài khoản theo hình cây, phân trang theo nút gốc
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: 17/7/2023

        [HttpGet("filter-tree")]
        public async Task<IActionResult> GetListTreeAsync([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] string? keySearch = "")
        {
            var results = await _accountService.GetListTreeAsync(pageSize, pageNumber, keySearch ?? "");
            return StatusCode(StatusCodes.Status200OK, results);
        }

        /// <summary>
        /// lay ma nhan vien moi
        /// </summary>
        /// <returns>ma nhan vien moi</returns>
        /// Created by: vdtien (17/7/2023)
        [HttpGet("parent/{parentId}/childrens")]
        public async Task<IActionResult> GetListAccountByParentId([FromRoute] Guid parentId)
        {

            var result = await _accountService.GetListAccountByParentId(parentId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// lay danh sach tai khoan con theo danh sach id tai khoan cha
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// created by: vdtien(17/7/2023)
        [HttpPost("parents/childrens")]
        public async Task<IActionResult> GetListAccountByListParentId([FromBody] List<Guid> listId)
        {
            var results = await _accountService.GetListAccountByListParentId(listId);
            return StatusCode(StatusCodes.Status200OK, results);
        }

        /// <summary>
        /// xuất file excel danh sách tài khoản theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: vdtien(17/7/2023)
        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] string? keySearch = "")
        {
            var excelData = await _accountService.ExportAccountsToExcel(keySearch ?? "");
            DateTime currentTime = DateTime.UtcNow;
            long timestampInMilliseconds = currentTime.Ticks / TimeSpan.TicksPerMillisecond;
            var fileName = $"Danh_sach_tai_khoan_{timestampInMilliseconds}.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// cập nhật trạng thái cho tài khoản
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// created by: vdtien(17/7/2023)

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromQuery] Guid accountId, [FromQuery] Status status, [FromQuery] TypeUpdate type)
        {
            var result = await _accountService.UpdateStatusAsync(accountId, status, type);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// tra ve danh sach tat ca ban ghi theo query
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (19/7/2023)
        [HttpGet("filter-query")]
        public async Task<IActionResult> GetAllAsync([FromQuery] List<AccountFeature>? accountFeatures, [FromQuery] List<UserObject>? userObjects)
        {
            var accounts = await _accountService.GetAllQueryAsync(accountFeatures, userObjects);
            return StatusCode(StatusCodes.Status200OK, accounts);
        }

        #endregion
    }
}
