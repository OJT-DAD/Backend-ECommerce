using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Carts.Queries.GetCartDetail
{
    public class GetCartDetailIndexDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string TotalCost { get; set; }
        public string FinalTotalCost { get; set; }
        public string ShippingCost { get; set; }
        public int ShippingId { get; set; }
        public int PaymentId { get; set; }
        public IList<GetCartDetailDto> Lists { get; set; }
    }
}
