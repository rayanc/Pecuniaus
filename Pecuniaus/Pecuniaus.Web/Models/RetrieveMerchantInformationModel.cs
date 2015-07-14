using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Pecuniaus.Web.Models
{
    public class RetrieveMerchantInformationModel
    {
        public string ownerId { get; set; }
        public string rnc { get; set; }
        public double mccv { get; set; }
        public string score { get; set; }
        public string finalletter { get; set; }
        public string roundedscore { get; set; }
        public string rawData { get; set; }


    }
}