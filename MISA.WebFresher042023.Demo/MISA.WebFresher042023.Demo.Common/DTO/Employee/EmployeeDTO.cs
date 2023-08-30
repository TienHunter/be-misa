using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Employee
{
    public class EmployeeDTO
    {
        /// <summary>
        /// id cua employee
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// ma nhan vien
        /// </summary>
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// ten nhan vien
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// id phong ban
        /// </summary>

        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// ten phong ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// vi tri
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// ngay sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// gioi tinh
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// so can cuoc cong dan
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// ngay cap
        /// </summary>
        public DateTime? IdentityDateRelease { get; set; }

        /// <summary>
        /// noi cap
        /// </summary>
        public string? IdentityPlaceRelease { get; set; }


        /// <summary>
        /// dia chi
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// dien thoai co dinh
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// dien thoai di dong
        /// </summary>
        public string? MobilePhoneNumber { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// tai khoan ngan hang
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// ten ngan hang
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// chi nhanh ngan hang
        /// </summary>
        public string? BankBranch { get; set; }

        /// <summary>
        /// nguoi tao
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// nguoi sua
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// la khach hang
        /// </summary>
        public int? IsCustomer { get; set; }

        /// <summary>
        /// la nha chung cap
        /// </summary>
        public int? IsSupplier { get; set; }
    }
}
