using Domain.Enums;
using System.Collections.Generic;

namespace Application.AllRoles.Histories.Queries.GetAllStoreHistory
{
    public class GetAllStoreHistoryIndexDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string PaymentMethod { get; set; }
        public string TotalTransactionPrice { get; set; }
        public string DateTransactionFinish { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public Status StatusTransaction { get; set; }
        public GetAllStoreHistoryUserPropertyDto UserData { get; set; }
        public GetAllStoreHistoryShippingDto Shipping { get; set; }
        public IList<GetAllStoreHistoryDto> ListsItem { get; set; }
    }
}
