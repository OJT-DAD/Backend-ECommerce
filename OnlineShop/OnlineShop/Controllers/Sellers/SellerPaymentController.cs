using Application.Sellers.Payments.Commands.ChoosePayment;
using Application.Sellers.Payments.Commands.DeletePayment;
using Application.Sellers.Payments.Queries.GetPayment;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("seller/Payment")]
    public class SellerPaymentController : ApiControllerBase
    {
        [HttpGet("{storeId}")]
        public async Task<GetPaymentVm> Get(int storeId)
        {
            return await Mediator.Send(new GetPaymentQuery { StoreId = storeId });
        }

        [HttpPost]
        public async Task<string> AddPayment(ChoosePaymentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await Mediator.Send(new DeletePaymentCommand { Id = id });
        }
    }
}
