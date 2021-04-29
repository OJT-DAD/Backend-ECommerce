using Application.Transactions.Commands.CreateTransaction;
using Application.Transactions.Queries.GetTransaction;
using Application.Users.Transactions.Commands.ConfirmationArrivedItem;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("user/transaction")]
    public class UserTransactionController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetTransactionVm> Get([FromQuery] GetTransactionQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> Transaction([FromQuery] CreateTransactionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("confirm-arrived-item")]
        public async Task<string> ConfirmArrivedItem([FromQuery] ConfirmationArrivedItemCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
