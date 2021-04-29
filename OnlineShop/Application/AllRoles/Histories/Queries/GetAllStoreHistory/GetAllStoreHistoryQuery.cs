using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AllRoles.Histories.Queries.GetAllStoreHistory
{
    public class GetAllStoreHistoryQuery : IRequest<GetAllStoreHistoryVm>
    {
        public int StoreId { get; set; }
    }

    public class GetAllStoreHistoryQueryHandler : IRequestHandler<GetAllStoreHistoryQuery, GetAllStoreHistoryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStoreHistoryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllStoreHistoryVm> Handle(GetAllStoreHistoryQuery request, CancellationToken cancellationToken)
        {
            if (!_context.Stores.Any(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request.StoreId);


        }
    }
}
