using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantCollectionDetailModel
    {
        public MPMerchantCollectionDetailModel()
        {
            CollectionDetail = new List<MPMerchantCollectionModel>();
        }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public IList<MPMerchantCollectionModel> CollectionDetail { get; set; }
    }
}