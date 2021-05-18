using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Histories.Queries.GetAllHistory
{
    public class GetAllHistoryDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageName { get; set; }
        public string ProductImageUrl  { get; set; }
        public string PricePerUnit { get; set; }
        public string Quantity { get; set; }
        public string TotalPrice { get; set; }
    }
}
