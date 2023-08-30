using Dapper;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    /// <summary>
    /// class thực thi LocationRepository
    /// </summary>
    /// created by: vdtien (27/7/2023)
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {

        #region Constructor
        public LocationRepository(IUnitOfWork uow) : base(uow)
        {

        }
        #endregion


        #region MyRegion
        /// <summary>
        /// lấy danh sách vị trí địa lý theo id cha
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<Location>?> GetAllLocationByParentId(Guid? parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@v_ParentId", parentId);
            var results = await _uow.Connection.QueryAsync<Location>("Proc_Location_GetAllByParentId", parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
            return results.ToList();
        } 
        #endregion
    }
}
