using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Histories.Queries.GetAllUserHistory
{
    public class GetAllUserHistoryDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string PricePerUnit { get; set; }
        public string TotalPrice { get; set; }
    }
}
