using MISA.WebFresher042023.Demo.Common.DTO.Acccounting;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher042023.Demo.Common.DTO.Payment
{
    /// <summary>
    /// class payment create dto
    /// </summary>
    public class PaymentCreateDTO
    {
        /// <summary>
        /// id nhà cung cấp
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Supplier))]
        public Guid? SupplierId { get; set; }

        /// <summary>
        /// id nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// id loại phiếu chi
        /// </summary>
        public ReasonType? ReasonTypeId { get; set; }

        /// <summary>
        /// số phiếu chi
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [MaxLength(length: 20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PaymentCode))]
        public string? PaymentCode { get; set; }

        /// <summary>
        /// kèm theo
        /// </summary>

        public int? AttachOriginalDocuments { get; set; }

        /// <summary>
        /// ngày hạch toán
        /// </summary>

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountingDate))]
        public DateTime? AccountingDate { get; set; }

        /// <summary>
        /// ngày phiếu chi
        /// </summary>

        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PaymentDate))]
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// người nhận
        /// </summary>
        public string? Receiver { get; set; }

        /// <summary>
        /// tên nhà cung cấp
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.SupplierName))]
        public string? SupplierName { get; set; }

        /// <summary>
        /// địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// lý do chi
        /// </summary>
        public string? ReasonSpending { get; set; }
        //public string? EmployeeName { get; set; }

        /// <summary>
        /// danh sách hạch toán 
        /// </summary>
        [Required]
        public List<AccountingCreateDTO> Accountings { get; set; }

        /// <summary>
        /// tổng tiền
        /// </summary>
        public decimal TotalMoney { get; set; }
    }
}
