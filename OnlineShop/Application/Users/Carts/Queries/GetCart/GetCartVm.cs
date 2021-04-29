using System;
using System.Collections.Generic;

namespace Application.Carts.Queries.GetCart
{
    public class GetCartVm
    {
       public IList<GetCartIndexDto> Carts { get; set; }
    }
}