using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using MISA.WebFresher042023.Demo.Common.DTO.Supplier;
using MISA.WebFresher042023.Demo.Common.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();
            CreateMap<PaymentCreateDTO, Payment>();
            CreateMap<PaymentUpdateDTO, Payment>();

        }
    }
}
