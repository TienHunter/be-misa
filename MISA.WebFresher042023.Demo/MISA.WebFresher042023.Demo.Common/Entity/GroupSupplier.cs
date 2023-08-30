using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Entity
{
    /// <summary>
    /// class group supplier 
    /// </summary>
    public class GroupSupplier : BaseEntity
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
        /// tên nhón nhà cung cấp
        /// </summary>
        public string? GroupSupplierName { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? Explain { get; set; }

        /// <summary>
        /// trạng thái
        /// </summary>
        public Status? Active { get; set; }
    }
}
