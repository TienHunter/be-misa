using Dapper;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    public class UserRepository :BaseRepository<User>,  IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public override async Task<int> InsertAsync(User user)
        {
            var sql = "INSERT INTO user  ( ";
            var properties = typeof(User).GetProperties();
            sql += string.Join(" , ", properties.Select(p => $"{p.Name}"));
            sql += ") VALUES (";
            sql += string.Join(" , ", properties.Select(p => $"@{p.Name}"));
            sql += ");";

            var res = await _uow.Connection.ExecuteAsync(sql, user, transaction: _uow.Transaction);
            if (res == 0) throw new Exception("insert failure");
            return res;
        }

        public async Task<User> GetUserByUsernameAsync(string userName)
        {
            var sql = "SELECT * FROM user WHERE UserName = @userName ;";
            var res = await _uow.Connection.QueryFirstOrDefaultAsync<User>(sql, new {userName});
            return res;
        }
    }
}
