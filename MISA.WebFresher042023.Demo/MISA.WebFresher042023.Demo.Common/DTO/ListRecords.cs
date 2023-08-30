using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO
{
    /// <summary>
    /// class list record
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListRecords<T>
    {
        #region Property
        /// <summary>
        /// tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// tổng số gốc
        /// </summary>
        public int TotalRoot { get; set; }

        /// <summary>
        /// danh sách dữ liệu
        /// </summary>
        public List<T> Data { get; set; }
        #endregion
    }
}
