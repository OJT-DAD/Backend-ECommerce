using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionAdditionalDataDto
    {
        public int AdditionalDataId { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
    }
}
