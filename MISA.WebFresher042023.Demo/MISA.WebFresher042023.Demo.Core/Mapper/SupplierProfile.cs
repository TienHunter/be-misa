using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Account;
using MISA.WebFresher042023.Demo.Common.DTO.Supplier;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MISA.WebFresher042023.Demo.Common.DTO.GroundSupplier;
using MISA.WebFresher042023.Demo.Common.DTO;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {

            CreateMap<Supplier, SupplierDTO>()
                .ForMember(dest => dest.GroupSuppliersId, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<Guid>?>(src.GroupSuppliersId)))
                .ForMember(dest => dest.BanksAccount, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<BankAccount>?>(src.BanksAccount)))
                .ForMember(dest => dest.DeliverAddress, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<string>?>(src.DeliverAddress)))
                .ForMember(dest => dest.ContractInfor, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<ContractInfor?>(src.ContractInfor)));
            CreateMap<SupplierDTO, Supplier>()
                .ForMember(dest => dest.GroupSuppliersId, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.GroupSuppliersId)))
                .ForMember(dest => dest.BanksAccount, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.BanksAccount)))
                .ForMember(dest => dest.DeliverAddress, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.DeliverAddress)))
                .ForMember(dest => dest.ContractInfor, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.ContractInfor)));


            CreateMap<SupplierCreateDTO, Supplier>()
                                .ForMember(dest => dest.BanksAccount, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.BanksAccount)))
                                .ForMember(dest => dest.DeliverAddress, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.DeliverAddress)))
                                .ForMember(dest => dest.GroupSuppliersId, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.GroupSuppliersId)))
                                .ForMember(dest => dest.ContractInfor, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.ContractInfor)));

            CreateMap<SupplierUpdateDTO, Supplier>()
                   .ForMember(dest => dest.BanksAccount, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.BanksAccount)))
                                .ForMember(dest => dest.DeliverAddress, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.DeliverAddress)))
                                .ForMember(dest => dest.GroupSuppliersId, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.GroupSuppliersId)))
                                .ForMember(dest => dest.ContractInfor, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.ContractInfor)));

            CreateMap<Supplier, SupplierExcelDTO>();

        }
    }
}
