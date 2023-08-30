using MISA.WebFresher042023.Demo.Common.Enums;

namespace MISA.WebFresher042023.Demo.Common.Exceptions
{
    public class DupCodeException : Exception
    {
        public DupCodeException() : base() { }
        public DupCodeException(List<string> userMsg, Dictionary<string, List<string>>? errorsMore)
        {
            UserMsg = userMsg;
            ErrorsMore = errorsMore;
        }
        public DupCodeException(List<string> userMsg, Dictionary<string, List<string>>? errorsMore, ErrorCode? errorCode)
        {
            UserMsg = userMsg;
            ErrorsMore = errorsMore;
            ErrorCode = errorCode;
        }

        #region Properties

        public List<string> UserMsg { get; set; }
        public Dictionary<string, List<string>>? ErrorsMore { get; set; }
        public ErrorCode? ErrorCode { get; set; }

        #endregion
    }
}
