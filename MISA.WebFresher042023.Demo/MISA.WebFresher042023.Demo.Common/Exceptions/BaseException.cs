using MISA.WebFresher042023.Demo.Common.Enums;
using System.Text.Json;

namespace MISA.WebFresher042023.Demo.Common.Exceptions
{
    public class BaseException
    {
        #region Propperties

        public ErrorCode ErrCode { get; set; }
        public string? DevMsg { get; set; }
        public List<string>? UserMsg { get; set; }
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set; }

        public Dictionary<string, List<string>>? ErrorsMore { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}
