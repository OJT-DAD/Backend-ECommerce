using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommand : IRequest<string>
    {
        public int UserPropertyId { get; set; }
        public string NPWP { get; set; }
        public string IdCardNumber { get; set; }
        public DateTime DateRequest { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string StoreAddress { get; set; }
        public string StoreContact { get; set; }
    }

    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, string>
    {
        private readonly IApplicationDbContext _context;
        
        public CreateStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {

            var now = DateTime.Now;

            var entity = new NewSeller
            {
                UserPropertyId = request.UserPropertyId,
                NPWP = request.NPWP,
                IdCardNumber = request.IdCardNumber,
                StoreName = request.StoreName,
                StoreDescription = request.StoreDescription,
                StoreAddress = request.StoreAddress,
                StoreContact = request.StoreContact,
                DateRequest = now
            };

            _context.NewSellers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return "Waiting for confirmation from admin";
        }
    }
}
