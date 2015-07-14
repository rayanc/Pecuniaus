using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class OfferModel
    {
        public Int64 offerId { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public double turn { get; set; }
        public double loanAmount { get; set; }
        public double ownedAmount { get; set; }
        public double proportion { get; set; }
        public double retention { get; set; }
        public DateTime offerCreationDate { get; set; }
        public DateTime offerAcceptanceDate { get; set; }
        public Int64 insertuserId { get; set; }
        public DateTime offerexpirationDate { get; set; }
        public double monthlyPayment { get; set; }
        public double yearly { get; set; }
        public double salestaken { get; set; }
        public bool IsSelected { get; set; }
        public long maxprice { get; set; }
        public bool IsEmailSent { get; set; }
    }
    public class MerchantInformationOfferModel
    {
        public string businessName { get; set; }
        public Address address { get; set; }
        public double score { get; set; }
        public int ismccvupdated { get; set; }
        public double yearlysales { get; set; }
        public double avgmccv { get; set; }
        public string reason { get; set; }
        public List<SalesRepresentative> salesRepresentative { get; set; }

        public long salestaken { get; set; }
        public string finalscore { get; set; }
        public List<OfferModel> offers { get; set; }
        public List<MinPrice> turnminprice { get; set; }
        public long maxturn { get; set; }
        public bool IsSelected { get; set; }
        public bool IsOffersEmailSent { get; set; }
    }
    /// <summary>
    /// List of sales representatives to send offers to the sales rep
    /// </summary>
    public class SalesRepresentative
    {
        public string email { get; set; }
        public string salesrepname { get; set; }
    }

    public class MinPrice
    {
        public string score { get; set; }
        public long maxtime { get; set; }
        public double minprice { get; set; }

    }
    
}