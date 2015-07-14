using Pecuniaus.ApiHelper;
using Pecuniaus.Contract.Models;
using Pecuniaus.Models.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Contract.Repository
{
    public class TradeReferenceSessionRepository
    {
          private readonly string SessionTradeList = "_TradeReferenceList";

        public List<TradeReferenceModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionTradeList] != null)
                return (List<TradeReferenceModel>)HttpContext.Current.Session[SessionTradeList];
            return new List<TradeReferenceModel>();
        }

        public void Set(List<TradeReferenceModel> tradereference)
        {
            if (tradereference != null)
                foreach (var p in tradereference)
                {
                    if (p.Id == 0)
                        p.Id = tradereference.Max(a => a.Id) + 1;
                }

            HttpContext.Current.Session[SessionTradeList] = tradereference;
        }
        
        public void Add(TradeReferenceModel tradereference)
        {
           var data = GetAll();

            if (tradereference.Id == 0)
            {
                if (data.Count > 0)
                    tradereference.Id = data.Max(a => a.Id) + 1;
                else
                    tradereference.Id = 100000;
            }

            data.Add(tradereference);
            HttpContext.Current.Session[SessionTradeList] = data;
        }

        public void Update(TradeReferenceModel tradereference)
        {
            Delete(tradereference.Id);
            Add(tradereference);
        }

        public void Delete(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.Id == id).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionTradeList] = data;
            }
        }

        public TradeReferenceModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.Id == id).FirstOrDefault();
        }
        
    }
}