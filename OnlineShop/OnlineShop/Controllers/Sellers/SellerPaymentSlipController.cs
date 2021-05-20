using Application.Common.Models;
using Application.Sellers.PaymentSlips.Commands.AcceptSubmission;
using Application.Sellers.PaymentSlips.Commands.RefuseSubmission;
using Application.Sellers.PaymentSlips.Queries.GetSellerPaymentSlip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Authorize(Roles = Role.Seller)]
    [Route("seller/payment-slip")]
    public class SellerPaymentSlipController : ApiControllerBase
    {
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
