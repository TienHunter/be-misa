using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    /// <summary>
    /// account repository
    /// </summary>
    /// created by: vdtien (18/7/2023)
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        #region Constructor
        public AccountRepository(IUnitOfWork uow) : base(uow)
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// lay danh sach tai khoan theo id tai khoan cha
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public async Task<List<Account>> GetListAccountByParentId(Guid parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@v_ParentId", parentId);


            // thuc hien ket noi database


            var results = await _uow.Connection.QueryAsync<Account>("Proc_Account_GetListByParentId", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return (List<Account>)results;

        }

        /// <summary>
        /// lay danh sach tài khoản con theo danh sách tài khoản cha
        /// </summary>
        /// <param name="parentList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (18/7/2023)
        public async Task<List<Account>> GetListAccountByListParentId(string listParentId)
        {
            // chuan bi tham so 
            var parameters = new DynamicParameters();
            parameters.Add("@v_ListParentId", listParentId, DbType.String); // Sử dụng DbType.String cho kiểu text 

            // Khởi tạo kết nối tới DB MariaDB


            var result = await _uow.Connection.QueryAsync<Account>("Proc_Account_GetAllChildrenByListParentId", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (18/7/2023)
        public async Task<ListRecords<Account>?> GetListTreeAsync(int limit, int offset, string keySearch)
        {
            // chuan bi tham so 
            var parameters = new DynamicParameters();
            parameters.Add("@v_Limit", limit);
            parameters.Add("@v_Offset", offset);
            parameters.Add("@v_KeySearch", keySearch);
            // Khởi tạo kết nối tới DB MariaDB

            var results = await _uow.Connection.QueryMultipleAsync("Proc_Account_GetListTree", parameters, commandType: System.Data.CommandType.StoredProcedure);

            var totalRecords = results.Read<int>().FirstOrDefault();
            var totalRoots = results.Read<int>().FirstOrDefault();
            var records = results.Read<Account>().ToList();
            return new ListRecords<Account>()
            {
                TotalRecord = totalRecords,
                TotalRoot = totalRoots,
                Data = records
            };

        }

        /// <summary>
        /// cập nhật trạng thái của tài khoản
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public async Task<int> UpdateStatusAsync(string keyCode, Status status, TypeUpdate type)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@v_KeyCode", keyCode, DbType.String);
            parameters.Add("@v_Status", status);
            parameters.Add("@v_Type", type);

            // Khởi tạo kết nối tới DB MariaDB
            var result = await _uow.Connection.ExecuteAsync("Proc_Account_UpdateStatus", parameters, commandType: CommandType.StoredProcedure);

            return result;


        }

        /// <summary>
        /// lay danh sach tai khoan theo account feature 
        /// </summary>
        /// <param name="accountFeatures"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public async Task<List<Account>> GetAllQueryAsync(List<AccountFeature>? accountFeatures, List<UserObject>? userObjects)
        {

            // Khởi tạo kết nối tới DB MariaDB

            var sql = "SELECT * FROM account ";
            if (accountFeatures != null && accountFeatures.Count > 0 && userObjects != null && userObjects.Count > 0)
            {
                sql += "WHERE AccountFeature IN @accountFeatures AND UserObject IN @userObjects ";
            }
            else if (accountFeatures != null && accountFeatures.Count > 0)
            {
                sql += "WHERE AccountFeature IN @accountFeatures ";
            }
            else if (userObjects != null && userObjects.Count > 0)
            {
                sql += "WHERE UserObject IN @userObjects ";
            }
            sql += " ORDER BY KeyCode; ";
            var results = await _uow.Connection.QueryAsync<Account>(sql, new { accountFeatures, userObjects });


            return results.ToList();


        }

        /// <summary>
        /// láy mã tài khoản đầu tiên trong danh sách id nhưng không trong danh sách đối tượng sử dụng
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userObjects"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public async Task<List<string>> GetListAccountCodeInIdsButNotInUserObjects(List<Guid>? ids, List<UserObject> userObjects)
        {
            var sql = "SELECT AccountCode FROM account WHERE AccountId IN @Ids AND UserObject NOT IN @UserObjects";

            var res = await _uow.Connection.QueryAsync<string>(sql, new { Ids = ids, UserObjects = userObjects }, transaction: _uow.Transaction);
            return res.ToList();
        }

        /// <summary>
        /// láy mã tài khoản đầu tiên trong danh sách id và trong danh sách đối tượng sử dụng
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userObjects"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public async Task<List<string>> GetListAccountCodeInIdsAndInUserObjects(List<Guid>? ids, List<UserObject> userObjects)
        {
            var sql = "SELECT AccountCode FROM account WHERE AccountId IN @Ids AND UserObject IN @UserObjects";

            var res = await _uow.Connection.QueryAsync<string>(sql, new { Ids = ids, UserObjects = userObjects }, transaction: _uow.Transaction);
            return res.ToList();
        }


    }
    #endregion
}

