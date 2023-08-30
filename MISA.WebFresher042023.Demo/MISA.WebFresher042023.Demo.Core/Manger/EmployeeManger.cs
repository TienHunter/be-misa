using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Common.Resources;
using MISA.WebFresher042023.Demo.Core.Interface.Manager;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manger
{
    /// <summary>
    /// validate nghiep vu cua nhan vien
    /// </summary>
    public class EmployeeManger : IEmployeeManger
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManger(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// check tồn tại nhân viên theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Employee> CheckEmployeeExsitByIdAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetAsync(employeeId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundEmployee);

            return employee;
        }

        public async Task IsDupEmployeeCode(string code, Guid? id)
        {
            // check dup employeecode
            var dupCode = await _employeeRepository.IsDupCodeAsync(code, id);
            if (dupCode != null)
            {
                var errMsg = String.Format(ResourceVN.UserMsg_DupEmployeeCode, dupCode);
                var errsMsgs = new List<string>();
                errsMsgs.Add(errMsg);
                var errsMore = new Dictionary<string, List<string>>
                {
                    { "EmployeeCode", errsMsgs }
                };
                throw new DupCodeException(errsMsgs, errsMore);
            }
        }

    }
}
