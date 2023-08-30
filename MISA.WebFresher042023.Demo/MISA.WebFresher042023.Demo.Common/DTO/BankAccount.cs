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
    /// class bank account
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// số tài khoản ngân hàng
        /// </summary>
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [MaxLength(length: 25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankAccount))]
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankName))]
        public string? BankName { get; set; }

        /// <summary>
        /// chi nhánh
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankBranch))]
        public string? BankBranch { get; set; }

        /// <summary>
        /// tỉnh/ thành phố ngân hàng
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankCity))]
        public string? BankCity { get; set; }

    }
}
