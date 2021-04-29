using Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Seller
{
    public class Shipment
    {
        public int Id { get; set; }
        public AvailableShipment AvailableShipment { get; set; }
        public int AvailableShipmentId { get; set; }
        public Store Store{ get; set; }
        public int StoreId { get; set; }
    }
}
