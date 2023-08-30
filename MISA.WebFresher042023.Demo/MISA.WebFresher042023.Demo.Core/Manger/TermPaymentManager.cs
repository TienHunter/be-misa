using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Common.Resources;
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
    /// class thực thi TermPaymentManager
    /// </summary>
    /// created by: vdtien (25/7/2023)
    public class TermPaymentManager : ITermPaymentManager
    {
        #region Field
        private readonly ITermPaymentRepository _termPaymentRepository;
        #endregion

        #region Constructor
        public TermPaymentManager(ITermPaymentRepository termPaymentRepository)
        {
            _termPaymentRepository = termPaymentRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// check điều khoản thanh toán tồn tại
        /// </summary>
        /// <param name="termPaymentId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (25/7/2023)
        public async Task<TermPayment> CheckTermPaymentExsitByIdAsync(Guid termPaymentId)
        {
            var termPayment = await _termPaymentRepository.GetAsync(termPaymentId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundTermPayment);

            return termPayment;
        } 
        #endregion
    }
}
