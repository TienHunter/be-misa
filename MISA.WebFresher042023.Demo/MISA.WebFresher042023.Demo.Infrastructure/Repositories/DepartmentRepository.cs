using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork uow) : base(uow)
        {
        }

    }
}
