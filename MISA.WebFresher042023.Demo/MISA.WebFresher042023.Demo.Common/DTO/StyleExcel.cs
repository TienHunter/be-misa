using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO
{
    public class StyleExcel
    {
        public int Row { get;set; }
        public int Col { get;set; }
        public bool FontBold { get; set; }
        public int PaddingTop { get; set; }
        public int PaddingBottom { get; set;}
        public int PaddingLeft { get; set;}
        public int PaddingRight { get; set;}
        public StyleAlignment Horizontal { get; set; }
        public StyleAlignment Vertical { get; set; }
        public string? ColorHex { get; set; }
    }
}
