using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repositories
{
    /// <summary>
    /// interface supplier repository
    /// </summary>
    /// created by: vdtien (25/7/2023)
    public interface ISupplierRepository:IBaseRepository<Supplier>
    {
        /// <summary>
        /// kiểm tra xem có supplier nào dùng id tài khoan nay không 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// created by: vdtien (25/7/2023)
        public Task<Guid> CheckSupplierByAccountId(Guid accountId);
    }
}
