
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.Web.Models;
using System.Web.Script.Serialization;
using Pecuniaus.Web.HelperClasses;
using System.Net.Http;
using Pecuniaus.UICore.Controllers;

namespace Pecuniaus.Web.Controllers
{
    public class MerchantController : UiBaseController
    {
        public ActionResult Index()
        {
            //Setting UserID
            string Querymerchantid = Request.QueryString["dupmerchantid"] ?? "";
            if (!string.IsNullOrEmpty(Querymerchantid) && Querymerchantid != "0")
            {
                Session["dupmerchantid"] = Querymerchantid;
            }
            ViewBag.BaseURL = System.Configuration.ConfigurationManager.AppSettings["APIURI"];
            IEnumerable<GeneralModel> Industries = new CommonFunctions().RetrieveGeneralTypes("1001");
            ViewBag.IndustryTypes = new SelectList(Industries, "keyId", "description");
            IEnumerable<SalesRepresentativeModel> SalesRep = new CommonFunctions().RetrieveSalesRep("");
            var selectedSalesResp = SalesRep.FirstOrDefault(a => a.UserId == base.CurrentUserID);
            long seleSalesRepId = 0;
            if (selectedSalesResp != null)
            {
                seleSalesRepId = selectedSalesResp.salesRepId;
            }

            ViewBag.SalesRep = new SelectList(SalesRep, "salesRepId", "fullName", seleSalesRepId);
            ViewBag.APIURI = System.Configuration.ConfigurationManager.AppSettings["APIURI"];

            if (base.CurrentMerchantID != 0)
            {
                MerchantsModel result = GetMerchantInfo(base.CurrentMerchantID);
                return View("Index", result);
            }

            else
                return View(new MerchantsModel { merchantId = 0 });
        }

        public ActionResult MerchantListing()
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/queue");
            objRequest.Method = "Get";

            IList<MerchantTempModel> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<MerchantTempModel>>(reader.ReadToEnd());
            }

            //Store merchants in Temp Storage 
            Session["SFMerchants"] = new JavaScriptSerializer().Serialize(objBE);

