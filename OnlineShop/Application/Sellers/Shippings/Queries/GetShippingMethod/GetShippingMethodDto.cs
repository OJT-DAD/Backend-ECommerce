using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Shippings.Queries.GetShippingMethod
{
    public class GetShippingMethodDto
    {
        public int Id { get; set; }
        public string ShippingName { get; set; }
        public string ShippingCost { get; set; }
    }
}
