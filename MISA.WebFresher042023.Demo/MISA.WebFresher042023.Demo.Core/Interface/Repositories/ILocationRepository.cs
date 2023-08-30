using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repositories
{
    /// <summary>
    /// interface location reposiotry
    /// </summary>
    /// created by: vdtien (26/7/2023)
    public interface ILocationRepository : IBaseRepository<Location>
    {
        /// <summary>
        /// láy ra danh sách vị trí con từ id vị trí cha
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// created by: vdtien (26/7/2023)
        public Task<List<Location>?> GetAllLocationByParentId(Guid? parentId);
    }
}
