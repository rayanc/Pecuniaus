
using System;
namespace Pecuniaus.Models.Merchant
{
    public class MPMerchantOwnersModel
    {
        public long OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OwnerIdentification { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime DateBecameOwner { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
