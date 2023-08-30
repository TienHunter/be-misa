using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    /// <summary>
    /// class thực thi TermPaymentRepository
    /// </summary>
    /// created by: vdtien (27/7/2023)
    public class TermPaymentRepository : BaseRepository<TermPayment>, ITermPaymentRepository
    {
        #region Constructor
        public TermPaymentRepository(IUnitOfWork uow) : base(uow)
        {
        } 
        #endregion
    }
}
