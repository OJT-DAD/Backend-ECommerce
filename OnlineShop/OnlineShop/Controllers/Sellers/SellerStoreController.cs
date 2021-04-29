using Application.Stores.Commands.DeleteStore;
using Application.Stores.Commands.UpdateStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Route("seller/store")]
    public class SellerStoreController : ApiControllerBase
    {
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, UpdateStoreCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await Mediator.Send(new DeleteStoreCommand { Id = id });
        }
    }
}
