using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Manager
{
    /// <summary>
    /// interface check validate termpayment
    /// </summary>
    /// created by: vdtien (27/7/2023)
    public interface ITermPaymentManager
    {
        /// <summary>
        /// check tồn tại điều khoản thanh toán theo id
        /// </summary>
        /// <param name="termPaymentId"></param>
        /// <returns></returns>
        /// created by: vdtien (27/7/2023)
        Task<TermPayment> CheckTermPaymentExsitByIdAsync(Guid termPaymentId);
    }
}
