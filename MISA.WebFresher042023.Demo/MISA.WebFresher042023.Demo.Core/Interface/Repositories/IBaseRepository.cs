using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repositories
{
    /// <summary>
    /// interface BaseEntity repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: vetien (19/6/2023)
    public interface IBaseRepository<TEntity>
    {

        /// <summary>
        /// lay thong tin ban ghi theo id
        /// </summary>
        /// <param name="recordId">id ban ghi</param>
        /// <returns>nhan vien</returns>
        /// Created by: vdtien (18/6/2023)
        public Task<TEntity?> GetAsync(Guid? recordId);

        /// <summary>
        /// lay thong tin tat ca ban ghi
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (18/6/2023)
        public Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// lay danh sach ban ghi theo paging va filter
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (18/6/2023)
        public Task<ListRecords<TEntity>> GetListAsync(int limit, int offset, string keySearch);

        /// <summary>
        /// lay danh sach ban ghi theo filter
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (27/6/2023)
        public Task<List<TEntity>> GetListByFilterAsync(string keySearch);


        /// <summary>
        /// lay danh sach ban ghi theo danh sach id
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetListByListIdAsync(List<Guid> listId);


        /// <summary>
        /// them moi 1 ban ghi
        /// </summary>
        /// <returns>ban ghi</returns>
        /// Created by: vdtien (18/6/2023)
        public Task<TEntity?> InsertAsync(TEntity record);

        /// <summary>
        /// cap nhat 1 ban ghi
        /// </summary>
        /// <returns>ban ghi</returns>
        /// Created by: vdtien (18/6/2023)
        public Task<TEntity?> UpdateAsync(TEntity record);

        /// <summary>
        /// xoa 1 ban ghi
        /// </summary>
        /// <returns>so ban ghi bi anh huong</returns>
        /// Created by: vdtien (18/6/2023)
        public Task<int> DeleteAsync(Guid recordId);

        /// <summary>
        /// xoa nhieu ban ghi
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>so ban ghi bi anh huong</returns>
        /// Created by: vdtien (20/6/2023)
        public Task<int> DeleteMultiAsync(List<Guid> listId);

        /// <summary>
        /// Kiem tra ma trung
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns>ma trung hoac null</returns>
        /// Created by: vdtien (18/6/2023)
        public Task<string?> IsDupCodeAsync(string code, Guid? id);

        /// <summary>
        /// tao ma moi
        /// </summary>
        /// <returns>ma moi</returns>
        /// created by: vdtien (28/7/2023)
        public Task<string> GetNewCode();

        /// <summary>
        /// insert nhiều bản ghi
        /// </summary>
        /// <param name="listEmtity"></param>
        /// <returns></returns>
        /// created by: vdtien (1/8/2023)
        public Task BulkInsertAsync(List<TEntity> listEmtity);

        /// <summary>
        /// update nhiều bản ghi
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        /// created by: vdtien (1/8/2023)
        public Task BulkUpdateAsync(List<TEntity> listEntity);
    }
}
