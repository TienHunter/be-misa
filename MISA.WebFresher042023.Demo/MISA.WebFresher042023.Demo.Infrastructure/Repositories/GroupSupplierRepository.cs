using Microsoft.Extensions.Configuration;
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
    public class GroupSupplierRepository : BaseRepository<GroupSupplier>, IGroupSupplierRepository
    {
        /// <summary>
        /// class thực thi groupSupplier repository
        /// </summary>
        /// <param name="uow"></param>
        /// created by: vdtien (27/7/2023)
        public GroupSupplierRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
