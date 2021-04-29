namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public virtual Store Store { get; set; }
        public int StoreId { get; set; }
        public Stock Stock { get; set; }
    }
}
