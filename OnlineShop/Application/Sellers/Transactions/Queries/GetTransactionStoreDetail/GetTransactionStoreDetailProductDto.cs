using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Transactions.Queries.GetTransactionStoreDetail
{
    public class GetTransactionStoreDetailProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageName { get; set; }
        public string ProductImageUrl { get; set; }
        public string UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
    }
}
