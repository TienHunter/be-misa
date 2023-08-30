using MISA.WebFresher042023.Demo.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Entity
{

    /// <summary>
    /// class accounting
    /// </summary>
    public class Accounting:BaseEntity
    {

        /// <summary>
        /// id hạch toán
        /// </summary>
        [KeyProperty]
        public Guid AccountingId { get; set; }

        /// <summary>
        /// id phiếu chi
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// id tài khoản nợ
        /// </summary>
        public Guid AccountDebtId { get; set; }

        /// <summary>
        /// id tài khoản có
        /// </summary>
        public Guid AccountBalanceId { get; set; }

        /// <summary>
        /// số tài khoản nợ
        /// </summary>
        [IgnorePropertyAttribute]
        public string? AccountDebtCode { get; set; }

        /// <summary>
        /// số tài khoản có
        /// </summary>
        [IgnorePropertyAttribute]
        public string? AccountBalanceCode { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? AccountingExplain { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// chỉ số 
        /// </summary>
        [NotUpdateProperty]
        public int NumberOrder { get; set; }
    }
}
