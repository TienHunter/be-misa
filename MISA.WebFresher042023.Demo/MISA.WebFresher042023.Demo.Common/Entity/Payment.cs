using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Entity
{

    /// <summary>
    /// class payment
    /// </summary>
    public class Payment : BaseEntity
    {
        /// <summary>
        /// id phiếu chi
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// id nhà cung cấp
        /// </summary>
        public Guid? SupplierId { get; set; }

        /// <summary>
        /// id nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// id loại sử dụng
        /// </summary>
        public ReasonType? ReasonTypeId { get; set; }

        /// <summary>
        /// mã phiếu chi
        /// </summary>
        public string PaymentCode { get; set; }

        /// <summary>
        /// kèm theo
        /// </summary>
        public int? AttachOriginalDocuments { get; set; }

        /// <summary>
        /// ngày hạch toán
        /// </summary>
        public DateTime? AccountingDate { get; set; }

        /// <summary>
        /// ngày phiếu chi
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// người nhận
        /// </summary>
        public string? Receiver { get; set; }

        /// <summary>
        /// mã nhà cung cấp
        /// </summary>
        public string? SupplierCode { get; set; }

        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        public string? SupplierName { get; set; }

        /// <summary>
        /// địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// lý do chi
        /// </summary>
        public string? ReasonSpending { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        public string? EmployeeName { get; set; }

        /// <summary>
        /// danh sách hạch toán
        /// </summary>
        [IgnorePropertyAttribute]
        public List<Accounting> Accountings { get; set; }

        /// <summary>
        /// trạng thái phiếu chi
        /// </summary>
        public PaymentStatus? PaymentStatus { get; set; }

        /// <summary>
        ///  tổng tiền
        /// </summary>
        public decimal TotalMoney { get; set; }
    }
}
