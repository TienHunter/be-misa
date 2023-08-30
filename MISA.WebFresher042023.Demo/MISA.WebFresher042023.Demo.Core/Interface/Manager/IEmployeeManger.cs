using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Manager
{
    /// <summary>
    /// interface employee manger validate nghiep vu
    /// </summary>
    public interface IEmployeeManger
    {
        /// <summary>
        /// kiem tra trung ma nhan vien
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task IsDupEmployeeCode(string code, Guid? id);

        /// <summary>
        /// check tồn tại nhân viên theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<Employee> CheckEmployeeExsitByIdAsync(Guid employeeId);
    }
}
