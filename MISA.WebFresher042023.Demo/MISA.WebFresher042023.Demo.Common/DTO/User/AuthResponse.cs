using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.User
{
    public class AuthResponse
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
