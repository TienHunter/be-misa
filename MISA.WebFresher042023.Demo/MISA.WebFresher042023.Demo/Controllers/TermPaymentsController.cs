using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Common.DTO.GroundSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.GroupSupplier;
using MISA.WebFresher042023.Demo.Common.DTO.TermPayment;
using MISA.WebFresher042023.Demo.Core.Interface.Services;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TermPaymentsController
        : BasesController<TermPaymentDTO, TermPaymentCreateDTO, TermPaymentUpdateDTO>
    {
        public TermPaymentsController(ITermPaymentService termPaymentService) : base(termPaymentService)
        {
        }
    }
}
