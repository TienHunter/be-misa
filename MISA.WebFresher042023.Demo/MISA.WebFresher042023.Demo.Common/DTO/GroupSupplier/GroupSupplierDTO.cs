using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.GroundSupplier
{
    /// <summary>
    /// class group supplier dto
    /// </summary>
    public class GroupSupplierDTO
    {
        /// <summary>
        /// id nhóm nhà cung cấp
        /// </summary>
        public Guid GroupSupplierId { get; set; }

        /// <summary>
        /// mã nhóm nhà cung cấp
        /// </summary>
        public string? GroupSupplierCode { get; set; }

        /// <summary>
        /// tên nhóm nhà cung cấp
        /// </summary>
        public string? GroupSupplierName { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? Explain { get; set; }

        /// <summary>
        /// trạng thái hoạt động
        /// </summary>
        public Status? Active { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
