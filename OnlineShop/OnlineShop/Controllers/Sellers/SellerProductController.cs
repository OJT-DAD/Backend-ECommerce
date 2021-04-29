using Application.Products.Commands.AddNewProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Route("seller/product")]
    public class SellerProductController : ApiControllerBase
    {
        [HttpPost]
        public async Task<int> Add([FromForm] AddNewProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromForm]UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return await Mediator.Send(new DeleteProductCommand { Id = id });
        }
    }
}
