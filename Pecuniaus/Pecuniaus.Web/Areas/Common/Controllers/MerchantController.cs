using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using System.Web.Mvc;

namespace Pecuniaus.Common.Controllers
{
    public class MerchantController : Controller
    {
        //      [HttpGet]
        public ActionResult Info(int merchantID,int taskTypeId=0 ,int contractId=0)
        {
            if (merchantID > 0)
            {
                //var apiMethod = string.Format("merchants/merchantinfo/{0}", merchantID);
                var apiMethod = string.Format("merchants/merchantinfo/{0}?tasktypeId={1}&contractId={2}", merchantID, taskTypeId, contractId);

                var model = BaseApiData.GetAPIResult<MerchantModel>(apiMethod, () => new MerchantModel()) ?? new MerchantModel();
                model.MerchantID = merchantID;
                return PartialView("_MerchantInfo", model);
            }
            return PartialView("_MerchantInfo", new MerchantModel());
        }
    }
}