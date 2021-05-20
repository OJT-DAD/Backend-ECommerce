using Application.Common.Models;
using Application.Sellers.Transactions.Commands.SetArrived;
using Application.Sellers.Transactions.Commands.SetOnDelivery;
using Application.Sellers.Transactions.Queries.GetTransactionStore;
using Application.Sellers.Transactions.Queries.GetTransactionStoreDetail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Authorize(Roles = Role.Seller)]
    [Route("seller/transaction")]
    public class SellerTransactionController : ApiControllerBase
    {
        [HttpGet("all-transaction/{storeId}")]
        public async Task<GetTransactionByStoreIdVm> GetAll(int storeId)
        {
            return await Mediator.Send(new GetTransactionByStoreIdQuery { StoreId = storeId });
        }

        [HttpGet("detail-transaction/{transactionIndexId}")]
        public async Task<GetTransactionStoreDetailVm> GetDetail(int transactionIndexId)
        {
            return await Mediator.Send(new GetTransactionStoreDetailQuery { TransactionIndexId = transactionIndexId });
        }

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
