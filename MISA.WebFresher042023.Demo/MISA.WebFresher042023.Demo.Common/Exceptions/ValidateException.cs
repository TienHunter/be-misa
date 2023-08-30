namespace MISA.WebFresher042023.Demo.Common.Exceptions
{
    public class ValidateException:Exception
    {
        public ValidateException(string msg) : base(msg) { }
        public ValidateException(List<string> userMsg, Dictionary<string, List<string>>? errsMore)
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
