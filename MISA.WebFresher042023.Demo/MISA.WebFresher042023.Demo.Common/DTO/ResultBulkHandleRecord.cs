using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO
{
    /// <summary>
    /// class kết quả trả về ResultBulkHandleRecord
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultBulkHandleRecord<T>
    {
        /// <summary>
        /// tống số bản ghi xử lý
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// xử lý thành công
        /// </summary>
        public int Success { get; set; }

        /// <summary>
        /// xử lý thất bại
        /// </summary>
        public int Failure { get; set; }

        /// <summary>
        /// danh sách bản ghi xử lý thất bại
        /// </summary>
        public List<T>? ListRecordFailure { get; set; }
    }
}
