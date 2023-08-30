using Dapper;
using MISA.WebFresher042023.Demo.Common.DTO;
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
    /// <summary>
    /// thao tac truy vấn với table accounting
    /// </summary>
    /// created by: vdtien (13/8/2023)
    public class AccountingRepository : BaseRepository<Accounting>, IAccountingRepository
    {
        public AccountingRepository(IUnitOfWork uow) : base(uow)
        {
        }

        /// <summary>
        /// xóa hạch toán theo id phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (13/8/2023)
        public async Task DeleteAccoutingByPaymentIdAsync(Guid paymentId)
        {
            var sql = "DELETE FROM accounting WHERE PaymentId = @paymentId ;";
            await _uow.Connection.ExecuteAsync(sql, new { paymentId }, transaction: _uow.Transaction);
        }

        /// <summary>
        /// tìm hạch toán đầu tiên theo id tài khoản nợ hoặc id tài khoản xóa
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (13/8/2023)
        public async Task<Guid> CheckAccountingByAccountId(Guid accountId)
        {
            var sql = "SELECT AccountingId FROM accounting WHERE AccountDebtId = @accountId OR AccountBalanceId = @accountId LIMIT 1;";

            var res = await _uow.Connection.QueryFirstOrDefaultAsync<Guid>(sql, new { accountId }, transaction: _uow.Transaction);
            return res;
        }

        /// <summary>
        /// lấy chỉ số hạch toán lớn nhất
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (24/8/2023)
        public async Task<int> GetMaxNumberOrderAccountingAsync()
        {
            var sql = "SELECT NumberOrder FROM accounting ORDER BY NumberOrder DESC LIMIT 1 ;";
            var res = await _uow.Connection.QueryFirstOrDefaultAsync<int?>(sql);
            return res ?? 0;
        }
    }
}
