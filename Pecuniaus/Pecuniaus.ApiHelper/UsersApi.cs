using Pecuniaus.Models.User;
using System.Collections.Generic;

namespace Pecuniaus.ApiHelper
{
    public class UsersApi
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            return BaseApiData.GetAPIResult<IEnumerable<UserModel>>("users/GetAllUsers",
                () => new List<UserModel>());
        }

    }
}
