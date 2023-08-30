using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Core.Interface.Manager;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manger
{
    /// <summary>
    /// interface validate accounting manager 
    /// </summary>
    /// created by: vdtien (14/8/2023)
    public class AccountingManager : IAccountingManager
    {
        private readonly IAccountingRepository _accountingRepository;
        public AccountingManager(IAccountingRepository accountingRepository)
        {
            _accountingRepository = accountingRepository;
        }

        //public void CheckAccountBalanceNullOrEmpty(Guid? accountBalanceId)
        //{
        //    if (accountBalanceId == null || accountBalanceId.Equals(Guid.Empty))
        //    {
        //        var userMsg = new List<string>()
        //            {
        //                "Tài khoản có không được để trống."
        //            };
        //        var errMore = new Dictionary<string, List<string>>()
        //            {
        //                {"AccountBalanceId",userMsg }
        //            };
        //        throw new ValidateException(userMsg, errMore);
        //    }
        //}

        //public void CheckAccountDebtNullOrEmpty(Guid? accountDebtId)
        //{
           
        //    if (accountDebtId == null || accountDebtId.Equals(Guid.Empty))
        //    {
        //        var userMsg = new List<string>()
        //            {
        //                "Tài khoản nợ không được để trống."
        //            };
        //        var errMore = new Dictionary<string, List<string>>()
        //            {
        //                {"AccountDebtId",userMsg }
        //            };
        //        throw new ValidateException(userMsg, errMore);
        //    }
        //}
    }
}
