using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
     public class BaseModelUser  
    { 
        public bool IsActive { get; set; }        
        public long InsertUserId { get; set; } 
        public DateTime InsertDate { get; set; }
        public long ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
    }

     public class UserModel : BaseModelUser
    {
        public long ID { get; set; }
        public int merchantID { get; set; }
        public string SecurityStamp { get; set; }
        public string UserID { get; set; }   
        public string UserName { get; set; }
        public string Password { get; set; }
         
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Notes { get; set; }
        public string GroupName { get; set; }
        public string SSN { get; set; }
      
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public bool IsSales { get; set; }
        public bool IsCollector { get; set; }

        public string Email { get; set; }
       
        public long ContactID { get; set; }
        public int StateID { get; set; }
        public long AddressID { get; set; }
        public long? GroupID { get; set; }  
    }
    public class LoginModel 
    { 
        public string userId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string email { get; set; }
    }
}