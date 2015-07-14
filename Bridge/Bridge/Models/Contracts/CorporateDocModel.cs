using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class CorporateDocModel
    {
        public string nameOfCompany { get; set; }
        public string addressDesc { get; set; }
        public string RNCNumber { get; set; }
        public List<OwnerModel> OwnerList { get; set; }
        public string fileName { get; set; }
        public string fileDetails { get; set; }
    }

    //public class OwnerCorpModel
    //{
    //    public Int64 ownerId { get; set; }
    //    public string ownerName { get; set; }
    //    public string ownerLastName { get; set; }
    //    public string passportNbr { get; set; }
    //    public string telephone { get; set; }
    //    public bool isAuthorized { get; set; }
    //    public Int64 addressId { get; set; }
    //    public Int64 contactId { get; set; }
    //}
}