using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Location
{
    /// <summary>
    /// class location dto
    /// </summary>
    public class LocationDTO
    {
        /// <summary>
        /// id vị trí địa lý
        /// </summary>
        public Guid LocationId { get; set; }

        /// <summary>
        /// mã vị trí địa lý
        /// </summary>
        public string? LocationCode { get; set; }

        /// <summary>
        /// tên vị trí địa lý
        /// </summary>
        public string? LocationName { get; set;}

        /// <summary>
        /// id vị trí cha
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
