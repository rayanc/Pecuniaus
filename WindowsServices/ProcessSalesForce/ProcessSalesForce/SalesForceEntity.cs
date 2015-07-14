using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSalesForce
{
    public class SalesForceEntity
    {
        public string merchantName { get; set; }
        public string businessName { get; set; }
        public string legalName { get; set; }
        public string ownerName { get; set; }
        public string rnc { get; set; }
        public Int16 isSynced { get; set; }
        public DateTime businessStartDate { get; set; }
        public Int64 businessTypeId { get; set; }
        public int industryTypeid { get; set; }
        public int salesRepId { get; set; }
        public int userId { get; set; }
    }
}
