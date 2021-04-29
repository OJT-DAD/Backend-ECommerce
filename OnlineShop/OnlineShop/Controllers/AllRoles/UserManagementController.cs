using Application.UserManagements.Commands.DeleteUser;
using Application.UserManagements.Commands.LoginUser;
using Application.UserManagements.Commands.RegisterUser;
using Application.UserManagements.Commands.UpdateUser;
using Application.UserManagements.Queries.GetAllUser;
using Application.UserManagements.Queries.GetUserById;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Route("user-management")]
    public class UserManagementController : ApiControllerBase
    {
        [HttpGet("get-all")]
        public async Task<GetAllUserVm> GetAll()
        {
            return await Mediator.Send(new GetAllUserQuery());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<UserModel> GetById(int id)
        {
            var queries = new GetUserByIdQuery
            {
                Id = id
            };

            return await Mediator.Send(queries);
        }

        [HttpPost("register")]
        public async Task<UserProperty> Register(RegisterUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<UserLoginSuccessDto> Login(LoginUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<UpdateModel>> Update(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            command.Id = id;
            
            return await Mediator.Send(command);
        }

        [HttpDelete("delete/{id}")]
        public async Task<int> Delete(int id)
        {
            return await Mediator.Send(new DeleteUserCommand { Id = id });
        }
    }
}
