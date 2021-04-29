using Application.Sellers.Transactions.Commands.SetArrived;
using Application.Sellers.Transactions.Commands.SetOnDelivery;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Route("seller/transaction")]
    public class SellerTransactionController : ApiControllerBase
    {
        [HttpPut("set-delivery")]
        public async Task<string> SetOnDelivery(SetOnDeliveryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("set-arrived")]
        public async Task<string> SetArrived(SetArrivedCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
