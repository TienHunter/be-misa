using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repositories
{
    /// <summary>
    /// interface supplier_groupSupplier repo
    /// </summary>
    /// created by: 25/08/2023
    public interface ISupplier_GroupSupplierRepository : IBaseRepository<Supplier_GroupSupplier>
    {
        /// <summary>
        /// xóa quan hệ 2 bảng theo id nhà cung cấp
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// created by: 25/08/2023
        public Task<int> DeleteBySupplierIdAsync(Guid supplierId);

        /// <summary>
        /// xóa quan hệ theo danh sách id nhà cung cấp
        /// </summary>
        /// <param name="listSupplierId"></param>
        /// <returns></returns>
        /// created by: 25/08/2023
        public Task DeleteByListSupplierIdAsync(List<Guid> listSupplierId);

        /// <summary>
        /// thêm hoặc cập nhật quan hệ nếu trùng key
        /// </summary>
        /// <param name="listSupplierGroupSupplier"></param>
        /// <returns></returns>
        public  Task InsertIgnoreAsync(List<Supplier_GroupSupplier> listSupplierGroupSupplier);

        /// <summary>
        /// xóa quan hệ theo danh sách id nhà cung cấp nhưng khác id nhóm nhà cung cấp
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="groupSupplierId"></param>
        /// <returns></returns>
        /// created by: 25/08/2023
        public Task DeleteBySupplierIdDifferentGroupSupplierId( Guid supplierId, List<Guid>? groupSupplierId);
    }
}
