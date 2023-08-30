using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.DTO.GroundSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.GroupSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.Location;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Services;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : BasesController<LocationDTO, LocationCreateDTO, LocationUpdateDTO>
    {
        #region Field
        private readonly ILocationService _locationService;
        #endregion

        #region Constructor
        public LocationsController(ILocationService locationService) : base(locationService)
        {
            _locationService = locationService;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// lấy ra vị trí cấp dưới của vị trí hiện tại
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet("childrens")]
        public async Task<IActionResult> GetAllLocationByParentId([FromQuery] Guid? parentId)
        {
            var locations = await _locationService.GetAllLocationByParentId(parentId);
            return StatusCode(StatusCodes.Status200OK, locations);
        }
        #endregion
    }
}
