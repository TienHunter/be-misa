using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.DTO.User;
using MISA.WebFresher042023.Demo.Core.Interface.Services;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] UserCreateDTO userCreateDTO)
        {
            var res = await _userService.RegisterAsync(userCreateDTO);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest request)
        {
            var res = await _userService.LoginAsync(request);
            return StatusCode(StatusCodes.Status200OK, res);
        }
    }
}
