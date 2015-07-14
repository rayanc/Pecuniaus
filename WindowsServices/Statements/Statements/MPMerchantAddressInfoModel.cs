using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statements
{
    public class MPMerchantAddressInfoModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int ProvinceID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
    }
}