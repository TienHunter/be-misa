using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Entity
{
    /// <summary>
    /// class location
    /// </summary>
    public class Location: BaseEntity
    {
        /// <summary>
        /// id vị trí
        /// </summary>
        public Guid LocationId { get; set; }

        /// <summary>
        /// mã vị trí
        /// </summary>
        public string LocationCode { get; set; }

        /// <summary>
        /// tên vị trí
        /// </summary>
        public string LocationName { get; set;}

        /// <summary>
        /// id vị trí cha
        /// </summary>
        public Guid? ParentId { get; set; }

    }
}
