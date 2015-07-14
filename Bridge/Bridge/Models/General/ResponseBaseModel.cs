using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bridge.Models
{
    public class ResponseBaseModel
    {
        public List<HandleErrorModel> errors { get; set; }
    }
}