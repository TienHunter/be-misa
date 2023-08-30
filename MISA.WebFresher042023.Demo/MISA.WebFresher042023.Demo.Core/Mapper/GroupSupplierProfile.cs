using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.GroundSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.GroupSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.Supplier;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class GroupSupplierProfile : Profile
    {
        public GroupSupplierProfile()
        {
            CreateMap<GroupSupplier, GroupSupplierDTO>().ReverseMap();
            CreateMap<GroupSupplierCreateDTO, GroupSupplier>();
            CreateMap<GroupSupplierUpdateDTO, GroupSupplier>();
        }
    }
}
