using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bridge.Models
{
    /// <summary>
    /// Collections model for the c
    /// </summary>
    public class CollectionsModel
    {
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public IList<CollectionContractInformation> ContractInfo { get; set; }
        public List<PreventionModel> Prevention { get; set; }
        public List<CollectionDaysModel> CollectionDays { get; set; }
        public IList<LawyerModel> AssignedLawyers { get; set; }
        public List<ActiveContracts> RNCActiveContracts { get; set; }
        public List<InActiveContracts> RNCInActiveContracts { get; set; }
        public List<ActiveContracts> OwnerActiveContracts { get; set; }        
    }
    public class CollectionContractInformation
    {
        public string collectionStatus { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public DateTime fundingDate { get; set; }
        public double monthlyAverage { get; set; }
        public double CCAverageOffer { get; set; }
        public double AEC { get; set; }
        public double ownedAmount { get; set; }
        public double retention { get; set; }
        public double time { get; set; }
        public double pendingAmount { get; set; }

    }
    public class PreventionModel    {
        
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }        
        public string merchantName { get; set; }
        public double pendingAmount { get; set; }
        public DateTime fundingDate { get; set; }
        public double expectedTurn { get; set; }
        public double realTurn { get; set; }
        public double percentage { get; set; }        
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
        public Int64 affiliationId { get; set; }
        public string merchantName { get; set; }
        public Int64 merchantId { get; set; }
        public double aecAmount { get; set; }
        public double ownedAmount { get; set; }
        public double pendingAmount { get; set; }
        public double retentionTime { get; set; }
        public DateTime fundingDate { get; set; }
        public string merchantStatus { get; set; }
    }

    public class InActiveContracts
    {
        public Int64 merchantId { get; set; }
        public Int64 affiliationId { get; set; }
        public string merchantName { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime lastDateofEvaluation { get; set; }
        public string requestedCCVolumes { get; set; }
        public string volumeStatus { get; set; }        
    }
   
    public class CollectionActivityModel
    {
        public int activityTypeId { get; set; }
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
    public class DropDown
    {
        public int keyId { get; set; }
        public string value { get; set; }
    }
    public class CollectionLandlordModel
    {
        public Int64 merchantId { get; set; }
        public string typeOfPropertyId { get; set; }
        public string landlordCompanyName { get; set; }
        public string landlordFirstName { get; set; }
        public string landlordLastName { get; set; }
        public string monthlyRentAmount { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string telePhone { get; set; }
        public string cellPhone { get; set; }       
        public string email { get; set; }
        public Int64 userId { get; set; }
    }
    public class CollectionActivities
    {
        public bool pNotifyPayment { get; set; }
        public bool pPaymentAgreement { get; set; }
    }
}