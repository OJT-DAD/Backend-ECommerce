using Application.Admins.Commands.Sellers.AcceptNewSeller;
using Application.Admins.Commands.Sellers.DeclineNewSeller;
using Application.Admins.Queries.Sellers.GetNewSeller;
using Application.Admins.Queries.Sellers.GetNewSellerDetail;
using Application.Admins.Sellers.Queries.GetSellerActive;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Admin
{
    [Authorize(Roles = Role.Admin)]
    [Route("admin/new-seller")]
    public class AdminSellerController : ApiControllerBase
    {
        [HttpGet("get-all-active-seller")]
        public async Task<GetSellerActiveVm> GetActiveSeller()
        {
            return await Mediator.Send(new GetSellerActiveQuery());
        }

        [HttpGet("get-new-seller")]
        public async Task<GetNewSellerVm> GetNewSeller()
        {
            return await Mediator.Send(new GetNewSellerQuery());
        }

        [HttpGet("get-new-seller-detail/{id}")]
        public async Task<GetNewSellerDetailVm> GetNewSellerDetail(int id)
        {
            return await Mediator.Send(new GetNewSellerDetailQuery { Id = id });
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

        [HttpPost("reject-new-seller/{id}")]
        public async Task<string> RejectNewSeller(int id)
        {
            var query = new DeclineNewSellerCommand
            {
                NewSellerId = id
            };

            return await Mediator.Send(query);
        }
    }
}
