using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Payments
{
    /// <summary>
    /// class payment excel dto
    /// </summary>
    public class PaymentExcelDTO
    {

        /// <summary>
        /// STT
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.NumberOrder))]
        public int NumberOrder { get; set; }

        /// <summary>
        /// ngày hạch toán
        /// </summary>
        [Display(ResourceType=typeof(ResourceVN),Name =nameof(ResourceVN.AccountingDate))]
        public DateTime AccountingDate { get; set; }

        /// <summary>
        /// ngày phiếu chi
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PaymentDate))]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// số phiếu chi
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PaymentCode))]
        public string? PaymentCode { get; set; }

        /// <summary>
        /// lý do chi
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Explain))]
        public string? ReasonSpending { get; set; }

        /// <summary>
        /// tổng tiền
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Money))]
        public decimal TotalMoney { get; set; }

        /// <summary>
        ///  tên nhà cung cấp
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.SupplierName))]
        public string? SupplierName { get;set; }

        /// <summary>
        /// loại phiếu chi
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.ResonDepositWithdraw))]
        public ReasonType ReasonTypeId { get; set; }

        /// <summary>
        /// loại chứng từ
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.ReceiptType))]
        public string? PaymentType { get; set; }

    }
}
