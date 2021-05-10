using Application.Carts.Commands.CreateCart;
using Application.Carts.Commands.DeleteCart;
using Application.Carts.Queries.GetCart;
using Application.Carts.Queries.GetCartDetail;
using Application.Users.Carts.Commands.UpdateCart;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("user/cart")]
    public class UserCartController : ApiControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<GetCartVm> GetAll(int userId)
        {
            return await Mediator.Send(new GetCartQuery { UserId = userId });
        }

        [HttpGet("GetDetailCart/{indexId}")]
        public async Task<GetCartDetailVm> GetDetail(int indexId)
        {
            return await Mediator.Send(new GetCartDetailQuery { CartIndexId = indexId });
        }

        [HttpPost]
        public async Task<string> Add(CreateCartCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("edit-quantity")]
        public async Task<string> UpdateStock(UpdateCartCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{productId}")]
        public async Task<string> Delete(int productId)
        {
            return await Mediator.Send(new DeleteCartCommand { Id = productId });
        }
    }
}
