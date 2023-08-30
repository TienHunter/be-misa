using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTO,Employee>();
            //CreateMap<EmployeeDTO,EmployeeExcelDTO>().ReverseMap();
            CreateMap<Employee, EmployeeExcelDTO>();
        }
    }
}
