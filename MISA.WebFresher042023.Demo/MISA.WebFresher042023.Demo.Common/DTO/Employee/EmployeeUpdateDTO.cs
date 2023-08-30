using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Employee
{
    public class EmployeeUpdateDTO
    {
        /// <summary>
        /// id nhan vien
        /// </summary>
        [Required]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// ma nhan vien
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [RegularExpression(@"^(NV-)(\d+)$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_EmployeeCodeFormat))]
        [MaxLength(length: 20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.EmployeeCode))]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// ten nhan vien
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Fullname))]

        public string? FullName { get; set; }

        /// <summary>
        /// id phong ban
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_DepartmentIdEmpty))]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// ten phong ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// vi tri
        /// </summary>
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PositionName))]
        public string? PositionName { get; set; }

        /// <summary>
        /// ngay sinh
        /// </summary>
        [ValidDateLessNow(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_DateOfBirth))]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// gioi tinh
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// so can cuoc cong dan
        /// </summary>
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [MaxLength(length: 25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.IdentityNumber))]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// ngay cap
        /// </summary>
        [ValidDateLessNow(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_IdentityDateRelease))]

        public DateTime? IdentityDateRelease { get; set; }

        /// <summary>
        /// noi cap
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.IdentityPlaceRelease))]
        public string? IdentityPlaceRelease { get; set; }


        /// <summary>
        /// dia chi
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Address))]
        public string? Address { get; set; }

        /// <summary>
        /// dien thoai co dinh
        /// </summary>
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [MaxLength(length: 50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PhoneNumber))]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// dien thoai di dong
        /// </summary>
       // [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [MaxLength(length: 50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.MobilePhoneNumber))]
        public string? MobilePhoneNumber { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_InValidEmail))]
        //[ValidEmail(ErrorMessageResourceType =typeof(ResourceVN),ErrorMessageResourceName = nameof(ResourceVN.UserMsg_InValidEmail))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Email))]
        public string? Email { get; set; }

        /// <summary>
        /// tai khoan ngan hang
        /// </summary>
        [MaxLength(length: 25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankAccount))]
        public string? BankAccount { get; set; }

        /// <summary>
        /// ten ngan hang
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankName))]
        public string? BankName { get; set; }

        /// <summary>
        /// chi nhanh ngan hang
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankBranch))]
        public string? BankBranch { get; set; }

        /// <summary>
        /// la khach hang
        /// </summary>

        [Range(0, 1, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_WrongInput))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.IsCustomer))]
        public int? IsCustomer { get; set; }

        /// <summary>
        /// la nha chung cap
        /// </summary>
        [Range(0, 1, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_WrongInput))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.IsSupplier))]
        public int? IsSupplier { get; set; }
    }
}
