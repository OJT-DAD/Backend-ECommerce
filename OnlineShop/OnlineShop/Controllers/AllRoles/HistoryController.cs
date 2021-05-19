using Application.AllRoles.Histories.Queries.GetAllHistory;
using Application.AllRoles.Histories.Queries.GetAllStoreHistory;
using Application.AllRoles.Histories.Queries.GetAllUserHistory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.AllRoles
{
    [Route("history")]
    public class HistoryController : ApiControllerBase
    {
        [HttpGet("user-history/{userId}")]
        public async Task<GetAllUserHistoryVm> GetUserHistory(int userId)
        {
            return await Mediator.Send(new GetAllUserHistoryQuery { UserId = userId });
        }
        [HttpGet("seller-history/{storeId}")]
        public async Task<GetAllStoreHistoryVm> GetStoreHistory(int storeId)
        {
            return await Mediator.Send(new GetAllStoreHistoryQuery { StoreId = storeId });
        }
        [HttpGet("admin-history")]
        public async Task<GetAllHistoryVm> GetAdminHistory()
        {
            return await Mediator.Send(new GetAllHistoryQuery());
        }
    }
}
