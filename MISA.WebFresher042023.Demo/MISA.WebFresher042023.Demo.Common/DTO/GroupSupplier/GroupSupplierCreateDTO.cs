using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.GroupSupplier
{
    /// <summary>
    /// class group supplier create dto
    /// </summary>
    public class GroupSupplierCreateDTO
    {
        /// <summary>
        /// mã code nhóm nhà cung cấp
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
