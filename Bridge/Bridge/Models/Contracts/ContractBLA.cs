using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class ContractBLA
    {        
        public string contractNumber { get; set; }
        public string salesRepName { get; set; }
        public string salesRepId { get; set; }
        public DateTime date { get; set; }
        public string companyName { get; set; }
        public string businessName { get; set; }
        public string orgCountry { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string legalAddress { get; set; }
        public string legalProvince { get; set; }
        public string legalCountry { get; set; }        
        public DateTime businessStartDate { get; set; }
        public string RNCNumber { get; set; }
        public string ownerName { get; set; }
        //-- ID number
        public string jobTitle { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string bankName { get; set; }
        public string bankCity { get; set; }
        public string bankCountry { get; set; }
        public string mcaAmount { get; set; }
        public string totalOwnedAount { get; set; }
        public string retention { get; set; }
        public string expenseAmount { get; set; }
        public List<EntityType> EntityList { get; set; }

        public Int64 businessTypeId { get; set; }
        public List<OwnerList> ownerList { get; set; }
       
    }

    public class EntityType
    {
        public string entityTypeId { get; set; }
        public string entityName { get; set; }       
    }

    public class OwnerList
    {
        public OwnerList()
        {
            Address = new Address();
        }
        public string name { get; set; }
        public string jobtitle { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string PassportNbr { get; set; }
        public Address Address { get; set; }
    }
}