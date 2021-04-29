using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class NewSeller
    {
        public int Id { get; set; }
        public UserProperty UserProperty { get; set; }
        public int UserPropertyId { get; set; }
        public string NPWP { get; set; }
        public string IdCardNumber { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string StoreAddress { get; set; }
        public string StoreContact { get; set; }
        public DateTime DateRequest { get; set; }
    }
}
