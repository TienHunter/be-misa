using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Commons
{
    /// <summary>
    /// Ten cac proc
    /// </summary>
    /// Created by: vdtien (19/6/2023)
    public class Procedures
    {
        /// <summary>
        /// ten Proc lay tat ca ban ghi
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// ten Proc lay danh sach ban ghi theo filter va paging
        /// </summary>
        public static string GET_LIST = "Proc_{0}_GetList";

        /// <summary>
        /// ten Proc lay danh sach ban ghi theo filter va paging
        /// </summary>
        public static string GET_LIST_BY_FILTER = "Proc_{0}_GetListByFilter";

        /// <summary>
        /// ten Proc lay 1 ban ghi theo id
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string GET_BY_ID = "Proc_{0}_GetById";

        /// <summary>
        /// ten proc them moi 1 ban ghi
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// ten proc cap nhat 1 ban ghi
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// ten proc xoa 1 ban ghi theo id
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string DELETE = "Proc_{0}_DeleteById";

        /// <summary>
        /// ten proc xoa nhieu ban ghi
        /// </summary>
        /// Created by: vdtien (20/6/2023)
        public static string DELETE_MULTI = "Proc_{0}_DeleteMulti";
    }
}
