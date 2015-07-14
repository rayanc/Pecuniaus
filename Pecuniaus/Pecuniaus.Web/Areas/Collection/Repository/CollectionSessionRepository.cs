using Pecuniaus.Collection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Collection.Repository
{
    public class CollectionSessionRepository
    {
        private readonly string SessionCollectionList = "M_CollectionList";

        public MerchantsDetail GetAll()
        {
            if (HttpContext.Current.Session[SessionCollectionList] != null)
                return (MerchantsDetail)HttpContext.Current.Session[SessionCollectionList];
            return new MerchantsDetail();
        }

        public void Set(MerchantsDetail MerchantsDetail)
        {
            HttpContext.Current.Session[SessionCollectionList] = MerchantsDetail;
        }    

        public void ClearAll()
        {
            if (HttpContext.Current.Session[SessionCollectionList] != null)
                HttpContext.Current.Session[SessionCollectionList] = null;

        }
    }
}