using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.User.Models
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            States = new List<SelectListItem>();
            Groups = new List<SelectListItem>();
            UserID = UserName = PostalCode = FirstName = LastName = string.Empty;
            DateofBirth = DateTime.Now.AddYears(-10);

        }
        public int ID { get; set; }
        public string UserID { get; set; }
        public string SecurityStamp { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.User.User), ErrorMessageResourceName = "UserNameReq")]
        [Display(Name = "UserName", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string UserName { get; set; }


        /*
         *  [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        */

        [Required(ErrorMessageResourceType = typeof(Resources.User.User), ErrorMessageResourceName = "PasswordReq")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Password", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The  password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.User.User), ErrorMessageResourceName = "FirstNameReq")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "FirstName", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.User.User), ErrorMessageResourceName = "LastNameReq")]
        [Display(Name = "LastName", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "DateofBirth", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string Notes { get; set; }

        [Display(Name = "GroupName", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string GroupName { get; set; }
        [Required]
        [Display(Name = "SSN", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string SSN { get; set; }



        [Display(Name = "AddressLine1", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string AddressLine2 { get; set; }

        [Display(Name = "State", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string State { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "PostalCode", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public string PostalCode { get; set; }      

        [Display(Name = "IsSales", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public bool IsSales { get; set; }

        [Display(Name = "IsCollector", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public bool IsCollector { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }

        public List<UserModel> ListUsers { get; set; }

        public long ContactID { get; set; }

        [Display(Name = "State", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        [Required(ErrorMessageResourceType = typeof(Resources.User.User), ErrorMessageResourceName = "StateReq")]
        public int StateID { get; set; }

        public long AddressID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.User.User), ErrorMessageResourceName = "GroupReq")]
        public long? GroupID { get; set; }

    }

    public class LoginUser
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}