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
    /// class supplier create dto
    /// </summary>
    public class SupplierCreateDTO
    {

        /// <summary>
        /// mã nhà cung cấp
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [MaxLength(length: 20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.SupplierCode))]
        public string? SupplierCode { get; set; }

        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_NotEmpty))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.SupplierName))]
        public string? SupplierName { get; set; }

        /// <summary>
        /// mã sổ thuế
        /// </summary>
        [MaxLength(length: 20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.TaxCode))]
        public string? TaxCode { get; set; }

        /// <summary>
        /// địa chỉ
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Address))]
        public string? Address { get; set; }

        /// <summary>
        /// số điện thoại cố định
        /// </summary>
        [MaxLength(length: 50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_ContainsOnlyNumber))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.PhoneNumber))]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// website
        /// </summary>
        [MaxLength(length: 255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Website))]
        public string? Website { get; set; }

        /// <summary>
        /// danh sách id nhóm nhà cung cấp
        /// </summary>
        public List<Guid>? GroupSuppliersId { get; set; }

        /// <summary>
        /// id nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// loại nhà cung cấp
        /// </summary>
        public int? SupplierType { get; set; }

        /// <summary>
        /// là khách hàng không
        /// </summary>
        public int? IsCustomer { get; set; }

        /// <summary>
        /// thông tin liên hệ
        /// </summary>
        public ContractInfor? ContractInfor { get; set; }

        /// <summary>
        /// id điều khoản thanh toán
        /// </summary>
        public Guid? TermPaymentId { get; set; }

        /// <summary>
        /// số ngày hết hạn
        /// </summary>
        [Range(0,999999, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_Range))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.DueTime))]
        public int? DueTime { get; set; }

        /// <summary>
        /// số nợ
        /// </summary>

        [Range(0, 999999999999, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_Range))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.MaxAccountOfDebt))]
        public decimal? MaxAccountOfDebt { get; set; }

        /// <summary>
        /// tài khoản trả
        /// </summary>
        public Guid? AccountPayableId { get; set; }

        /// <summary>
        /// tài khoản thu
        /// </summary>
        public Guid? AccountReceivableId { get; set; }

        /// <summary>
        /// danh sách tài khoản ngân hàng
        /// </summary>
        public List<BankAccount>? BanksAccount { get; set; }

        /// <summary>
        /// danh sách địa chỉ nhận
        /// </summary>
        public List<string>? DeliverAddress { get; set; }

        /// <summary>
        /// id đất nước
        /// </summary>
        public Guid? CountryId { get; set; }

        /// <summary>
        /// id thành phố
        /// </summary>
        public Guid? CityId { get; set; }

        /// <summary>
        /// id quận huyện
        /// </summary>
        public Guid? DistrictId { get; set; }


        /// <summary>
        /// id xã phường
        /// </summary>
        public Guid? WardId { get; set; }

        /// <summary>
        /// giống dịa chỉ nhà cung cấp
        /// </summary>
        public int? IsSameSupplierAddress { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        [MaxLength(length: 500, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_MaxLength))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Note))]
        public string? Note { get; set; }
    }
}
