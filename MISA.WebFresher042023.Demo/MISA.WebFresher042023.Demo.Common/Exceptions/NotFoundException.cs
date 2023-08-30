namespace MISA.WebFresher042023.Demo.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string msg) : base(msg) { }
        public NotFoundException(List<string> userMsg, Dictionary<string, List<string>>? errorsMore)
        {
            UserMsg = userMsg;
            ErrorsMore = errorsMore;
        }

        #region Properties

        public List<string> UserMsg { get; set; }
        public Dictionary<string, List<string>>? ErrorsMore { get; set; }

        #endregion
    }
}
