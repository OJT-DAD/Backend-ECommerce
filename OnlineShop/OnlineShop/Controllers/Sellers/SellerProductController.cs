using Application.Common.Models;
using Application.Products.Commands.AddNewProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Sellers.Products.Queries.GetProductUpdate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Sellers
{
    [Authorize(Roles = Role.Seller)]
    [Route("seller/product")]
    public class SellerProductController : ApiControllerBase
    {
        [HttpGet("get-product-update/{productId}")]
        public async Task<GetProductUpdateVm> Get(int productId)
        {
            return await Mediator.Send(new GetProductUpdateQuery { ProductId = productId });
        }

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
