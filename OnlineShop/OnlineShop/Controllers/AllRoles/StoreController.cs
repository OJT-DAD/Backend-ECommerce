using Application.Stores.Commands.CreateStore;
using Application.Stores.Commands.DeleteStore;
using Application.Stores.Commands.UpdateStore;
using Application.Stores.Queries.GetAllStore;
using Application.Stores.Queries.GetStoreById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("store")]
    public class StoreController : ApiControllerBase
    {
        [HttpGet("get-all")]
        public async Task<GetAllStoreVm> GetAll()
        {
            return await Mediator.Send(new GetAllStoreQueries());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<GetStoreByIdVm> GetById(int id)
        {
            return await Mediator.Send(new GetStoreByIdQuery { StoreId = id });
        }

        [HttpPost("create-store")]
        public async Task<string> Add(CreateStoreCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
