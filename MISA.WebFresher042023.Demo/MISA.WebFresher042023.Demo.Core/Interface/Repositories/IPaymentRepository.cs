using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using MISA.WebFresher042023.Demo.Common.DTO.Payments;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repositories
{
    /// <summary>
    /// interface payment repository
    /// </summary>
    /// created by: vdtien (13/8/2023)
    public interface IPaymentRepository : IBaseRepository<Payment>
    {

        /// <summary>
        /// cập nhật trạng thái của phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public Task UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus);

        /// <summary>
        /// cập nhật trạng thái phiếu chi hàng loạt
        /// </summary>
        /// <param name="listPaymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public Task BulkUpdatePaymentStatusAsync(List<Guid> listPaymentId, PaymentStatus paymentStatus);

        /// <summary>
        /// Lấy danh sách phiếu chi xuất excel theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public Task<List<PaymentExcelDTO>> GetAllPaymentExcelAsync(string keySearch);

        /// <summary>
        /// lấy phiếu chi theo id nhà cung cấp
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public Task<bool> CheckSupplierHasPaymentBySupplierIdAsync(Guid supplierId);

        /// <summary>
        /// lấy danh sach guid_guid paymentId và supplierId theo supplierId
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public Task<List<Guid>> GetAllSupplierIdBySupplierIdAsync(List<Guid> supplierId);


    }
}
