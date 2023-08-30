using MISA.WebFresher042023.Demo.Common.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Services
{
    public interface IDepartmentService:IBaseService<DepartmentDTO,DepartmentCreateDTO,DepartmentUpdateDTO>
    {
    }
}
