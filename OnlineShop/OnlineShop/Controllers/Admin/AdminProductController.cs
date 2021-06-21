using Application.Products.Queries.GetAllProduct;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Admin
{
    [Route("admin/product")]
    public class AdminProductController : ApiControllerBase
    {
        [HttpGet("get-all-product")]
        public async Task<GetAllProductVm> GetAll()
        {
            return await Mediator.Send(new GetAllProductQueries());
        }
    }
}
