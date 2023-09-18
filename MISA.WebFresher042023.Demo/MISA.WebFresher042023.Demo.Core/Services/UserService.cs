using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Common.DTO.User;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Core.Interface.Identities;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Services
{
    public class UserService : BaseService<User, UserDTO, UserCreateDTO, UserUpdateDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtIdentity _jwtIdentity;
   
        public UserService(IJwtIdentity jwtIdentity,IUserRepository userRepository, IMapper mapper, IUnitOfWork uow) : base(userRepository, mapper, uow)
        {
            _userRepository = userRepository;
            _jwtIdentity = jwtIdentity;
           
        }

        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            // kiểm tra username
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            var passwordHasher = new PasswordHasher<User>();
            if (user == null || passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) != PasswordVerificationResult.Success)
            {
                throw new BadRequestException("Tài khoản hoặc mật khẩu không đúng .");
            }
            var token = _jwtIdentity.GenerateJwtToken(request.Username);
            var userDTO = _mapper.Map<UserDTO>(user);

            return new AuthResponse
            {
                User = userDTO,
                Token = token
            };
        }

        public async Task<int> RegisterAsync(UserCreateDTO userCreateDTO)
        {
            // check user exist 
            var userExsit = await _userRepository.GetUserByUsernameAsync(userCreateDTO.Username);
            if (userExsit != null) throw new BadRequestException("user exsit, Plz try user other .");

            // hash password 
            var user = _mapper.Map<User>(userCreateDTO);
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user,user.Password); // Assuming PasswordHasher is a class

            // create new user 
            user.UserId = Guid.NewGuid();
            user.CreatedDate = DateTime.Now;

           var res =  await _userRepository.InsertAsync(user);
            return res;
        }

    }
}
