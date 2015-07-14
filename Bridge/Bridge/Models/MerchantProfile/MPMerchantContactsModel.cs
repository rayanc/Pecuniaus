using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantContactsModel
    {
        public Int64 ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime DateofBirth { get; set; }
        public string IsOwner { get; set; }
        public string OwnerIdentification { get; set; }
    }
}