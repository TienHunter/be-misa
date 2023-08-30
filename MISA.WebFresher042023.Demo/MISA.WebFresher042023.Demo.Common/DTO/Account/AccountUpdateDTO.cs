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
    /// <summary>
    /// update dto tai khoan
    /// Created by: vdtien (18/7/2023)
    /// </summary>
    public class AccountUpdateDTO
    {
        /// <summary>
        /// id tai khoan
        /// </summary>
        public Guid AccountId { get; set; }


        /// <summary>
        /// ma tai khoan
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [MaxLength(length: 20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [MinLength(length: 3, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MinLength))]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_AccountCodeFormat))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountCode))]
        public string? AccountCode { get; set; }

        /// <summary>
        /// ten tai khoan
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountName))]
        public string? AccountName { get; set; }

        /// <summary>
        /// ten tieng anh cua tai khoan
        /// </summary>
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountNameEnglish))]
        public string? AccountNameEnglish { get; set; }

        /// <summary>
        /// id cha tai khoan
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// tinh chat tai khoan
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.AccountFeature))]
        public AccountFeature? AccountFeature { get; set; }

        /// <summary>
        /// dien giai tai khoan
        /// </summary>
        public string? Explain { get; set; }

        /// <summary>
        /// Trang thai su dung
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Status))]
        public Status? Status { get; set; }

        /// <summary>
        /// doi tuong su dung
        /// </summary>
        public UserObject? UserObject { get; set; }

    }
}
