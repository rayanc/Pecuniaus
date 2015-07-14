using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class ContractIOU
    {
        public string LegalCompanyName { get; set; }

        public string RNC { get; set; }

        public string CompanyAddress { get; set; }

        public long stateId { get; set; }

        public long businesTypeId { get; set; }

        public string province { get; set; }

        public DateTime FundingDate { get; set; }

        public DateTime LetterDate { get; set; }

        public double TotalOwnedAmount { get; set; }
        public List<IOUOwnerList> ownerList { get; set; }

    }


    public class IOUOwnerList
    {
        public string name { get; set; }
        public string ID { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool IsAuthorised { get; set; }

    }
}