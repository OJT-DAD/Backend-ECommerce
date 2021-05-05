using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserLoginSuccessDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserLoginSuccessDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagement _userService;
        private readonly AppSettingUsers _appSettings;

        public LoginUserCommandHandler(
            IUserManagement userService, 
            IOptions<AppSettingUsers> appSettings,
            IApplicationDbContext context)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<UserLoginSuccessDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);

            if (user == null)
            {
                throw new NotFoundException("Username or Password is incorrect");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var entity = new UserLoginSuccessDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenString,
                Role = user.Role,
                StoreId = StoreAsset(user.Id, user.Role, _context)
            };
            // return basic user info and authentication token
            return entity;
        }

        private static int StoreAsset(int id, string role, IApplicationDbContext context)
        {
            if(role == Role.Seller)
            {
                var storeAsset = context.Stores
                    .Where(x => x.UserPropertyId == id)
                    .FirstOrDefault();

                return storeAsset.Id;
            }

            return 0;
        }
    }
}
