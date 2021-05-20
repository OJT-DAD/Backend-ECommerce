using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Transactions.Queries.GetTransactionStore
{
    public class GetTransactionStoreDto
    {
        public int TransactionIndexId { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string TotalTransactionPrice { get; set; }
        public string ShipmentMethod { get; set; }
        public string PaymentMethod{ get; set; }
    }
}
