using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Pecuniaus.Web.Models
{
    public class SearchModel
    {
        public string MerchantInformation { get; set; }
        public IEnumerable<GeneralModel> TaskNameList { get; set; }
        public IEnumerable<GeneralModel> TaskStatusList { get; set; }
    }
}