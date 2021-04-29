using Application.Users.Shippings.Commands.ChooseUserShipping;
using Application.Users.Shippings.Queries.GetUserShipping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Users
{
    [Route("user/shipping")]
    public class UserShippingController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetUserShippingVm> GetUserShipping([FromQuery]GetUserShippingQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<string> ChooseUserShipping(ChooseUserShippingCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
