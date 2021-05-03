using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Products.Queries.GetProductByStore
{
    public class GetProductByStoreDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public int StockProduct { get; set; }
    }
}
