using Application.Sellers.PaymentSlips.Commands.AcceptSubmission;
using Application.Sellers.PaymentSlips.Commands.RefuseSubmission;
using Application.Sellers.PaymentSlips.Queries.GetSellerPaymentSlip;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Route("seller/payment-slip")]
    public class SellerPaymentSlipController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetSellerPaymentSlipVm> Get([FromQuery]GetSellerPaymentSlipQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("accept-submission")]
        public async Task<string> AcceptSubmission(AcceptSubmissionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("refuse-Submission")]
        public async Task<string> RefuseSubmission(RefuseSubmissionCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
