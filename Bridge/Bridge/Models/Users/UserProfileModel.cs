using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class UserProfileModel:BaseModelUser
    {
        public int UserID { get; set; }
        public int TaskTypeID { get; set; }    
        public string TaskName { get; set; }  
        public int WorkFlowID { get; set; } 
        public string WorlFlowName { get; set; }  
        public DateTime? AssignedDate { get; set; }       
        public bool Monday{ get; set; } 
        public bool Tuesday{ get; set; } 
        public bool Wednesday{ get; set; } 
        public bool Thursday{ get; set; } 
        public bool Friday{ get; set; } 
        public bool Saturday{ get; set; } 
        public bool Sunday{ get; set; } 
        public string FromTime{ get; set; } 
        public string ToTime{ get; set; }        
    }


}