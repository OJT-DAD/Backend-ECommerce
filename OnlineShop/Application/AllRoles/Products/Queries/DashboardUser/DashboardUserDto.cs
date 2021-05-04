using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Products.Queries.DashboardUser
{
    public class DashboardUserDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public string DateAddedOrUpdated { get; set; }
        public string Price { get; set; }
        public int StockProduct{ get; set; }
    }
}
