using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Histories.Queries.GetAllHistory
{
    public class GetAllHistoryShippingDto
    {
        public int ShippingId { get; set; }
        public string ShippingMethodName { get; set; }
        public string ShippingCost { get; set; }
    }
}
