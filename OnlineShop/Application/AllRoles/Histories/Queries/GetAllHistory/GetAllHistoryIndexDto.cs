using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Histories.Queries.GetAllHistory
{
    public class GetAllHistoryIndexDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string TotalTransactionPrice { get; set; }
        public string DateTransactionFinish { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public Status StatusTransaction { get; set; }
        public GetAllHistoryUserPropertyDto UserData { get; set; }
        public GetAllHistoryShippingDto Shipping { get; set; }
        public IList<GetAllHistoryDto> Items { get; set; }
        public object PaymentMethod { get; internal set; }
    }
}
