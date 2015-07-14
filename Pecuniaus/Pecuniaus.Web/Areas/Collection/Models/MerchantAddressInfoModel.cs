using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Collection.Models 
{
    public class MerchantAddressInfoModel
    {
        public MerchantAddressInfoModel()
            {
                Provinces = new List<SelectListItem>();
            }
            public string Address { get; set; }
            public string City { get; set; }
            public int ProvinceID { get; set; }
            public int Phone1 { get; set; }
            public int Phone2 { get; set; }
            public int Cell { get; set; }
            public string Email { get; set; }
            public int Fax { get; set; }
            public IEnumerable<SelectListItem> Provinces { get; set; }
        
    }
}