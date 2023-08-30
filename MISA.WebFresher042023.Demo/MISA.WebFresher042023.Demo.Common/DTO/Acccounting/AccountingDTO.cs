using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Acccounting
{
    /// <summary>
    /// accounting DTO
    /// </summary>
    /// created by: 13/8/2023
    public class AccountingDTO
    {
        /// <summary>
        /// id hạch toán
        /// </summary>
        public Guid AccountingId { get; set; }

        /// <summary>
        /// id phiếu chi
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// id tài toàn nợ
        /// </summary>
        public Guid AccountDebtId { get; set; }

        /// <summary>
        /// id tài khoản có
        /// </summary>
        public Guid AccountBalanceId { get; set; }

        /// <summary>
        /// số tài khoản nợ
        /// </summary>
        public string? AccountDebtCode { get; set; }

        /// <summary>
        /// số tài khoản có
        /// </summary>
        public string? AccountBalanceCode { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set;}

        /// <summary>
        /// ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? AccountingExplain { get; set; }

        /// <summary>
        /// tổng tiền
        /// </summary>
        public decimal Money { get; set; }
    }
}
