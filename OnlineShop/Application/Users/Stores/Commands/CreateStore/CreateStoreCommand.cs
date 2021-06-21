using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

            //validasi satu user satu toko
            var userAsset = await _context.UserProperties.FindAsync(request.UserPropertyId);
            if (userAsset.Role == "Seller")
                throw new AppException("You can only have one store!");

            var uniqueNameValidation = await _context.Stores
                .AllAsync(x => x.Name == request.StoreName);

            if (uniqueNameValidation)
                throw new AppException("Store name alredy exist!");

            if (_context.NewSellers.Any(x => x.UserPropertyId == request.UserPropertyId))
                throw new AppException("You have made a request, please wait for confirmation from the ADMIN!");

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
