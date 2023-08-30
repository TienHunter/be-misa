using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController : BasesController<PaymentDTO, PaymentCreateDTO, PaymentUpdateDTO>
    {
        #region Field
        private readonly IPaymentService _paymentService;
        #endregion

        #region Constructor
        public PaymentsController(IPaymentService paymentService) : base(paymentService)
        {
            _paymentService = paymentService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// cập nhật trạng thái phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        [HttpPut("update-payment-status/{paymentId}")]
        public async Task<IActionResult> UpdateStatusPayment([FromRoute][Required] Guid paymentId, [FromQuery][Required] PaymentStatus paymentStatus)
        {
            var result = await _paymentService.UpdatePaymentStatusAsync(paymentId, paymentStatus);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// xóa nhiều phiếu chi
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)

        [HttpPost("delete-multi")]
        public override async Task<IActionResult> DeleteMultiAsync([FromBody] List<Guid> listId)
        {
            var results = await _paymentService.BulkDeletePaymentAsync(listId);
            return StatusCode(StatusCodes.Status200OK, results);
        }

        /// <summary>
        /// cập nhật trạng thái nhiều phiếu cho
        /// </summary>
        /// <param name="listPaymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        [HttpPut("update-multi-payment-status")]
        public async Task<IActionResult> UpdateStatusPayment([FromBody] List<Guid> listPaymentId, [FromQuery] PaymentStatus paymentStatus)
        {
            var result = await _paymentService.BulkUpdatePaymentStatusAsync(listPaymentId, paymentStatus);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// xuất excel danh sách phiếu chi theo từ khóa
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] string? keySearch = "")
        {
            var excelData = await _paymentService.ExportPaymentsToExcel(keySearch ?? "");
            DateTime currentTime = DateTime.UtcNow;
            long timestampInMilliseconds = currentTime.Ticks / TimeSpan.TicksPerMillisecond;
            var fileName = $"Danh_sach_phieu_chi_{timestampInMilliseconds}.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        } 
        #endregion
    }
}
