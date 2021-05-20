using Application.Admins.Commands.Banks.CreateBank;
using Application.Admins.Commands.Banks.DeleteBank;
using Application.Admins.Queries.Banks.GetAllBank;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Admin
{
    [Authorize(Roles = Role.Admin + ", " + Role.Seller)]
    [Route("admin/bank")]
    public class AdminBankController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetAllBankVm> GetAll()
        {
            return await Mediator.Send(new GetAllBankQuery());
        }

        [HttpPost]
        public async Task<string> AddNewBank(CreateBankCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await Mediator.Send(new DeleteBankCommand { Id = id });
        }
    }
}
