using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AdditionalData
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public int CartIndexId { get; set; }
    }
}
