using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Acccounting;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class AccountingProfile : Profile
    {
        public AccountingProfile()
        {
            CreateMap<Accounting, AccountingDTO>().ReverseMap();
            CreateMap<AccountingCreateDTO, Accounting>();
            CreateMap<AccountingUpdateDTO, Accounting>();
        }
    }
}
