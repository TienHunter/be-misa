using MISA.WebFresher042023.Demo.Common.Attributes;
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

namespace MISA.WebFresher042023.Demo.Common.DTO.Account
{
    public class AccountExcelDTO
    {
        /// <summary>
        /// chi so hang
        /// </summary>
        [Display(Name = "STT")]
        [IndexProperty]
        public int Index { get; set; }

        /// <summary>
        /// ma tai khoan
        /// </summary>

        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountCode))]
        public string? AccountCode { get; set; }

        /// <summary>
        /// ten tai khoan
        /// </summary>
      
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountName))]
        public string? AccountName { get; set; }

        /// <summary>
        /// ten tieng anh cua tai khoan
        /// </summary>
      
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountNameEnglish))]
        public string? AccountNameEnglish { get; set; }


        /// <summary>
        /// tinh chat tai khoan
        /// </summary>
    
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountFeature))]
        public AccountFeature? AccountFeature { get; set; }

        /// <summary>
        /// dien giai tai khoan
        /// </summary>
        [Display(ResourceType =typeof(ResourceVN),Name =nameof(ResourceVN.Explain))]
        public string? Explain { get; set; }

        /// <summary>
        /// Trang thai su dung
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Status))]
        public Status? Status { get; set; }


    }
}
