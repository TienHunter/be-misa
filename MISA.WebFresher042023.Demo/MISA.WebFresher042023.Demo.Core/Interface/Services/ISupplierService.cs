using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Services
{
    public interface ISupplierService : IBaseService<SupplierDTO, SupplierCreateDTO, SupplierUpdateDTO>
    {

        /// <summary>
        /// xuất file excel danh sach nhà cung cấp theo từ khóa
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: vdtien (27/7/2023)
        public Task<byte[]> ExportSuppliersToExcel(string keySearch);

        /// <summary>
        /// xóa nhiều nhà cung cấp theo danh sách id
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        ///  created by: vdtien (27/7/2023)
        public Task<ResultBulkHandleRecord<SupplierDTO>> DeleteMultiSupplierAsync(List<Guid> listId);
    }
}
