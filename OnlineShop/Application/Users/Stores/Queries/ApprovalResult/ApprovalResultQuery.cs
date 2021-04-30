using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Stores.Queries.ApprovalResult
{
    public class ApprovalResultQuery : IRequest<string>
    {
        public int NewSellerId { get; set; }
    }


}
