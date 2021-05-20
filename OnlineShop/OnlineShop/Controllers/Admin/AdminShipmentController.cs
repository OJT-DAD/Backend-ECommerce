using Application.Admins.Commands.AvailableShipments.CreateAvailableShipment;
using Application.Admins.Commands.AvailableShipments.DeleteAvailableShipment;
using Application.Admins.Queries.AvailableShipments.GetAllAvailableShipment;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Admin
{
    [Authorize(Roles = Role.Admin + ", " + Role.Seller)]
    [Route("admin/shipping")]
    public class AdminShipmentController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetAllAvailableShipmentVm> GetAll()
        {
            return await Mediator.Send(new GetAllAvailableShipmentQuery());
        }

        [HttpPost]
        public async Task<string> AddNewShippingMethod(CreateAvailableShipmentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteShippingMethod(int id)
        {
            return await Mediator.Send(new DeleteAvailableShipmentCommand { Id = id });
        }
    }
}
