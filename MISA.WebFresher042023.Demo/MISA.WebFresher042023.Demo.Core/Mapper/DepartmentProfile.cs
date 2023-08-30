using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Department;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<DepartmentCreateDTO, Department>();
            CreateMap<DepartmentUpdateDTO, Department>();
        }
    }
}
