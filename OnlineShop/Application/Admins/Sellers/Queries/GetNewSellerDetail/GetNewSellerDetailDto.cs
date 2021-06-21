namespace Application.Admins.Queries.Sellers.GetNewSellerDetail
{
    public class GetNewSellerDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string NPWP { get; set; }
        public string IdCardNumber { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string StoreAddress { get; set; }
        public string StoreContact { get; set; }
        public string DateRequest { get; set; }
        public string DateApprovalResult { get; set; }
        public string ApprovalResult { get; set; }
    }
}
