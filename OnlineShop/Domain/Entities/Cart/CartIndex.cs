using Domain.Entities.Seller;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CartIndex
    {
        public int Id { get; set; }
        public UserProperty UserProperty { get; set; }
        public int UserPropertyId { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public int PaymentId { get; set; }
        public int ShipmentId { get; set; }
    }
}
