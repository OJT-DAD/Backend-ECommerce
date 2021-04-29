using Application.Common.Mappings;
using Domain.Entities;

namespace Application.UserManagements.Queries.GetUserById
{
    public class UserModel : IMapFrom<UserProperty>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
