using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Notification.Models 
{ 
    public class ProcessorModel : BaseModel    {
        public ProcessorModel()
        {
            LstProcessors = new List<ProcessorModel>();
        }
        public int ProcessorId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Notification.Processor), ErrorMessageResourceName = "ProcessorNameReq")]
        [Display(Name = "ProcessorName", ResourceType = typeof(Pecuniaus.Resources.Notification.Processor))]
        public string Name { get; set; }

        [Display(Name = "ProcessorCode", ResourceType = typeof(Pecuniaus.Resources.Notification.Processor))]      
        public string ProcessorCode { get; set; }
        public string Description { get; set; } 
        public bool IsActive { get; set; }
        public List<ProcessorModel> LstProcessors { get; set; }    
    }
}