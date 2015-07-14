using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantContactDetailModel
    {
        public MPMerchantContactDetailModel()
        {
            Contact = new MPMerchantContactsModel();
            Address = new MPMerchantAddressInfoModel();
        }
        public Int64 UserId { get; set; }
        public Int64 MerchantID { get; set; }
        public MPMerchantContactsModel Contact{get; set;}
        public MPMerchantAddressInfoModel Address{get; set;}
    }
}