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
    /// interface accounting repository
    /// </summary>
    /// created by: vdtien (13/8/2023)
    public interface IAccountingRepository : IBaseRepository<Accounting>
    {
        /// <summary>
        /// xóa hạch toán theo id phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public Task DeleteAccoutingByPaymentIdAsync(Guid paymentId);

        /// <summary>
        /// tìm hạch toán đầu tiên theo id tài khoản nợ hoặc tài khoản có
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public Task<Guid> CheckAccountingByAccountId(Guid accountId);

        /// <summary>
        /// Lấy chỉ số hạch toán lớn nhất
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (24/8/2023)
        public Task<int> GetMaxNumberOrderAccountingAsync();
    }
}
