using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException(string msg) : base(msg) { }
        public BadRequestException(List<string> userMsg, Dictionary<string, List<string>>? errsMore)
        {

            UserMsg = userMsg;
            ErrorsMore = errsMore;

        }

        #region Properties

        public List<string> UserMsg { get; set; }
        public Dictionary<string, List<string>>? ErrorsMore { get; set; }

        #endregion
    }
}
