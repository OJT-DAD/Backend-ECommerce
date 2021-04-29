using Application.UserManagements.Queries.GetUserById;
using System.Collections.Generic;

namespace Application.UserManagements.Queries.GetAllUser
{
    public class GetAllUserVm
    {
        public IList<UserModel> UserDatas { get; set; }
    }
}
