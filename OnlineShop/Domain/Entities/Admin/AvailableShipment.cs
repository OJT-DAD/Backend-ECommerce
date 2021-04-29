using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Admin
{
    public class AvailableShipment
    {
        public int Id { get; set; }
        public string ShipmentName { get; set; }
        public decimal ShipmentCost { get; set; }
    }
}
