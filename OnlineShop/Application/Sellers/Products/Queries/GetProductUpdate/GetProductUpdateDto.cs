using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Products.Queries.GetProductUpdate
{
    public class GetProductUpdateDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageProductUrl { get; set; }
        public string ImageProductName { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Stock { get; set; }
    }
}
