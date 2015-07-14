using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.User.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            InsertUserId = 2;
            ModifyUserId = 2;
        }

        public bool IsUpdate {get;set; }
         [Display(Name = "IsActive", ResourceType = typeof(Pecuniaus.Resources.User.User))]
        public bool IsActive { get; set; }
        public long InsertUserId { get; set; }
        public DateTime InsertDate { get; set; }
        public long ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}