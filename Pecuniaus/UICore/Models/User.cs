using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecuniaus.UICore.Models
{
    public class User : IUser
    {
        public string UserId { get; set; } //PK
        public string PasswordHash { get; set; }
        public string UserName { get; set; }//LoginName
        public string SecurityStamp { get; set; }
        public string FullName { get; set; }
        
        public string Id
        {
            get { return UserId.ToString(); }
        }
    }

}
