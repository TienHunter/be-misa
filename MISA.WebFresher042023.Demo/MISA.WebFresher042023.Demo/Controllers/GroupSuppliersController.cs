using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.DTO.GroundSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.GroupSupplier;
using MISA.WebFresher042023.Demo.Core.Interface.Services;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GroupSuppliersController : BasesController<GroupSupplierDTO, GroupSupplierCreateDTO, GroupSupplierUpdateDTO>
    {
        #region Field
        private readonly IGroupSupplierService _groupSupplierService;
        #endregion

        #region Constructor
        public GroupSuppliersController(IGroupSupplierService groupSupplierService) : base(groupSupplierService)
        {
            _groupSupplierService = groupSupplierService;
        } 
        #endregion
    }
}
