using Application.Admins.Sellers.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.Sellers.AcceptNewSeller
{
    public class NewSellerConfirmationCommand : IRequest<Store>
    {
        public int Id { get; set; }
    }

    public class NewSellerConfirmationCommandHandler : IRequestHandler<NewSellerConfirmationCommand, Store>
    {
        private readonly IApplicationDbContext _context;

        public NewSellerConfirmationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Store> Handle(NewSellerConfirmationCommand request, CancellationToken cancellationToken)
        {
            if (!_context.NewSellers.Any(x => x.Id == request.Id))
                throw new NotFoundException(nameof(NewSeller), request.Id);

            //get new seller by id
            var asset = await _context.NewSellers.FindAsync(request.Id);

            //add new Store data
            var entity = new Store
            {
                UserPropertyId = asset.UserPropertyId,
                Name = asset.StoreName,
                Description = asset.StoreDescription,
                Address = asset.StoreAddress,
                Contact = asset.StoreContact,
                IdCardNumber = asset.IdCardNumber,
                NPWP = asset.NPWP
            };

            _context.Stores.Add(entity);
            
            //Set Role from USER to SELLER
            var userRoleAsset = await _context.UserProperties
                .Where(x => x.Id == asset.UserPropertyId)
                .FirstOrDefaultAsync();

            userRoleAsset.Role = Role.Seller;

            asset.DateApprovalResult = DateTime.Now;
            asset.ApprovalResult = Model.Approved;

            _context.NewSellers.Remove(asset);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

    }
}