using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.TermPayment;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class TermPaymentProfile:Profile
    {
        public TermPaymentProfile()
        {
            CreateMap<TermPayment, TermPaymentDTO>().ReverseMap();
            CreateMap<TermPaymentCreateDTO, TermPayment>();
            CreateMap<TermPaymentUpdateDTO, TermPayment>();
        }
    }
}
