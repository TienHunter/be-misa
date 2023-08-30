using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Services
{
    /// <summary>
    /// interface employeeService
    /// </summary>
    /// Created by: vdtien (19/6/2023)
    public interface IEmployeeService:IBaseService<EmployeeDTO,EmployeeCreateDTO,EmployeeUpdateDTO>
    {
        /// <summary>
        /// Lay ma code moi nhat
        /// </summary>
        /// <returns>ma code</returns>
        /// Created by: vdtien (19/06/2023)
        public Task<string> GetNewEmployeeCodeSerivceAsync();

        /// <summary>
        /// xuat file excel danh sach nhan vien
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// Created by: vdtien (27/6/2023)
        public Task<byte[]> ExportEmployeesToExcel(string keySearch);


    }
}
