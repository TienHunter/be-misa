using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Location;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class LocationProfile:Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<LocationCreateDTO, Location>();
            CreateMap<LocationUpdateDTO, Location>();
        }
    }
}
