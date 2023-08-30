using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Location
{
    /// <summary>
    /// class location update dto
    /// </summary>
    public class LocationUpdateDTO
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
        public string? LocationName { get; set; }

        /// <summary>
        /// id vị trí địa lý cha
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
