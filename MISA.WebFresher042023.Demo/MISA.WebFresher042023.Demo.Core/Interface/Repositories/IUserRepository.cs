using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
        /// <summary>
        /// lấy user theo username
        /// </summary>
        /// <returns></returns>
        Task<User> GetUserByUsernameAsync(string userName);

    }
}
