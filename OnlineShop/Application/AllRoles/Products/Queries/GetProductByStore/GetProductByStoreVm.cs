using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Products.Queries.GetProductByStore
{
    public class GetProductByStoreVm
    {
        public int NumberOfProducts { get; set; }
        public IList<GetProductByStoreDto> Products { get; set; }
    }
}
