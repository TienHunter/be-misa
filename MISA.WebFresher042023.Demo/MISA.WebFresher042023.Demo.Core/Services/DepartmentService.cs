using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Department;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Services
{
    public class DepartmentService : BaseService<Department,DepartmentDTO,DepartmentCreateDTO,DepartmentUpdateDTO>, IDepartmentService
    {

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IUnitOfWork uow) : base(departmentRepository, mapper, uow)
        {
   
        }

    }
}
