using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO
{
    /// <summary>
    /// tùy chọn style excel
    /// </summary>
    public class OptionsExcel
    {
        /// <summary>
        /// vị trí hàng
        /// </summary>
        public int? Row { get; set; }

        /// <summary>
        /// vị trí cột
        /// </summary>
        public int? Col { get; set; }

        /// <summary>
        /// font chữ đậm
        /// </summary>
        public bool? FontBold { get; set; }

        /// <summary>
        /// padding top
        /// </summary>
        public int? PaddingTop { get; set; }

        /// <summary>
        /// padding bottom
        /// </summary>
        public int? PaddingBottom { get; set; }

        /// <summary>
        /// padding left
        /// </summary>
        public int? PaddingLeft { get; set; }
        /// <summary>
        /// padding right
        /// </summary>
        public int? PaddingRight { get; set; }

        /// <summary>
        /// căn ngang
        /// </summary>
        public StyleAlignment? Horizontal { get; set; }

        /// <summary>
        /// căn dọc
        /// </summary>
        public StyleAlignment? Vertical { get; set; }

        /// <summary>
        /// màu sác
        /// </summary>
        public string? ColorHex { get; set; }

        /// <summary>
        /// màu nền
        /// </summary>
        public string? BackgroundColorHex { get; set; }

    }
}
