using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.TermPayment
{

    /// <summary>
    /// class term payment create dto
    /// </summary>
    public class TermPaymentCreateDTO
    {
        /// <summary>
        /// mã điều khoản thanh toán
        /// </summary>
        public string? TermPaymentCode { get; set; }

        /// <summary>
        /// tên điều khoản thanh toán
        /// </summary>
        public string? TermPaymentName { get; set; }

        /// <summary>
        /// diễn giải
        /// </summary>
        public string? Explain { get; set; }

    }
}
