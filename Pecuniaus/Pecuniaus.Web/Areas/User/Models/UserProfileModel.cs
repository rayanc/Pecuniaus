using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.User.Models
{
    public class UserProfileModel : BaseModel
    {
        public UserProfileModel()
        {
            WorkFlows = new List<SelectListItem>();
            Users = new List<SelectListItem>();
            ListUsrTasks = new List<UserProfileModel>();
        }

        [Required(ErrorMessageResourceType = typeof(Resources.User.UserProfile), ErrorMessageResourceName = "UserReq")]
        [Display(Name = "Users", ResourceType = typeof(Pecuniaus.Resources.User.UserProfile))]
         public long UserID { get; set; }

        public int TaskTypeID { get; set; }

          public string TaskName { get; set; } 
        [Required(ErrorMessageResourceType = typeof(Resources.User.UserProfile), ErrorMessageResourceName = "WorkflowReq")]
        [Display(Name = "Workflow", ResourceType = typeof(Pecuniaus.Resources.User.UserProfile))]
        public int WorkFlowID { get; set; }
        public string WorlFlowName { get; set; }  
        public DateTime? AssignedDate { get; set; } 
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public IEnumerable<SelectListItem> WorkFlows { get; set; } 
        public IEnumerable<SelectListItem> Users { get; set; }
        public IList<UserProfileModel> ListUsrTasks { get; set; } 
    }

  

}