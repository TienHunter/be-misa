using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.GroundSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.GroupSupplier;
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
    public class GroupSupplierService : BaseService<GroupSupplier, GroupSupplierDTO, GroupSupplierCreateDTO, GroupSupplierUpdateDTO>, IGroupSupplierService
    {
        IGroupSupplierRepository _groupSupplierRepository;
        public GroupSupplierService(IGroupSupplierRepository groupSupplierRepository, IMapper mapper, IUnitOfWork uow) : base(groupSupplierRepository, mapper, uow)
        {
            _groupSupplierRepository = groupSupplierRepository;
        }
    }
}
