using Application.Common.Models;
using Application.Stores.Commands.DeleteStore;
using Application.Stores.Commands.UpdateStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Authorize]
    [Route("seller/store")]
    public class SellerStoreController : ApiControllerBase
    {
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, UpdateStoreCommand command)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            if (id != command.Id)
                return BadRequest();

            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            return await Mediator.Send(new DeleteStoreCommand { Id = id });
        }
    }
}
