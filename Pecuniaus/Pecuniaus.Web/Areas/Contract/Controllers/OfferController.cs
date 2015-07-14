using Pecuniaus.ApiHelper;
using Pecuniaus.Contract.Models;
using Pecuniaus.Contract.Repository;
using Pecuniaus.Models.Contract;
using Pecuniaus.UICore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Contract.Controllers
{
    public class OfferController : BaseController
    {
          //
        // GET: /OfferCreation/
        MerchantApi merchantApi;
      //  OfferSesssionRepository offerSesssionRepository;


        public OfferController()
        {
            merchantApi = new MerchantApi();
       //     offerSesssionRepository = new OfferSesssionRepository();
        }

        public ActionResult Index()
        {
            MerchantInformationOfferModel model = merchantApi.GetOfferDetails(CurrentMerchantID, ContractID);
            if (!string.IsNullOrEmpty(model.reason))
                model.AddManulMccv = true;
            //if (model != null)
            //    Session["maxvalue"] = model.yearlysales;
            //else
            //    Session["maxvalue"] = "0";

          //  offerSesssionRepository.Set(model.offers);
            return View(model);

        }

        [HttpPost]
        public ActionResult SendEmail(string[] SelectedEmail)
        {
            Utilities.Email.Emailer obj = new Utilities.Email.Emailer();
            if (SelectedEmail.Count() > 0)
            {
                string emailRecipent = String.Join(",", SelectedEmail);
                string emailBody = PrepareBody();
                obj.Send(emailRecipent, "Testmails", emailBody, string.Empty);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Index(MerchantInformationOfferModel model, string button)
        {
            var isComplete = 0;
            if (button == "Complete")
            {
                isComplete = 1;
            }

            //model.TypeOfPropertyID = 1;
            if (ModelState.IsValid)
            {
                ///Save Data      
               // model.offers = offerSesssionRepository.GetAll();

                if (isComplete == 0)
                {
                    var apiMethod = string.Format("CreditReport/offers/create?merchantId={0}&contractId={1}&isCompleted={2}", CurrentMerchantID, ContractID, isComplete);
                    BaseApiData.PostAPIData(apiMethod, model);
                }
                else
                {
                    var apiMethod = string.Format("CreditReport/offers/create?merchantId={0}&contractId={1}&isCompleted={2}", CurrentMerchantID, ContractID, isComplete);
                    BaseApiData.PostAPIData(apiMethod, model);

                    string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
            CurrentMerchantID, (int)TaskTypes.PQOfferCreation, 1, ContractID);
                    BaseApiData.GetAPIResult<object>(apiData, () => new object());
                }

                if (isComplete == 1)
                {
                    base.SetSuccessMessage("Offer - Task Completed.");
                }
                else
                {
                    base.SetSuccessMessage("Data Updated.");

                }
                return RedirectToAction("Index");
            }

            return View(model);

        }

        //public ActionResult SendEmail()
        //{
        //    Pecuniaus.Models.Prequel.Email objEmail = new Models.Prequel.Email();
        //    var apiMethod = string.Format("merchants/GetEmail?SalesRepId={0}", CurrentMerchantID);
        //    var model = BaseApiData.GetAPIResult<List<EmailModel>>(apiMethod, () => new List<EmailModel>()) ?? new List<EmailModel>();
        //    var eml = new List<SelectListItem>();
        //    objEmail.EmailList = from i in model
        //                         select new SelectListItem { Text = i.Email, Value = i.Email };

        //    return PartialView("_SendEmail", objEmail);
        //}

        public string PrepareBody()
        {

            //System.Text.StringBuilder body = new System.Text.StringBuilder();
            //using (StreamReader reader = new StreamReader(Server.MapPath(@"..\..\Templates\OfferTemplate.html")))
            //{
            //    body.Append(reader.ReadToEnd());
            //}
            //if (!String.IsNullOrEmpty(body.ToString()))
            //{
            //    var apiMethod = string.Format("merchants/merchantinfo/{0}", CurrentMerchantID);
            //    var model = BaseApiData.GetAPIResult<MerchantModel>(apiMethod, () => new MerchantModel()) ?? new MerchantModel();
            //    model.MerchantID = CurrentMerchantID;

            //    body.Replace("{XXX}", model.MerchantID.ToString());
            //    body.Replace("{MerchantName}", model.MerchantName);
            //    body.Replace("{SaleRep}", model.AssignedSalesRep);
            //    body.Replace("{Owner}", model.OwnerName);
            //    body.Replace("{OfferDate}", DateTime.Now.ToShortDateString()); //Dummy
            //    body.Replace("{ExpirationDate}", DateTime.Now.ToShortDateString());  //Dummy

            //    List<OfferModel> objOffers = offerSesssionRepository.GetAll();
            //    if (objOffers.Count > 0)
            //    {
            //        int count = 1;
            //        System.Text.StringBuilder strOffer = new System.Text.StringBuilder(); ;
            //        foreach (OfferModel offer in objOffers)
            //        {
            //            strOffer.Append("Offer: " + count.ToString() + "<br/>");
            //            strOffer.Append("MCA Amount: " + offer.loanAmount + "<br/>");
            //            strOffer.Append("Owen Amount: " + offer.ownedAmount + "<br/>");
            //            strOffer.Append("Price: " + offer.maxprice + "<br/>"); //dummy
            //            strOffer.Append("% Retention: " + offer.retention + "<br/>");
            //            strOffer.Append("Repayment Time (months): " + offer.offerexpirationDate.ToString("MMMM", CultureInfo.InvariantCulture) + "<br/><br/>"); //dummy                       
            //            count++;
            //        }
            //        body.Replace("{OfferList}", strOffer.ToString());
            //    }
            //    else
            //        body.Replace("{OfferList}", "No Offer Available");
            //}

            //return body.ToString();
            return string.Empty;
        }

        //[HttpPost]
        //public ActionResult GenratePDF(string email)
        //{
        //    List<OfferModel> offers = offerSesssionRepository.GetAll();
        //    return new Rotativa.PartialViewAsPdf("_ListOffer", offers) { FileName = "OfferPdf.pdf" };
        //}


        //public ActionResult ListOffers()
        //{
        //    List<OfferModel> offers = offerSesssionRepository.GetAll();
        //    return PartialView("_ListOffer", offers);
        //}

        //[HttpGet]
        //public ActionResult AddOffer()
        //{
        //    OfferModel mod = new OfferModel();
        //    //mod.MaxAnnualSalesAmount = Convert.ToDouble(Session["maxvalue"].ToString());
        //    //mod.MaxOwnedAmount = Convert.ToDouble(Session["maxvalue"].ToString()) * 2;
        //    return PartialView("_AddOffer", mod);
        //}

        //[HttpPost]
        //public ActionResult AddOffer(OfferModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        offerSesssionRepository.AddOffer(model);
        //        return RedirectToAction("AddOffer");
        //    }
        //    return PartialView("_AddOffer", model);
        //}

        //[HttpGet]
        //public ActionResult EditOffer(long id)
        //{
        //    var model = offerSesssionRepository.GetByID(id);
        //    //model.MaxAnnualSalesAmount = Convert.ToDouble(Session["maxvalue"].ToString());
        //    //model.MaxOwnedAmount = Convert.ToDouble(Session["maxvalue"].ToString()) * 2;
        //    model.IsUpdate = true;
        //    return PartialView("_AddOffer", model);
        //}

        //[HttpPost]
        //public ActionResult UpdateOffer(OfferModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        offerSesssionRepository.Update(model);
        //        return RedirectToAction("AddOffer");
        //    }

        //    return PartialView("_AddOffer", model);
        //}

        //[HttpPost]
        //public ActionResult DelOffer(long id)
        //{
        //    offerSesssionRepository.DelOffer(id);

        //    return Json("OK");
        //}
    }
}