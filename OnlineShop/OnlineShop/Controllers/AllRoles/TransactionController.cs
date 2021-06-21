using Application.Users.PaymentSlips.Queries.GetConfirmationPaymentSlip;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.AllRoles
{
    [Route("transaction")]
    public class TransactionController : ApiControllerBase
    {
        [HttpGet("status/{id}")]
        public async Task<string> Status(int id)
        {
            return await Mediator.Send(new GetStatusTransactionQuery { TransactionIndexId = id });
        }
    }
}
