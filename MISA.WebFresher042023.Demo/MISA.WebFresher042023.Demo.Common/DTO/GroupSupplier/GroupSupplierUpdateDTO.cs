using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.GroupSupplier
{
    /// <summary>
    /// class group supplier update dto
    /// </summary>
    public class GroupSupplierUpdateDTO
    {
        /// <summary>
        /// id nhóm nhà cungg cấp
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
    }
}
