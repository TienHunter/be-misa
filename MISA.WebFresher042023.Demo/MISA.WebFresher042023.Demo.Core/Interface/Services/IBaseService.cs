using MISA.WebFresher042023.Demo.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Services
{
    /// <summary>
    /// interface base service
    /// </summary>
    /// Created by: vdtien (19/6/2023)
    public interface IBaseService<TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO>
    {
        /// <summary>
        /// lay danh sach tat ca ban ghi
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public Task<List<TEntityDTO>> GetAllAsync();

        /// <summary>
        /// lay danh sach ban ghi theo filter
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (27/6/2023)
        public Task<List<TEntityDTO>> GetListByFilterAsync(string keySearch);

        /// <summary>
        /// lay 1 ban ghi theo id
        /// </summary>
        /// <param name="recordId">id ban ghi</param>
        /// <returns>ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public Task<TEntityDTO?> GetAsync(Guid? recordId);

        /// <summary>
        /// lay danh sach ban ghi theo paging va filter
        /// </summary>
        /// <param name="pageSize">so ban ghi tren trang</param>
        /// <param name="pageNumber">trang hien tai</param>
        /// <param name="keySearch">tu khoa tim kiem</param>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public Task<ListRecords<TEntityDTO>> GetListAsync(int pageSize, int pageNumber, string keySearch);



        /// <summary>
        /// them moi ban ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ban ghi vua them</returns>
        /// Created by: vdtien (19/6/2023)
        public Task<TEntityDTO?> InsertAsync(TEntityCreateDTO record);

        /// <summary>
        /// cap nhat ban ghi theo id
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="record"></param>
        /// <returns>ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public Task<TEntityDTO?> UpdateAsync(Guid recordId, TEntityUpdateDTO record);

        /// <summary>
        /// Xoa ban ghi theo id
        /// </summary>
        /// <param name="recordId">id ban ghi</param>
        /// <returns>so ban ghi bi anh huong</returns>
        /// Created by: vdtien (19/6/2023)
        public Task<int> DeleteAsync(Guid recordId);

        /// <summary>
        /// xoa nhieu ban ghi
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>so ban ghi bi anh huong</returns>
        /// Created by: vdtien (20/6/2023)
        public Task<int> DeleteMultiAsync(List<Guid> listId);

        /// <summary>
        /// tao ma moi
        /// </summary>
        /// <returns>ma moi</returns>
        /// created by: vdtien (28/7/2023)
        public Task<string> GetNewCode();
    }
}
