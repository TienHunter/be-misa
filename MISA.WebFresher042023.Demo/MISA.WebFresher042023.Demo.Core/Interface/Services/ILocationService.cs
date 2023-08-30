using MISA.WebFresher042023.Demo.Common.DTO.Location;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Services
{
    /// <summary>
    /// interface location service
    /// </summary>
    /// created by: vdtien (25/7/2023)
    public interface ILocationService:IBaseService<LocationDTO,LocationCreateDTO,LocationUpdateDTO>
    {
        /// <summary>
        /// lây danh sách vị trị địa lý theo id parent
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// created by: vdtien (25/7/2023)
        public Task<List<Location>> GetAllLocationByParentId(Guid? parentId);
    }
}