            return PartialView(objBE);
        }

        public JsonResult SearchMerchantInfo(string merchantId)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/merchantinfo/" + merchantId.ToString());
            objRequest.Method = "Get";

            MerchantsModel objBE = null;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<MerchantsModel>(reader.ReadToEnd());
                //new SessionManager().CurrentMerchantID = merchantId;
            }

            return Json(objBE, JsonRequestBehavior.AllowGet);
        }

        public MerchantsModel GetMerchantInfo(long merchantId)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/merchantinfo/" + merchantId.ToString());
            objRequest.Method = "Get";

            MerchantsModel objBE = null;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<MerchantsModel>(reader.ReadToEnd());
                //  new SessionManager().CurrentMerchantID = merchantId;
            }

            return (objBE);
        }


        public ActionResult SearchDuplicateMerchant(string rnc, string businessName, string legalName, string ownerName, string searchtype)
        {
            StringBuilder query = new StringBuilder();
            if (businessName != "")
            {
                if (query.ToString() == "")
                    query.Append("?businessName=" + businessName.ToString());
                else
                    query.Append("&businessName=" + businessName.ToString());
            }
            if (rnc != "")
            {
                if (query.ToString() == "")
                    query.Append("?rnc=" + rnc.ToString());
                else
                    query.Append("&rnc=" + rnc.ToString());
            }
            if (legalName != "")
            {
                if (query.ToString() == "")
                    query.Append("?legalName=" + legalName.ToString());
                else
                    query.Append("&legalName=" + legalName.ToString());
            }
            if (ownerName != "")
            {
                if (query.ToString() == "")
                    query.Append("?ownerName=" + ownerName.ToString());
                else
                    query.Append("&ownerName=" + ownerName.ToString());
            }
            if (query.ToString() == "")

                query.Append("?SearchType=D");
            else
                query.Append("&SearchType=D");

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/searchResult" + query.ToString());
            //HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/search?legalname=" + legalName);
            objRequest.Method = "Get";

            IList<MerchantsModel> objBE = null;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<MerchantsModel>>(reader.ReadToEnd());
            }

            return PartialView("MerchantSearch", objBE);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Complete")]
        public ActionResult CompleteMerchant(MerchantsModel obj, FormCollection obj1)
        {
            ViewBag.BusinessTypes = new CommonFunctions().RetrieveGeneralTypes("1002");

            //ViewBag.BusinessTypes = new CommonFunctions().RetrieveGeneralTypes("1002");
            //IEnumerable<GeneralModel> gm = ((List<GeneralModel>)ViewBag.BusinessTypes).Where(o => o.keyId == obj.businessTypeId);
            if (ModelState.IsValid)
            {
                var merchantid = Request["merchantid"] ?? "";

                MerchantTempModel objMerchant = new MerchantTempModel();
                objMerchant.businessName = obj.businessName;
                objMerchant.legalName = obj.legalName;
                objMerchant.businessTypeId = obj.businessTypeId;
                objMerchant.businessStartDate = Convert.ToDateTime(obj.businessStartDate);
                objMerchant.rnc = obj.rnc;
                objMerchant.industryTypeid = Convert.ToInt32(obj.industryTypeId);
                objMerchant.ownerName = obj.ownerName;
                objMerchant.salesRepId = Convert.ToInt32(obj.salesRepId);
                objMerchant.userId = CurrentUserID;
                string merchant = null;
                if (Session["dupmerchantid"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["dupmerchantid"].ToString()) && Session["dupmerchantid"].ToString() != "0")
                    {
                        objMerchant.IsDuplicate = true;
                    }
                    else
                        objMerchant.IsDuplicate = false;
                }
                else
                    objMerchant.IsDuplicate = false;

                using (var client = new HttpClient())
                {

                    string baseAddress = System.Configuration.ConfigurationManager.AppSettings["APIURI"];
                    client.BaseAddress = new Uri(baseAddress);
                    string restResponse = "merchants/temp/" + CurrentMerchantID + "?isCompleted=1";
                    var response = client.PutAsJsonAsync(restResponse, objMerchant).Result;
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    merchant = values["merchantid"];
                    if (response.StatusCode == HttpStatusCode.OK)
                        ViewBag.SuccessMsg = "Merchant Completed Successfully";
                }
                //string url = string.Format("?merchantid=" + CurrentMerchantID);
                //return Redirect(url);

                long uMerchatntId;
                long.TryParse(merchant, out uMerchatntId);
                Pecuniaus.UICore.SessionHelper.SetCurrentMerchant(uMerchatntId);
                Session["dupmerchantid"] = "0";
                return RedirectToAction("Index", "Merchant", new { merchantid = merchant });
            }

            IEnumerable<GeneralModel> Industries = new CommonFunctions().RetrieveGeneralTypes("1001");
            ViewBag.IndustryTypes = new SelectList(Industries, "keyId", "description");
            IEnumerable<SalesRepresentativeModel> SalesRep = new CommonFunctions().RetrieveSalesRep("");
            ViewBag.SalesRep = new SelectList(SalesRep, "salesRepId", "fullName");

            return View("Index");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult SaveMerchant(MerchantsModel obj, FormCollection obj1)
        {
            ViewBag.BusinessTypes = new CommonFunctions().RetrieveGeneralTypes("1002");

            if (ModelState.IsValid)
            {

                MerchantTempModel objMerchant = new MerchantTempModel();
                objMerchant.businessName = obj.businessName;
                objMerchant.legalName = obj.legalName;
                objMerchant.businessTypeId = obj.businessTypeId; //Convert.ToInt64(((List<GeneralModel>)ViewBag.BusinessTypes).Where(o => o.description == obj1["TypeofBusinessentity"]).FirstOrDefault().keyId);
                objMerchant.businessStartDate = Convert.ToDateTime(obj.businessStartDate);
                objMerchant.rnc = obj.rnc;
                objMerchant.industryTypeid = Convert.ToInt32(obj.industryTypeId);
                objMerchant.ownerName = obj.ownerName;
                objMerchant.salesRepId = Convert.ToInt32(obj.salesRepId);
                objMerchant.userId = CurrentUserID;

                if (Session["dupmerchantid"]!=null)
                {
                    if (!string.IsNullOrEmpty(Session["dupmerchantid"].ToString()) && Session["dupmerchantid"].ToString() != "0")
                    {
                        objMerchant.IsDuplicate = true;
                    }
                    else
                        objMerchant.IsDuplicate = false;
                }
                else
                    objMerchant.IsDuplicate = false;

                string merchant = null;
                using (var client = new HttpClient())
                {
                    string restResponse;
                    string baseAddress = System.Configuration.ConfigurationManager.AppSettings["APIURI"];
                    client.BaseAddress = new Uri(baseAddress);

                    restResponse = "merchants/temp/" + (string.IsNullOrEmpty(CurrentMerchantID.ToString()) ? "0" : CurrentMerchantID.ToString()) + "?isCompleted=0";

                    var response = client.PutAsJsonAsync(restResponse, objMerchant).Result;
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    merchant = obj.merchantId == 0 ? values["merchantid"] : obj.merchantId.ToString();
                    if (response.StatusCode == HttpStatusCode.OK)
                        ViewBag.SuccessMsg = "Merchant Saved Successfully";

                }

                //    string url = string.Format("?merchantid=" + merchant);
                base.SetSuccessMessage("Data Updated");

                long uMerchatntId;
                long.TryParse(merchant, out uMerchatntId);
                Pecuniaus.UICore.SessionHelper.SetCurrentMerchant(uMerchatntId);

                //return RedirectToAction("Index", "Merchant", new { merchantid = merchant });


                return RedirectToAction("Index", "Merchant", new { merchantid = merchant });
            }

            IEnumerable<GeneralModel> Industries = new CommonFunctions().RetrieveGeneralTypes("1001");
            ViewBag.IndustryTypes = new SelectList(Industries, "keyId", "description");
            IEnumerable<SalesRepresentativeModel> SalesRep = new CommonFunctions().RetrieveSalesRep("");
            ViewBag.SalesRep = new SelectList(SalesRep, "salesRepId", "fullName");

            return View("Index");

        }

        //[HttpPost]
        //[MultipleButton(Name = "action", Argument = "Decline")]
        //public ActionResult DeclineMerchant(MerchantsModel objMerchant)
        //{
        //    IEnumerable<GeneralModel> Industries = new CommonFunctions().RetrieveGeneralTypes("1001");
        //    ViewBag.IndustryTypes = new SelectList(Industries, "keyId", "description");
        //    IEnumerable<SalesRepresentativeModel> SalesRep = new CommonFunctions().RetrieveSalesRep("");
        //    ViewBag.SalesRep = new SelectList(SalesRep, "salesRepId", "fullName");
        //    return View("Index");
        //}

        public ActionResult MerchantDetails()
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/renewals");
            objRequest.Method = "Get";

            IList<MerchantsDetail> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<MerchantsDetail>>(reader.ReadToEnd());
            }
            return PartialView("MerchantDetails", objBE);
        }

        [HttpGet]
        public ActionResult Snooze()
        {
            return PartialView("Snooze");
        }

        [HttpGet]
        public ActionResult Decline(long merchantId, long contractId)
        {
            return PartialView("_Decline", new DeclineModel
            {
                WorkflowId = UICore.SessionHelper.GetWorkFlowID(),
                DeclineReasons = Pecuniaus.ApiHelper.CommonFunctions.GetDeclineReasons(),
                ContractID = contractId,
                merchantId = UICore.SessionHelper.CurrentMerchantID,
                Screen = UICore.SessionHelper.GetScreenName()
            });
        }

        [HttpPost]
        public ActionResult Decline(DeclineModel model)
        {
            Pecuniaus.ApiHelper.BaseApiData.PostAPIData("contracts/decline/" + model.ContractID, model);
            SetSuccessMessage("Declined");
            return Json("OK");
        }
    }
}