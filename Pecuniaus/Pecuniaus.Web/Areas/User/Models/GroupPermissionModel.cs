using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.User.Models
{

    public class GroupPermissionModel:BaseModel 
    {
        public GroupPermissionModel()
        {
            Groups = new List<SelectListItem>();
            ParentModules = new List<SelectListItem>();
            lstGroupRights = new List<GroupPermissionModel>();
        }


        public int GroupRightID { get; set; }

         [Display(Name = "GroupName", ResourceType = typeof(Pecuniaus.Resources.User.Group))]
        [Required(ErrorMessageResourceType = typeof(Resources.User.GroupPermission), ErrorMessageResourceName = "GroupReq")]
        public int? GroupID { get; set; }   
        public int ModuleID { get; set; }

          [Display(Name = "ModuleName", ResourceType = typeof(Pecuniaus.Resources.User.GroupPermission))]
        public string ModuleName { get; set; }

         [Display(Name = "Read", ResourceType = typeof(Pecuniaus.Resources.User.GroupPermission))]
        public bool Read { get; set; }
         [Display(Name = "Write", ResourceType = typeof(Pecuniaus.Resources.User.GroupPermission))]
        public bool Write { get; set; }
         [Display(Name = "Edit", ResourceType = typeof(Pecuniaus.Resources.User.GroupPermission))]
        public bool Edit { get; set; }
         public int PageId { get; set; }
         public IEnumerable<SelectListItem> Groups { get; set; }

         public List<GroupPermissionModel> lstGroupRights { get; set; }
         public IEnumerable<SelectListItem> ParentModules { get; set; }   
      
    }
}