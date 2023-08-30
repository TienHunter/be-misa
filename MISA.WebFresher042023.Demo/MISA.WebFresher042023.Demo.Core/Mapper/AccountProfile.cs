using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Account;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class AccountProfile:Profile
    {
        public AccountProfile() {
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<AccountCreatedDTO, Account>();
            CreateMap<AccountUpdateDTO, Account>();
            CreateMap<Account, AccountExcelDTO>();
        }
    }
}
