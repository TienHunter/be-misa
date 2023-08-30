using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Entity
{

    /// <summary>
    /// class supplier
    /// </summary>
    public class Supplier : BaseEntity
    {
        /// <summary>
        /// id nhà cung cấp
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// mã nhà cung cấp
        /// </summary>
        public string? SupplierCode { get; set; }

        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        public string? SupplierName { get; set; }

        /// <summary>
        /// mã số thuế
        /// </summary>
        public string? TaxCode { get; set; }

        /// <summary>
        /// địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// website
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// danh sách id nhóm nhà cung cấp
        /// </summary>
        public string? GroupSuppliersId { get; set; }

        /// <summary>
        /// id nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        public string? EmployeeName { get; set; }

        /// <summary>
        /// loại nhà cung cấp
        /// </summary>
        public int? SupplierType { get; set; }

        /// <summary>
        /// trạng thía
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// là khách hàng
        /// </summary>
        public int? IsCustomer { get; set; }

        /// <summary>
        /// thông tin liên hệ
        /// </summary>
        public string? ContractInfor { get; set; }

        /// <summary>
        /// id điều khoản thanh toán
        /// </summary>
        public Guid? TermPaymentId { get; set; }

        /// <summary>
        /// sô điều khoản thanh toán
        /// </summary>
        public string? TermPaymentCode { get; set; }

        /// <summary>
        /// tên điều khoản thanh toán
        /// </summary>
        public string? TermPaymentName { get; set; }

        /// <summary>
        /// số ngày được nợ
        /// </summary>
        public int? DueTime { get; set; }

        /// <summary>
        /// sô tiền nợ
        /// </summary>
        public decimal? MaxAccountOfDebt { get; set; }

        /// <summary>
        /// id tài khoản trả
        /// </summary>
        public Guid? AccountPayableId { get; set; }

        /// <summary>
        /// id tài khoản nợ
        /// </summary>
        public string? AccountPayableCode { get; set; }

        /// <summary>
        /// tên tài khoản nợ
        /// </summary>
        public string? AccountPayableName { get; set; }

        /// <summary>
        /// id tài khoản nhận
        /// </summary>
        public Guid? AccountReceivableId { get; set; }

        /// <summary>
        /// số tài khoản nhận
        /// </summary>
        public string? AccountReceivableCode { get; set; }
        
        /// <summary>
        /// tên tài khoản nhận
        /// </summary>
        public string? AccountReceivableName { get; set; }

        /// <summary>
        /// danh sách ngân hàng
        /// </summary>
        public string? BanksAccount { get; set; }

        /// <summary>
        /// địa chỉ nhận
        /// </summary>
        public string? DeliverAddress { get; set; }

        /// <summary>
        /// id quốc gia
        /// </summary>
        public Guid? CountryId { get; set; }

        /// <summary>
        /// tên quốc gia
        /// </summary>
        public string? CountryName { get; set; }

        /// <summary>
        /// id thành phố
        /// </summary>
        public Guid? CityId { get; set; }

        /// <summary>
        /// id thành phố 
        /// </summary>
        public string? CityName { get; set; }

        /// <summary>
        /// id quận huyện
        /// </summary>
        public Guid? DistrictId { get; set; }

        /// <summary>
        /// tên quận huyện
        /// </summary>
        public string? DistrictName { get; set; }

        /// <summary>
        /// id xã phường
        /// </summary>
        public Guid? WardId { get; set; }

        /// <summary>
        /// tên xã phường
        /// </summary>
        public string? WardName { get; set; }

        /// <summary>
        /// giống địa chỉ nhà cung cấp
        /// </summary>
        public int? IsSameSupplierAddress { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string? Note { get; set; }

    }
}
