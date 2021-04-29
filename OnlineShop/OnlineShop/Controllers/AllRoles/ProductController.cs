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
        [HttpGet]
        public async Task<GetAllProductVm> GetAll([FromQuery]GetAllProductQueries query)
        {
            return await Mediator.Send(query);
        }
    }
}
