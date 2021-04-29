using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Shippings.Queries.GetShippingMethod
{
    public class GetShippingMethodVm
    {
        public int ShippingMethodCount { get; set; }
        public IList<GetShippingMethodDto> ShippingMethods { get; set; }
    }
}
