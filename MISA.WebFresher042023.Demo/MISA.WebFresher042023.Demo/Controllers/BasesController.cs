using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BasesController<TEnityDTO, TEntityCreateDTO, TEntityUpdateDTO> : ControllerBase
    {
        #region Field
        protected readonly IBaseService<TEnityDTO, TEntityCreateDTO, TEntityUpdateDTO> _baseService;
        #endregion

        #region Constructor
        public BasesController(IBaseService<TEnityDTO, TEntityCreateDTO, TEntityUpdateDTO> baseService)
        {

            _baseService = baseService;
        } 
        #endregion

        /// <summary>
        /// tra ve danh sach tat ca ban ghi
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            var enityDTOs = await _baseService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, enityDTOs);
        }


        /// <summary>
        /// tra ve thong tin ban ghi theo id
        /// </summary>
        /// <returns>thong tin ban ghi</returns>
        /// Created by: vdtien( 12/6/2023)
        /// Modified by: vdtien (19/6/2023)
        [HttpGet("{recordId}")]
        public virtual async Task<IActionResult> GetByIdAsync([FromRoute] [Required] Guid recordId)
        {

            var enityDTO = await _baseService.GetAsync(recordId);
            return StatusCode(StatusCodes.Status200OK, enityDTO);

        }

        /// <summary>
        /// lay danh sach ban ghi theo paging va filter
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="keySearch"></param>
        /// <returns>TotalRecord: tong so ban ghi; Data: danh sach ban ghi</returns>
        [HttpGet("filter")]
        public virtual async Task<IActionResult> GetListAsync([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] string? keySearch = "")
        {
            var results = await _baseService.GetListAsync(pageSize, pageNumber, keySearch ?? "");
            return StatusCode(StatusCodes.Status200OK, results);
        }
        /// <summary>
        /// them moi ban ghi
        /// </summary>
        /// <returns>thong tin ban ghi</returns>
        /// Created by: vdtien(12/6/2023)
        [HttpPost]
        public virtual async Task<IActionResult> InsertAsync([FromBody] TEntityCreateDTO entityCreateDTO)
        {

            var result = await _baseService.InsertAsync(entityCreateDTO);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// cap nhat ban ghi
        /// </summary>
        /// <returns>ban ghi cap nhat</returns>
        /// Created by: vdtien(12/6/2023)
        [HttpPut("{recordId}")]
        public virtual async Task<IActionResult> UpdateAsync([FromRoute] [Required] Guid recordId, [FromBody] TEntityUpdateDTO entityUpdateDTO)
        {

            var result = await _baseService.UpdateAsync(recordId,entityUpdateDTO);
            return StatusCode(StatusCodes.Status200OK, result);

        }

        /// <summary>
        /// Xoa ban ghi
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>1: thanh cong, 0: that bai</returns>
        /// Created by: vdtien (13/6/2023)
        [HttpDelete("{recordId}")]
        public virtual async Task<IActionResult> DeleteAsync([FromRoute] [Required] Guid recordId)
        {
            var result = await _baseService.DeleteAsync(recordId);
            return StatusCode(StatusCodes.Status200OK, result);
        }


        /// <summary>
        /// Xoa nhieu ban ghi
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>so ban ghi bi anh huong</returns>
        /// Created by: vdtien (20/6/2023)
        [HttpPost("delete-multi")]
        public virtual async Task<IActionResult> DeleteMultiAsync([FromBody] List<Guid> listId)
        {
            var results = await _baseService.DeleteMultiAsync(listId);
            return StatusCode(StatusCodes.Status200OK, results);
        }


        /// <summary>
        /// tao ma moi
        /// </summary>
        /// <returns>ma moi</returns>
        /// created by: vdtien (28/7/2023)
        [HttpGet("new-code")]
        public virtual async Task<IActionResult> GetNewCodeAsync()
        {

            var newCode = await _baseService.GetNewCode();
            return StatusCode(StatusCodes.Status200OK, newCode);

        }
    }
}
