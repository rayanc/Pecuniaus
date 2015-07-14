using Bridge.Models;
using Bridge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.BusinessTier
{
    public interface IGeneral: IRepository<GeneralModel,int>
    {
        IList<GeneralModel> FindBy(dynamic query);
        bool SetSnooze(Int64 contractId, double percentPaid, DateTime snoozeDate);
        bool Kickback(Int64 merchantId, Int64 tasktypeId, Int64 workflowId, Int64 contractid);
    }
}
