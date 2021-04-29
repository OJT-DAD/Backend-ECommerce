using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stores.Commands.DeleteStore
{
    public class DeleteStoreCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var asset = await _context.Stores.FindAsync(request.Id);

            if (asset == null)
                throw new NotFoundException(nameof(Store), request.Id);

            _context.Stores.Remove(asset);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
