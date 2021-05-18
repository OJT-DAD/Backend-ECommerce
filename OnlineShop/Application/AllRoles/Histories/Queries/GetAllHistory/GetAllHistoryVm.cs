using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Histories.Queries.GetAllHistory
{
    public class GetAllHistoryVm
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public IList<GetAllHistoryIndexDto> Histories { get; set; }
    }
}
