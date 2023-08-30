using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Acccounting
{
    /// <summary>
    /// class accounting create dto
    /// </summary>
    /// created by: vdtien (13/8/2023)
    public class AccountingCreateDTO
    {
        /// <summary>
        /// id phiếu chi
        /// </summary>
        public Guid? PaymentId { get; set; }

        /// <summary>
        /// id tài khoản nợ
        /// </summary>
        public Guid AccountDebtId { get; set; }

        /// <summary>
        /// id tài khoản có
        /// </summary>
        public Guid AccountBalanceId { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? AccountingExplain { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>

        [Range(-999999999999, 999999999999, ErrorMessageResourceType =typeof(ResourceVN),ErrorMessageResourceName = nameof(ResourceVN.UserMsg_Range))]
        [Display(ResourceType = typeof(ResourceVN), Name = nameof(ResourceVN.Money))]
        public decimal? Money { get; set; }

    }
}
