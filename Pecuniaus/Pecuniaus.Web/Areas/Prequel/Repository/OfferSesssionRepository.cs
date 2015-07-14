using Pecuniaus.Models.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Prequel.Repository
{
    class OfferSesssionRepository
    {
        private readonly string SessionOfferList = "_Cont_OfferList";

        public List<OfferModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionOfferList] != null)
                return ((List<OfferModel>)HttpContext.Current.Session[SessionOfferList]).OrderBy(o=>o.Id).ToList();
            return new List<OfferModel>();
        }

        public void Set(List<OfferModel> offer)
        {
            if (offer != null)
            {
                foreach (var o in offer)
                {
                    if (o.Id == 0)
                        o.Id = offer.Max(a => a.Id) + 1;
                }
            }
            HttpContext.Current.Session[SessionOfferList] = offer;
        }

        public void AddOffer(OfferModel offer)
        {
            var data = GetAll();

            if (offer.Id == 0)
            {
                if (data.Count > 0)
                    offer.Id = data.Max(a => a.Id) + 1;
                else
                    offer.Id = 100000;
            }

            data.Add(offer);
            HttpContext.Current.Session[SessionOfferList] = data;
        }

        public void Update(OfferModel offer)
        {
            DelOffer(offer.Id);
            AddOffer(offer);
        }

        public void DelOffer(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.Id == id).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionOfferList] = data;
            }
        }

        public OfferModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.Id == id).FirstOrDefault();
        }


    }
}