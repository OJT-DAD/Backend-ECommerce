using System.Collections.Generic;

namespace Application.Products.Queries.GetAllProduct
{
    public class GetAllProductVm
    {
        public int NumberOfProducts { get; set; }
        public IList<ProductDto> Products { get; set; }
    }
}
