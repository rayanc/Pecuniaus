using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantLandlordModel
    {
        public MPMerchantLandlordModel()
        {
            Questions = new MPMerchantLandlordQuestionsModel();
            LandlordAddress = new MPMerchantAddressInfoModel();
        }
        public Int64 MerchantID { get; set; }
        public Int64 UserId { get; set; }
        public string MerchantStatus { get; set; }
        public string ContractStatus { get; set; }
        public string TypeofPropertyId { get; set; }
        public string LandlordCompanyName { get; set; }
        public string landlordFirstName { get; set; }
        public string landlordLastName { get; set; }
		public string LandlordName { get; set; }
        public string MonthlyRentAmount { get; set; }
        public MPMerchantAddressInfoModel LandlordAddress { get; set; }
        public MPMerchantLandlordQuestionsModel Questions { get; set; }
    }
}