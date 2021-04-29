using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionShippingDto
    {
        public int ShippingMethodId { get; set; }
        public string ShippingName { get; set; }
        public string ShippingCost { get; set; }
    }
}
