using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUserManagement _userService;

        public DeleteUserCommandHandler(IUserManagement userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.Delete(request.Id, cancellationToken);
        }
    }
}
