using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Enums
{
    public enum StatusAction
    {
        Delete = -1, // xóa
        NoChange=0, // không thay đổi
        Create = 1, // thêm mới
        Edit = 2, // sửa
    }
}
