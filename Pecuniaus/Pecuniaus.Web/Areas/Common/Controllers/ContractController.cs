using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using Pecuniaus.UICore.Controllers;
using System.Web.Mvc;
using System.Configuration;

namespace Pecuniaus.Common.Controllers
{
    public class ContractController : UiBaseController
    {
        //
        // GET: /Contract/
        public ActionResult Decline(int workFlowID, long contractID)
        {
            return PartialView("_Decline", new DeclineModel
            {
                WorkflowId = workFlowID,
                DeclineReasons = CommonFunctions.GetDeclineReasons(),
                ContractID=contractID,
                merchantId = UICore.SessionHelper.CurrentMerchantID,
                Screen = UICore.SessionHelper.GetScreenName()
            });
        }

        [HttpPost]
        public ActionResult AddReason(DeclineModel model)
        {
            string DeclineLetterTemplateFileName = ConfigurationManager.AppSettings["DeclineLetterTemplate"];
            string Path = Server.MapPath("~") + "Templates";
            string EmailBody = System.IO.File.ReadAllText(Path + "\\" + DeclineLetterTemplateFileName).Replace("<DeclineReason>", model.Declinereason.ToString()).Replace("<DeclineText>", model.DeclineNotes);
            
            BaseApiData.PostAPIData("contracts/decline/"+ model.ContractID, model);
            SetSuccessMessage("Declined");
            return Json("OK");
        }
        
    }
}