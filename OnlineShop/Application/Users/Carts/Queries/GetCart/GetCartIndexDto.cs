using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Carts.Queries.GetCart
{
    public class GetCartIndexDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string TotalPriceCart { get; set; }
        public IList<GetCartDto> Lists { get; set; }
    }
}
