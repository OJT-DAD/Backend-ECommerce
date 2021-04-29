using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Carts.Queries.GetCartDetail
{
    public class GetCartDetailIndexDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string TotalCartPrice { get; set; }
        public string ShippingCost { get; set; }
        public IList<GetCartDetailDto> Lists { get; set; }
    }
}
