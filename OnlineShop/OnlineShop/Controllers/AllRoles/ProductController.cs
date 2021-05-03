using Application.AllRoles.Products.Queries.GetProductByStore;
using Application.Products.Commands.AddNewProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetAllProduct;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("product")]
    public class ProductController : ApiControllerBase
    {
        [HttpGet("get-all-product")]
        public async Task<GetAllProductVm> GetAll()
        {
            return await Mediator.Send(new GetAllProductQueries());
        }
        [HttpGet("get-by-store")]
        public async Task<GetProductByStoreVm> GetProductByStore([FromQuery]GetProductByStoreQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
