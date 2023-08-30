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
    public class EmployeeExcelDTO
    {
        [IndexProperty]
        [Display(Name ="STT")]
        public int Index { get; set; }

        /// <summary>
        /// ma nhan vien
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.EmployeeCode))]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// ten nhan vien
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Fullname))]
        public string? FullName { get; set; }

        /// <summary>
        /// gioi tinh
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Gender))]
        public Gender? Gender { get; set; }

        /// <summary>
        /// ngay sinh
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DateOfBirth))]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// vi tri
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PositionName))]
        public string? PositionName { get; set; }

        /// <summary>
        /// ten phong ban
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DepartmentName))]
        public string? DepartmentName { get; set; }



        ///// <summary>
        ///// email
        ///// </summary>
        //[Display(Name = "Email")]
        //public string? Email { get; set; }

        /// <summary>
        /// tai khoan ngan hang
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankAccount))]
        public string? BankAccount { get; set; }

        /// <summary>
        /// ten ngan hang
        /// </summary>
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.BankName))]
        public string? BankName { get; set; }

    }
}
