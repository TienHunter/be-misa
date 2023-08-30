using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Location
{
    /// <summary>
    /// class location create dto
    /// </summary>
    public class LocationCreateDTO
    {
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
