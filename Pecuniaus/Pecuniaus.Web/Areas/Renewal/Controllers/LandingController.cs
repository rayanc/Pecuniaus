using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Newtonsoft.Json;
using Pecuniaus.Renewal.Models;
using Pecuniaus.UICore;


namespace Pecuniaus.Renewal.Controllers
{
    [Route("")]
    public class LandingController : BaseController
    {

        #region  Renewal functionality

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", GetRenewals(""));
        }


        /// <summary>
        /// Get renewal contract list
        /// </summary>
        /// <returns></returns>
        public IList<MerchantsDetail> GetRenewals(string filter)
        {
            return BaseApiData.GetAPIResult<IList<MerchantsDetail>>("renewals?filter=" + filter, () => new List<MerchantsDetail>());
        }

        /// <summary>
        /// Contract Information
        /// </summary>
        /// <param name="contractid"></param>
        /// <returns></returns>
        /// 
        public ActionResult ContractInformation(long contractid)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "renewals/contractInformation/" + contractid);
            objRequest.Method = "Get";

            MerchantsDetail objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<MerchantsDetail>(reader.ReadToEnd());
            }
            return PartialView("_ContractInformation", objBE);
        }
        #endregion

        #region snooze functionality
        /// <summary>
        /// Open Snooze partial view
        /// </summary>
        /// <returns></returns>

        public ActionResult snooze(long contractid)
        {
            SnoozeModel obj = BaseApiData.GetAPIResult<SnoozeModel>("renewals/snooze/" + Convert.ToString(contractid), () => new SnoozeModel());
            return PartialView("_Snooze", obj);
        }

        /// <summary>
        ///  Update Snooze
        /// </summary>
        /// <param name="Contractid"></param>
        /// <param name="snooze"></param>
        /// <param name="PercentPaid"></param>
        /// <returns></returns>       
        public ActionResult updateSnooze(SnoozeModel model)
        {
            if (ModelState.IsValid)
            {
                string URL = System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "renewals/snooze?contractid=" + model.contractID + "&snooze=" + model.snoozeDate + "&PercentPaid=" + model.snoozePercent;
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(URL);
                objRequest.Method = "Put";
                objRequest.ContentLength = 0;                
                var reponse = objRequest.GetResponse();
                base.SetSuccessMessage("Contract snooze successfully");
            }
            //return View("Index", GetRenewals(""));
            return Json("Snooze");
        }

        #endregion

        #region Decline functionality
        /// <summary>
        /// Open Decline Patial view
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult declineReason(Int64 contractid)
        {
            contractid = contractid == 0 ? ContractID : contractid;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "renewals/declinereasons");
            objRequest.Method = "GET";

            DeclineModel obj = new DeclineModel();

            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                obj.DeclineReasons = JsonConvert.DeserializeObject<IList<GeneralModel>>(reader.ReadToEnd());
            }
            obj.ContractID = contractid;
            obj.WorkflowId = (int)WorkflowID;
            return PartialView("_DeclineRenewal", obj);
        }
        /// <summary>
        /// Decline renewal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        public ActionResult decline(DeclineModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    model.WorkflowId = (int)WorkflowTypes.Renewal;
                    string apiMethod = System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "renewals/decline/" + model.ContractID;
                    var response = BaseApiData.PostAPIData(apiMethod, model);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        base.SetSuccessMessage("Contract decline successfully");
                        return Json("Added");                        
                    }
                    else
                    {
                        base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPOwnerUpdateFailure);
                        return PartialView("_DeclineRenewal", model);
                    }
                }
            }
            //return View("Index", GetRenewals(""));
            return Json("Decline Contract");
        }
        /// <summary>
        /// Complete renewal
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        /// 
        #endregion

        #region Complete functionality
        public ActionResult complete(Int64 ContractId, Int64 merchantId)
        {
            if (ContractId > 0)
            {
                SelectListItem obj = BaseApiData.GetAPIResult<SelectListItem>("renewals/complete/" + ContractId, () => new SelectListItem());

                if (obj != null)
                {
                    if (Convert.ToInt64(obj.Value) > 0)
                    {
                        SessionHelper.SetContractID(Convert.ToInt64(obj.Value));
                        SessionHelper.SetCurrentMerchant(merchantId);
                    }
                    base.SetSuccessMessage("Renewal task completed successfully");
                }
            }
            return PartialView("_RenewalListing");
        }


        public ActionResult Search()
        {

            return PartialView("Index", GetRenewals(Convert.ToString(ContractID)));
        }

        #endregion

    }
}