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

namespace MISA.WebFresher042023.Demo.Common.DTO
{

    /// <summary>
    /// class contract infor
    /// </summary>
    public class ContractInfor
    {
        /// <summary>
        /// xưng hô
        /// </summary>
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Vocative))]
        public string? Vocative { get;set; }

        /// <summary>
        /// họ tên
        /// </summary>
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Fullname))]
        public string? Fullname { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_InValidEmail))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Email))]
        public string? Email { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        [MaxLength(length: 50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.MobilePhoneNumber))]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// đại diện pháo luật
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.LegalRepresentative))]
        public string? LegalRepresentative { get; set; }

        /// <summary>
        /// họ tên người nhận hóa đơn
        /// </summary>
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.FullnameReceiverBill))]
        public string? FullnameReceiverBill { get; set; }

        /// <summary>
        /// email người nhận hóa đơn
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.EmailReceiverBill))]
        public string? EmailReceiverBill { get;set; }

        /// <summary>
        /// số điện thoại người nhận
        /// </summary>
        [MaxLength(length: 50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PhoneNumberReceiverBill))]
        public string? PhoneNumberReceiverBill { get;  set; }

        /// <summary>
        /// số chứng minh thư
        /// </summary>
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [MaxLength(length: 25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.IdentityNumber))]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// ngày cấp
        /// </summary>
        [ValidDateLessNow(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_IdentityDateRelease))]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// nơi cấp
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.IdentityPlaceRelease))]
        public string? IdentityPlace { get; set; }

    }
}
