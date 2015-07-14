using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Collection.Models
{
    public class CollectionsModel
    {       
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public Int64? PreventionSearch { get; set; }
        public bool isCollectionGrid { get; set; }
        public IList<CollectionContractInformation> ContractInfo { get; set; }
        public IList<OwnerColModel> OwnerInfo { get; set; }
        public IList<PreventionModel> Prevention { get; set; }
        public IEnumerable<SelectListItem> listDays { get; set; }
        public Int64? listDaysActivity { get; set; }
        public IList<CollectionDaysModel> CollectionDays { get; set; }
        public IList<ActiveContracts> RNCActiveContracts { get; set; }
        public IList<InActiveContracts> RNCInActiveContracts { get; set; }
        public IList<ActiveContracts> OwnerActiveContracts { get; set; }
        public IList<InActiveContracts> OwnerInActiveContracts { get; set; }
        public IList<MerchantDocumentModel> MerchantDoc { get; set; }
        public IList<LawyerModel> AssignedLawyers { get; set; }
        public IList<LawyerModel> LawyersToAssign { get; set; }
        public IList<LegalDocuments> LawyerDocuments { get; set; }
        //public IList<CollectionLandLordModel> CollectionLandLord { get; set; }
        public CollectionLandLordModel CollectionLandLord { get; set; }
        public IList<CollectionPaymentAgreementModel> LstCollectionPaymentAgreement { get; set; }
        public CollectionPaymentAgreementModel CollectionPaymentAgreement { get; set; }
        public MerchantsDetail MerchantsDetail { get; set; }
        public IEnumerable<SelectListItem> listActivities { get; set; }
        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
        public CollectionActivityModel CollectionActivity { get; set; }
        public CreditPullModel CreditPull { get; set; }
        public IList<CollectionActivities> ColActivities { get; set; }
    }

    public class CollectionContractInformation
    {
        private DateTime dtVal;
        public string collectionStatus { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public string fundingDate
        {
            get { return dtVal.ToShortDateString(); }
            set { dtVal = Convert.ToDateTime(value); }
        }
        public double monthlyAverage { get; set; }
        public double CCAverageOffer { get; set; }
        public double AEC { get; set; }
        public double ownedAmount { get; set; }
        public double retention { get; set; }
        public double time { get; set; }
        public double pendingAmount { get; set; }

    }
    public class PreventionModel
    {
        private DateTime dtVal;
        private double valPer;
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }        
        public string merchantName { get; set; }
        public double pendingAmount { get; set; }
        public string fundingDate
        {
            get { return dtVal.ToShortDateString(); }
            set { dtVal = Convert.ToDateTime(value); }
        }
        public double expectedTurn { get; set; }
        public double realTurn { get; set; }
        public string percentage
        {
            get { return valPer.ToString("##.##"); }
            set { valPer = Convert.ToDouble(value); }
        }
    }
    public class CollectionDaysModel
    {
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public string merchantName { get; set; }
        public double ownedAmount { get; set; }
        public double pendingAmount { get; set; }
        public double expectedTurn { get; set; }
        public double realTurn { get; set; }
        public double retention { get; set; }
        public double averagerccsalesCurrent { get; set; }
        public double averageccsalesOffered { get; set; }
        public int dayswithoutactivity { get; set; }
        public bool redColHighlight { get; set; }
    }
    public class ActiveContracts
    {
        public DateTime dtFunding;
        public Int64 affiliationId { get; set; }
        public string merchantName { get; set; }
        public Int64 merchantId { get; set; }
        public double aecAmount { get; set; }
        public double ownedAmount { get; set; }
        public double pendingAmount { get; set; }
        public double retentionTime { get; set; }
        public string fundingDate
        {
            get { return dtFunding.ToShortDateString(); }
            set { dtFunding = Convert.ToDateTime(value); }
        }
        public string merchantStatus { get; set; }
    }

    public class InActiveContracts
    {
        public DateTime dtCrt;
        public DateTime dtLde;
        public Int64 merchantId { get; set; }
        public Int64 affiliationId { get; set; }
        public string merchantName { get; set; }
        public string creationDate
        {
            get { return dtCrt.ToShortDateString(); }
            set { dtCrt = Convert.ToDateTime(value); }
        }
        public string lastDateofEvaluation
        {
            get { return dtLde.ToShortDateString(); }
            set { dtLde = Convert.ToDateTime(value); }
        }        
        public string requestedCCVolumes { get; set; }
        public string volumeStatus { get; set; }
    }
    public class CollectionActivityModel
    {
        [Required]
        [Display(Name = "Activity", ResourceType = typeof(Resources.Collection.Collection))]
        public int activityTypeId { get; set; }

        [Required]
        [Display(Name = "Note", ResourceType = typeof(Resources.Collection.Collection))]
        public string notes { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public Int64 insertUserId { get; set; }
    }

    public class OwnerColModel
    {
        public string ownerName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string telephone { get; set; }
        public string cellPhone { get; set; }
    }
    public class ColGeneralModel
    {
        public long keyId { get; set; }
        public string description { get; set; }
    }
    public class DropDown
    {
        public int keyId { get; set; }
        public string value { get; set; }
    }
    public class CollectionActivities
    {
        public bool pNotifyPayment { get; set; }
        public bool pPaymentAgreement { get; set; }
    }
}