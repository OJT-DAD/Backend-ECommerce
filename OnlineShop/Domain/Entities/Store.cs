namespace Domain.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string NPWP { get; set; }
        public string IdCardNumber { get; set; }
        public int UserPropertyId { get; set; }
    }
}