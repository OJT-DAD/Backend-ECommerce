using Microsoft.AspNetCore.Http;
using System;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public decimal Price { get; set; }
        public virtual Store Store { get; set; }
        public int StoreId { get; set; }
        public DateTime DateAddedOrUpdated { get; set; }
        public Stock Stock { get; set; }
    }
}
