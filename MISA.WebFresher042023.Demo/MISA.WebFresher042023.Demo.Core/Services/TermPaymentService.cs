using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.TermPayment;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Services
{
    /// <summary>
    /// class thuc thi logic điều khoản thanh toán
    /// </summary>
    /// created by: vdtien (26/07/2023)
    public class TermPaymentService : BaseService<TermPayment, TermPaymentDTO, TermPaymentCreateDTO, TermPaymentUpdateDTO>, ITermPaymentService
    {
        #region Constructor
        public TermPaymentService(ITermPaymentRepository termPaymentRepository, IMapper mapper, IUnitOfWork uow) : base(termPaymentRepository, mapper, uow)
        {
        } 
        #endregion
    }
}
