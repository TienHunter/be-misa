using MISA.WebFresher042023.Demo.Common.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Services
{
    public interface IUserService : IBaseService<UserDTO, UserCreateDTO, UserUpdateDTO>
    {
        public Task<AuthResponse> LoginAsync(AuthRequest request);

        public Task<int> RegisterAsync(UserCreateDTO userCreateDTO);

    }
}
