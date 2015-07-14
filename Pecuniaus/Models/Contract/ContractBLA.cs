using System;
using System.Collections.Generic;

namespace Pecuniaus.Models.Contract
{
    public class ContractBLA
    {
        public string ContractNumber { get; set; }
        public string SalesRepName { get; set; }
        public string SalesRepId { get; set; }
        public DateTime Date { get; set; }
        public string CompanyName { get; set; }
        public string BusinessName { get; set; }
        public string OrgCountry { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string LegalAddress { get; set; }
        public string LegalProvince { get; set; }
        public string LegalCountry { get; set; }
        public DateTime BusinessStartDate { get; set; }
        public string RNCNumber { get; set; }
        public string OwnerName { get; set; }
    
        public string JobTitle { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string BankCity { get; set; }
        public string BankCountry { get; set; }
        public string McaAmount { get; set; }
        public string TotalOwnedAount { get; set; }
        public string Retention { get; set; }
        public string ExpenseAmount { get; set; }

        public long BusinessTypeId { get; set; }
        public List<EntityType> EntityList { get; set; }

        public List<OwnerList> OwnerList { get; set; }

    }

    public class EntityType
    {
        public string EntityTypeId { get; set; }
        public string EntityName { get; set; }
    }

    public class OwnerList
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string PassportNbr { get; set; }
        public Address Address { get; set; }
   
    }
}
