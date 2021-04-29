using Application.Users.Payments.Commands.ChooseUserPayment;
using Application.Users.Payments.Queries.GetUserPayment;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Users
{
    [Route("user/payment")]
    public class UserPaymentController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetUserPaymentVm> GetUserPayment([FromQuery] GetUserPaymentQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<string> ChooseUserPayment(ChooseUserPaymentCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}