using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Histories.Queries.GetAllUserHistory
{
    public class GetAllUserHistoryIndexDto
    {
        public int Id { get; set; }
        public string TotalTransactionPrice { get; set; }
        public string FullName { get; set; }
        public string DateTransactionFinish { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public Status Status { get; set; }
        public GetAllUserHistoryPaymentDto Payment { get; set; }
        public GetAllUserHistoryShippingDto Shipping { get; set; }
        public IList<GetAllUserHistoryDto> ListItem { get; set; }
    }
}
