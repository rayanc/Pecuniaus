using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MerchantsModel
    {
        public Int64 merchantId { get; set; }
        public string merchantName { get; set; }
        public string businessName { get; set; }
        public string legalName { get; set; }
        public string ownerName { get; set; }
        public string rnc { get; set; }
        public string mccCode { get; set; }
        public int ownerId { get; set; }
        public Int16 isSynced { get; set; }

        [DataType(DataType.Date)]
        public DateTime? businessStartDate { get; set; }
        public string businessWebSite { get; set; }
        public int rentFlag { get; set; }
        public Int64 businessTypeId { get; set; }
        public double rentAmount { get; set; }
        public double annualSales { get; set; }
        public Int32 industryTypeId { get; set; }
        public int propertyType { get; set; }
        public int processorCompany { get; set; }
        public int affiliationNumber { get; set; }
        public int CNetProcessorId { get; set; }
        public int VNetProcessoId { get; set; }
        public int salesRepId { get; set; }
        //public int companyId { get; set; }
        //public Address address { get; set; }
        //public ProcessorModel processor { get; set; }
        //public IEnumerable<GeneralModel> IndustriesList { get; set; }
        //public IEnumerable<GeneralModel> BusinessEntityList { get; set; }
    }
}