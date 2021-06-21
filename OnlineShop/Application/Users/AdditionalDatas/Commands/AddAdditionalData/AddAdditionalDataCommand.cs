using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.AdditionalDatas.Commands.AddAdditionalData
{
    public class AddAdditionalDataCommand : IRequest<string>
    {
        public int CartIndexId { get; set; }
        public string ShippingAddress { get; set; }
        public string Note { get; set; }
    }

    public class AddAdditionalDataCommandHandler : IRequestHandler<AddAdditionalDataCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public AddAdditionalDataCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AddAdditionalDataCommand request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.CartIndexs.AnyAsync(x => x.Id == request.CartIndexId);
            if (!validationExist)
                throw new NotFoundException(nameof(CartIndex), request.CartIndexId);

            var entity = new AdditionalData
            {
                CartIndexId = request.CartIndexId,
                ShippingAddress = request.ShippingAddress,
                Note = request.Note
            };

            _context.AdditionalDatas.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Additional data has been created";
        }
    }
}
