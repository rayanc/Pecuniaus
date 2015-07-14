using Microsoft.AspNet.Identity;
using Pecuniaus.UICore.Identity;
using Pecuniaus.UICore.Models;

namespace Pecuniaus.UICore
{
    public class PaswordHelper
    {
        public PaswordHelper()
            : this(new UserManager<User>(new UserStore()))
        {

        }

        public PaswordHelper(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<User> UserManager { get; private set; }

        public string HashPassword(string password)
        {
            return UserManager.PasswordHasher.HashPassword(password);
        }

    }
}
