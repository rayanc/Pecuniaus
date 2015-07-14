using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantOwnersModel
    {
        public Int64 OwnerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OwnerIdentification { get; set; }

        public DateTime DateofBirth { get; set; }

        public DateTime DateBecameOwner { get; set; }

        public bool IsAuthorized { get; set; }
    }
}