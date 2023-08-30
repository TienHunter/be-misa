using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Entity
{
    /// <summary>
    /// class Supplier - Group supplier
    /// </summary>
    public class Supplier_GroupSupplier
    {
        /// <summary>
        /// id nhà cung cấp
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// id nhóm nhà cung cấp
        /// </summary>
        public Guid GroupSupplierId { get; set; }
    }
}
