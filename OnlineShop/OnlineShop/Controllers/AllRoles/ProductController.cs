using Application.AllRoles.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("product")]
    public class ProductController : ApiControllerBase
    {
        [HttpGet("get-by-id")]
        public async Task<GetProductByIdVm> GetProductById([FromQuery]GetProductByIdQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
