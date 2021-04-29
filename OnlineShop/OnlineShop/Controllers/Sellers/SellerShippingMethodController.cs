using Application.Sellers.Shippings.Commands.ChooseShippingMethod;
using Application.Sellers.Shippings.Commands.DeleteShippingMethod;
using Application.Sellers.Shippings.Queries.GetShippingMethod;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("seller/shipping-method")]
    public class SellerShippingMethodController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetShippingMethodVm> Get([FromQuery] GetShippingMethodQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<string> ChooseShippingMethod(ChooseShippingMethodCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteShippingMethod(int id)
        {
            return await Mediator.Send(new DeleteShippingMethodCommand { Id = id });
        }
    }
}
