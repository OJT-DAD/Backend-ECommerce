using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<GetAllUserVm>
    {
    }
    
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, GetAllUserVm>
    {
        private readonly IUserManagement _userService;

        public GetAllUserQueryHandler(IUserManagement userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUserVm> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return new GetAllUserVm
            {
                UserDatas = await _userService.GetAll(cancellationToken)
            };
        }
    }
}
