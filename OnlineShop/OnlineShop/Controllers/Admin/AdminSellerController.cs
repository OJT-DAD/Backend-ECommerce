using Application.Admins.Commands.Sellers.AcceptNewSeller;
using Application.Admins.Queries.Sellers.GetNewSeller;
using Application.Admins.Queries.Sellers.GetNewSellerDetail;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Admin
{
    [Route("admin/new-seller")]
    public class AdminSellerController : ApiControllerBase
    {
        [HttpGet("get-new-seller")]
        public async Task<GetNewSellerVm> GetNewSeller()
        {
            return await Mediator.Send(new GetNewSellerQuery());
        }

        [HttpGet("get-new-seller-detail/{id}")]
        public async Task<GetNewSellerDetailVm> GetNewSellerDetail(int id, GetNewSellerDetailQuery query)
        {
            query.Id = id;
            return await Mediator.Send(query);
        }

        [HttpPost("accept-new-seller/{id}")]
        public async Task<Store> AcceptNewSeller(int id)
        {
            var query = new NewSellerConfirmationCommand()
            {
                Id = id
            };
            return await Mediator.Send(query);
        }
    }
}
