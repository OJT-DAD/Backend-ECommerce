using Application.Users.PaymentSlips.Commands.UploadPaymentSlip;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Users
{
    [Route("user/payment-slip")]
    public class UserPaymentSlipController : ApiControllerBase
    {
        [HttpPost]
        public async Task<string> UploadPaymentSlip([FromForm]UploadPaymentSlipCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
