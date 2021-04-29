using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductPrice { get; set; }
        public int ProductStock { get; set; }
    }
}
