using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }

    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var asset = await _context.Stores.FindAsync(request.Id);

            if (asset == null)
                throw new NotFoundException(nameof(Store), request.Id);

            if (request.Name != "")
                asset.Name = request.Name;
            if (request.Description != "")
                asset.Description = request.Description;
            if (request.Address != "")
                asset.Address = request.Address;
            if (request.Contact != "")
                asset.Contact = request.Contact;

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
