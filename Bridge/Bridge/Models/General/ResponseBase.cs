﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class ResponseBase
    {
        public List<HandleErrorModel> errors { get; set; }
    }
}