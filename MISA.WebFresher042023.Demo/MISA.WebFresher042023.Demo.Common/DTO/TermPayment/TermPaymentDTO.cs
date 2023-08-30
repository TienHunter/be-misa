using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.TermPayment
{

    /// <summary>
    /// class term payment dto
    /// </summary>
    public class TermPaymentDTO
    {
        /// <summary>
        /// id điều khoản thanh toán
        /// </summary>
        public Guid TermPaymentId { get; set; }

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

        /// <summary>
        /// trạng thái sử dụng
        /// </summary>
        public int? Active { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

    }
}
