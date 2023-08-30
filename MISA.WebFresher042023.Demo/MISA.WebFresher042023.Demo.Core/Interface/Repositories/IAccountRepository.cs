using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repositories
{
    /// <summary>
    /// interface account repository
    /// </summary>
    /// created by: vdtien (18/7/2023)
    public interface IAccountRepository : IBaseRepository<Account>
    {
        /// <summary>
        /// lay danh sach tai khoan theo id tai khoan cha
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public Task<List<Account>> GetListAccountByParentId(Guid parentId);

        /// <summary>
        /// Lay danh sach tai khoan con theo danh sach tai khoan cha
        /// </summary>
        /// <param name="parentList"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public Task<List<Account>> GetListAccountByListParentId(string listParentId);

        /// <summary>
        /// lay danh sach ban ghi phan cap cay theo paging va filter
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (18/7/2023)
        public Task<ListRecords<Account>?> GetListTreeAsync(int limit, int offset, string keySearch);

        /// <summary>
        /// cap nhat trang thai cua tai khoan
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public Task<int> UpdateStatusAsync(string keyCode, Status status, TypeUpdate type);

        /// <summary>
        /// lay danh sach tai khoan theo account feature 
        /// </summary>
        /// <param name="accountFeatures"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public Task<List<Account>> GetAllQueryAsync(List<AccountFeature>? accountFeatures, List<UserObject>? userObjects);

        /// <summary>
        /// láy mã tài khoản đầu tiên trong danh sách id nhưng không trong danh sách đối tượng sử dụng
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userObjects"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public Task<List<string>> GetListAccountCodeInIdsButNotInUserObjects(List<Guid>? ids, List<UserObject> userObjects);

        /// <summary>
        /// láy mã tài khoản đầu tiên trong danh sách id và trong danh sách đối tượng sử dụng
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userObjects"></param>
        /// <returns></returns>
        /// created by: vdtien (18/7/2023)
        public Task<List<string>> GetListAccountCodeInIdsAndInUserObjects(List<Guid>? ids, List<UserObject> userObjects);

    }
}
