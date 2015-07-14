using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.User.Models
{
    public class GroupModel : BaseModel
    {
        public GroupModel()
        {
            GroupTypes = new List<SelectListItem>();
            LstGroups = new List<GroupModel>();
        }
        public Int32 GroupID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.User.Group), ErrorMessageResourceName = "GroupTypeReq")]
        [Display(Name = "GroupType", ResourceType = typeof(Pecuniaus.Resources.User.Group))]
         public long? GroupTypeID { get; set; }

        [Display(Name = "GroupType", ResourceType = typeof(Pecuniaus.Resources.User.Group))]
        public string GroupType { get; set; }
        

        [Required(ErrorMessageResourceType = typeof(Resources.User.Group), ErrorMessageResourceName = "GroupNameReq")]
        [Display(Name = "GroupName", ResourceType = typeof(Pecuniaus.Resources.User.Group))]
        public string GroupName { get; set; }

        public string GroupTypeName { get; set; } 

        public bool IsDefault { get; set; } 
      
        public IEnumerable<SelectListItem> GroupTypes { get; set; }
        public List<GroupModel> LstGroups { get; set; }
    }

    public class GroupRights : BaseModel
    { 
        public Int32 GroupRightID { get; set; }
        public Int32 GroupID { get; set; }
        public Int32 ModuleID { get; set; }
        public bool Read { get; set; }  
        public bool Write { get; set; }
        public bool Edit { get; set; } 
    }
}