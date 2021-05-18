using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Histories.Queries.GetAllHistory
{
    public class GetAllHistoryIndexDto
    {
        public int IndexId { get; set; }
        public string StoreName { get; set; }
        public IList<GetAllHistoryDto> Items { get; set; }
    }
}
