using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    /// <summary>
    /// class thực thi SupplierRepository
    /// </summary>
    /// created by: vdtien (27/7/2023)
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        #region Field
        public SupplierRepository(IUnitOfWork uow) : base(uow)
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// kiểm tra xem có supplier nào dùng tài khoản có id này không
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (27/7/2023)
        public Task<Guid> CheckSupplierByAccountId(Guid accountId)
        {
            var sql = "SELECT SupplierId From supplier WHERE AccountReceivableId = @accountId OR AccountPayableId = @accountId LIMIT 1";
            var result = _uow.Connection.QueryFirstOrDefaultAsync<Guid>(sql, new { accountId }, transaction: _uow.Transaction);
            return result;
        } 
        #endregion
    }
}
