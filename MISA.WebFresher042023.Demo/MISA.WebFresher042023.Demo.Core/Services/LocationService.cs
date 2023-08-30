using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Location;
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
    /// <summary>
    /// class thực thu location service
    /// </summary>
    /// created by: vdtien (18/7/2023)
    public class LocationService : BaseService<Location, LocationDTO, LocationCreateDTO, LocationUpdateDTO>, ILocationService
    {
        #region Field
        private readonly ILocationRepository _locationRepository;
        #endregion

        #region Constructor
        public LocationService(ILocationRepository locationRepository, IMapper mapper, IUnitOfWork uow) : base(locationRepository, mapper, uow)
        {
            _locationRepository = locationRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lấy danh sách vị trí địa lý theo id cha
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public async Task<List<Location>> GetAllLocationByParentId(Guid? parentId)
        {
            var locations = await _locationRepository.GetAllLocationByParentId(parentId);
            var locationsDTO = _mapper.Map<List<Location>>(locations).ToList();
            return locationsDTO;
        } 
        #endregion
    }
}
