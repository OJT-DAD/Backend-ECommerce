using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Carts.Queries.GetCartDetail
{
    public class GetCartDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageName { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
    }
}
