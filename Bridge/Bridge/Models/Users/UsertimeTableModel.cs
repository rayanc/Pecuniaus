using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class UsertimeTableModel:BaseModelUser
    {
        public UsertimeTableModel()
        {
            MondayFromTime = MondayToTime = TuesdayFromTime = TuesdayToTime = WednesdayFromTime = WednesdayToTime;
            ThursdayFromTime = ThursdayToTime = FridayFromTime = FridayToTime = SaturdayFromTime = "";
            SaturdayToTime = SundayFromTime = SundayToTime = "";
            UserID = UserTimeTableID = 0;
            WorkFlowID = 0;
            lstTimeTableDetails=new List<UserTimeTableDetailsModel>();
        }
      public long UserTimeTableID { get; set; } 
      public long UserID { get; set; } 
      public int WorkFlowID { get; set; } 
      public DateTime? AssignedDate { get; set; } 
      public bool IsMondayWork{ get; set; } 
      public bool IsTuesdayWork{ get; set; } 
      public bool IsWednesdayWork{ get; set; } 
      public bool IsThursdayWork{ get; set; } 
      public bool IsFridayWork{ get; set; } 
      public bool IsSaturdayWork{ get; set; } 
      public bool IsSundayWork{ get; set; } 
      public string MondayFromTime { get; set; } 
      public string MondayToTime{ get; set; } 
      public string TuesdayFromTime{ get; set; } 
      public string TuesdayToTime{ get; set; } 
      public string WednesdayFromTime{ get; set; } 
      public string WednesdayToTime{ get; set; } 
      public string ThursdayFromTime{ get; set; } 
      public string ThursdayToTime{ get; set; } 
      public string FridayFromTime{ get; set; } 
      public string FridayToTime{ get; set; } 
      public string SaturdayFromTime{ get; set; } 
      public string SaturdayToTime{ get; set; } 
      public string SundayFromTime{ get; set; } 
      public string SundayToTime{ get; set; }
      public IList<UserTimeTableDetailsModel> lstTimeTableDetails { get; set; }

    }

 public class UserTimeTableDetailsModel:BaseModelUser
    {
        public long UserTimetableDetailsID { get; set; }
        public long UserTimetableID { get; set; }
        public int TaskTypeID { get; set; }   
        public long UserID { get; set; }           
        public string TaskName { get; set; }          
        public string WorlFlowName { get; set; }  
        public DateTime? AssignedDate { get; set; }       
        public bool Monday{ get; set; } 
        public bool Tuesday{ get; set; } 
        public bool Wednesday{ get; set; } 
        public bool Thursday{ get; set; } 
        public bool Friday{ get; set; } 
        public bool Saturday{ get; set; } 
        public bool Sunday{ get; set; } 
    }
}