using Pecuniaus.ApiHelper;
using Pecuniaus.Contract.Models;
using Pecuniaus.Models.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Contract.Repository
{
    public class ProcessorSessionRepository
    {
        private readonly string SessionProcessList = "_ProcessList";

        public List<ProcessorModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionProcessList] != null)
                return (List<ProcessorModel>)HttpContext.Current.Session[SessionProcessList];
            return new List<ProcessorModel>();
        }

        public void Set(List<ProcessorModel> processors)
        {
            if (processors != null)
                foreach (var p in processors)
                {
                    if (p.ID == 0)
                        p.ID = processors.Max(a => a.ID) + 1;
                }

            HttpContext.Current.Session[SessionProcessList] = processors;
        }

        public void Add(ProcessorModel processor)
        {
            var data = GetAll();

            var processors = CommonFunctions.GetProcessors();

            var p = processors.FirstOrDefault(a => a.KeyId == processor.ProcessorTypeId);
            if (p != null)
                processor.ProcessorName = p.Description;

            if (processor.ID == 0)
            {
                if (data.Count > 0)
                    processor.ID = data.Max(a => a.ID) + 1;
                else
                    processor.ID = 100000;
            }

            data.Add(processor);
            HttpContext.Current.Session[SessionProcessList] = data;
        }

        public void Update(ProcessorModel processor)
        {
            Delete(processor.ID);
            Add(processor);
        }

        public void Delete(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.ID == id).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionProcessList] = data;
            }
        }

        public ProcessorModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.ID == id).FirstOrDefault();
        }
    }
}