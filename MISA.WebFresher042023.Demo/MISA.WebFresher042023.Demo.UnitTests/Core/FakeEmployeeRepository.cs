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
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        public Guid ActualId;
        public Task<int> DeleteAsync(Guid recordId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMulti(string listId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> GetAsync(Guid recordId)
        {
            ActualId = id;
            return Task.FromResult(new Employee()
            {
                EmployeeId = id,
            });
        }

        public Task<ListRecords<Employee>?> GetListAsync(int pageSize, int pageNumber, string keySearch)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNewEmployeeCodeRepositoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> InsertAsync(Employee record)
        {
            throw new NotImplementedException();
        }

        public Task<string?> isDupEmployeecodeRepositoryAsync(string employeeCode, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> UpdateAsync(Employee record)
        {
            throw new NotImplementedException();
        }
    }
}
