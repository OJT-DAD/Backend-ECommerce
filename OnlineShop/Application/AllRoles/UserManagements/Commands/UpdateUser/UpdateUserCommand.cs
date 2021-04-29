using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateModel>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateModel>
    {
        private readonly IUserManagement _userService;

        public UpdateUserCommandHandler(IUserManagement userService)
        {
            _userService = userService;
        }

        public async Task<UpdateModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserProperty()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email
            };

            return await _userService.Update(user, cancellationToken, request.Password);
        }
    }
}
