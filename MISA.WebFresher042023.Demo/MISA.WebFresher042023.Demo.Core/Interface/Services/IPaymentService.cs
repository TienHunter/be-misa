using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Services
{
    /// <summary>
    /// interface payment service
    /// </summary>
    /// creatd by: vdtien (14/8/2023)
    public interface IPaymentService:IBaseService<PaymentDTO, PaymentCreateDTO,PaymentUpdateDTO>
    {
        /// <summary>
        /// cập nhật trạng thái của phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// creatd by: vdtien (14/8/2023)
        public Task<PaymentDTO> UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus);

        /// <summary>
        /// xóa nhiều phiếu chi theo danh sách id phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        /// creatd by: vdtien (14/8/2023)
        public Task<ResultBulkHandleRecord<PaymentDTO>> BulkDeletePaymentAsync (List<Guid> listPaymentId);

        /// <summary>
        /// cập nhật trạng thái phiếu chi theo danh sách idm và trạng thái
        /// </summary>
        /// <param name="listPaymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// creatd by: vdtien (14/8/2023)
        public Task<ResultBulkHandleRecord<PaymentDTO>> BulkUpdatePaymentStatusAsync(List<Guid> listPaymentId, PaymentStatus paymentStatus);

        /// <summary>
        /// xuất excel danmh sách phiếu chi theo từ khóa
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// creatd by: vdtien (14/8/2023)
        public Task<byte[]> ExportPaymentsToExcel(string keySearch);
    }
}
