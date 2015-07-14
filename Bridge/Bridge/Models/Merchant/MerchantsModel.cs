using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MerchantsModel
    {
        public Int64 merchantId { get; set; }
        public string merchantName { get; set; }
        public string businessName { get; set; }
        public string legalName { get; set; }
        public string rnc { get; set; }
        public string mccCode { get; set; }
        public int ownerId { get; set; }
        public Int16 isSynced { get; set; }
        public DateTime businessStartDate { get; set; }
        public string businessWebSite { get; set; }
        public int rentFlag { get; set; }
        public Int64 businessTypeId { get; set; }
        public double rentAmount { get; set; }
        public double pendingAmount { get; set; } 
        public double annualSales { get; set; }
        public Int32 industryTypeId { get; set; }
        public int propertyType { get; set; }
        public int processorCompany { get; set; }
        public int affiliationNumber { get; set; }
        public int CNetProcessorId { get; set; }
        public int VNetProcessoId { get; set; }
        public int salesRepId { get; set; }
        public string assignedSalesRep { get; set; }
        public int companyId { get; set; }
        public string ownerName { get; set; }
        public string phoneNumber { get; set; }
        public string OwnerAddress { get; set; }
        public Address address { get; set; }
        public Int64 taskTypeId { get; set; }
        public Int64 taskStatusId { get; set; }

        public Int64 merchantStatusId { get; set; }
        public Int64 contractStatusId { get; set; }

        public Int64 ContractId { get; set; }

        public string merchantStatus { get; set; }

        public string contractStatus { get; set; }

        public ProcessorModel processor { get; set; }
        public string SSN { get; set; }
        public string TypeofBusinessentity { get; set; }

        public double ownedamount { get; set; }

        public double paidamount { get; set; }

        public double balanceamount { get; set; }

        public string assignedUser { get; set; }

        public DateTime CompletionDate { get; set; }

        public DateTime AssignedDate { get; set; }
        public string industrytype { get; set; }

        public string merchantpropertytype { get; set; }    

    }
}