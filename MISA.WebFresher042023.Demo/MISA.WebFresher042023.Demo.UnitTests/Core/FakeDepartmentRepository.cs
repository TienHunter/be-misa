using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.UnitTests.Core
{
    public class FakeDepartmentRepository : IDepartmentRepository
    {
        public Task<int> DeleteAsync(Guid recordId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMulti(string listId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department?> GetAsync(Guid recordId)
        {
            throw new NotImplementedException();
        }

        public Task<ListRecords<Department>?> GetListAsync(int pageSize, int pageNumber, string keySearch)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> InsertAsync(Department record)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> UpdateAsync(Department record)
        {
            throw new NotImplementedException();
        }
    }
}
