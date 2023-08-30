using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Manager
{
    /// <summary>
    /// interface validate hạch toán
    /// </summary>
    /// created by: vdtien (13/8/2023)
    public interface IAccountingManager
    {
        /// <summary>
        /// check tài khoản nợ null hoặc rỗng
        /// </summary>
        /// <param name="accountDebtId"></param>
        /// created by: vdtien (13/8/2023)
        //void CheckAccountDebtNullOrEmpty(Guid? accountDebtId);

        /// <summary>
        /// check tài khoản có null hoặc rỗng
        /// </summary>
        /// <param name="accountBalanceId"></param>
        /// created by: vdtien (13/8/2023)
        //void CheckAccountBalanceNullOrEmpty(Guid? accountBalanceId);
    }
}
