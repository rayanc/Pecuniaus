using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantOwnerDetailModel
    {
        public MPMerchantOwnerDetailModel()
        {
            Owner = new MPMerchantOwnersModel();
            Address = new MPMerchantAddressInfoModel();
        }
        public Int64 MerchantID { get; set; }
        public Int64 UserId { get; set; }
        public MPMerchantOwnersModel Owner{get; set;}
        public MPMerchantAddressInfoModel Address{get; set;}
    }
}