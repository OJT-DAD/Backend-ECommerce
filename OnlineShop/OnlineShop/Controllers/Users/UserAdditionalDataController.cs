using Application.Users.AdditionalDatas.Commands.AddAdditionalData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Users
{
    [Route("user/additional-data")]
    public class UserAdditionalDataController : ApiControllerBase
    {
        [HttpPost]
        public async Task<string> AddAdditionalData(AddAdditionalDataCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
