using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Admins.Sellers.Queries.GetSellerActive
{
    public class GetSellerActiveDto
    {
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string Contact { get; set; }
        public int NumberOfProducts { get; set; }
    }
}
