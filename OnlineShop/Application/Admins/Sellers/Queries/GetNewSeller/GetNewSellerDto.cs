namespace Application.Admins.Queries.Sellers.GetNewSeller
{
    public class GetNewSellerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string NPWP { get; set; }
        public string IdCardNumber { get; set; }
        public string DateRequest { get; set; }
    }
}
