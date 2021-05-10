using Microsoft.AspNetCore.Http;

namespace Application.Products.Queries.GetAllProduct
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Price { get; set; }
        public int StockProduct { get; set; }
    }
}
