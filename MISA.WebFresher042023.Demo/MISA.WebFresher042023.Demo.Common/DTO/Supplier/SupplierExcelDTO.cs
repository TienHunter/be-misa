using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher042023.Demo.Common.DTO.Supplier
{
    /// <summary>
    /// class supplier excel dto
    /// </summary>
    public class SupplierExcelDTO
    {
        /// <summary>
        /// stt
        /// </summary>
        [IndexProperty]
        [Display(Name="STT")]
        public int Index { get; set; }
        
        /// <summary>
        /// mã nhà cung cấp
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.SupplierCode))]
        public string? SupplierCode { get; set; }
       
        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.SupplierName))]
        public string? SupplierName { get; set; }

        /// <summary>
        /// mã số thuế
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TaxCode))]
        public string? TaxCode { get; set; }

        /// <summary>
        /// địa chỉ
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Address))]
        public string? Address { get; set; }

        /// <summary>
        /// số nợ
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountOfDebt))]
        public decimal? MaxAccountOfDebt { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PhoneNumber))]
        public string? PhoneNumber { get; set; }

    }
}
