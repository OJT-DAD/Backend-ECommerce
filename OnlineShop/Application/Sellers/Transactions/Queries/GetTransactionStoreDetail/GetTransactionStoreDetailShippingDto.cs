using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Transactions.Queries.GetTransactionStoreDetail
{
    public class GetTransactionStoreDetailShippingDto
    {
        public int ShippingMethodId { get; set; }
        public string ShippingMethodName { get; set; }
        public string ShippingCost { get; set; }
    }
}
