
namespace Pecuniaus.Models.User
{
    public class ApplicationUser
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        //public string Email { get; set; }
    }
}
