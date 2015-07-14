using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using Pecuniaus.Models.Contract;
using Pecuniaus.Prequel.Repository;
using Pecuniaus.UICore;
using Pecuniaus.UICore.Controllers;


namespace Pecuniaus.Prequel.Controllers
{
    public class OfferAcceptanceController : UiBaseController
    {
        MerchantApi merchantApi;
        OfferSesssionRepository offerSesssionRepository;

        public OfferAcceptanceController()
        {
            merchantApi = new MerchantApi();
            offerSesssionRepository = new OfferSesssionRepository();
        }

        public ActionResult Index()
        {
            MerchantInformationOfferModel model = merchantApi.GetOfferDetails(CurrentMerchantID, ContractID);
            offerSesssionRepository.Set(model.offers);
            var firstOffer = model.offers.FirstOrDefault();
            if (firstOffer != null)
            {
                model.SelectedOfferId = firstOffer.offerId;
            }

            SetOfferModifiedFlag(false);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index([Bind(Exclude = "SelectedOffer")] MerchantInformationOfferModel model, string button, long selectedOffer)
        {
            var isComplete = 0;
            if (button == "Complete")
            {
                isComplete = 1;
            }

            //var errorList = new List<string>();
            //foreach (var key in ModelState.Keys)
            //{
            //    ModelState modelState = null;
            //    if (ModelState.TryGetValue(key, out modelState))
            //    {
            //        foreach (var error in modelState.Errors)
            //        {
            //            errorList.Add(
            //                key + "==" + error.ErrorMessage
            //           );
            //        }
            //    }
            //}

            //model.TypeOfPropertyID = 1;
            if (ModelState.IsValid)
            {
                ///Save Data      
                // var offers = offerSesssionRepository.GetAll().FirstOrDefault(m => m.IsSelected = true);
                var offers = offerSesssionRepository.GetAll().FirstOrDefault(m => m.offerId == selectedOffer);
                if (offers == null)
                {
                    offers = new OfferModel();
                    model.offers = new List<OfferModel> { offers };
                }

                if (offers.offerexpirationDate < DateTime.Today)
                {
                    ModelState.AddModelError("Expiry", "Offer Expired");
                }
                else
                {
                    if (isComplete == 0)
                    {
                        var apiMethod = string.Format("CreditReport/offers/OfferAcceptance?merchantId={0}&contractId={1}&isCompleted={2}", CurrentMerchantID, ContractID, isComplete);
                        BaseApiData.PostAPIData(apiMethod, offers);
                        base.SetSuccessMessage("Data Updated.");
                    }
                    else
                    {
                        var apiMethod = string.Format("CreditReport/offers/OfferAcceptance?merchantId={0}&contractId={1}&isCompleted={2}", CurrentMerchantID, ContractID, isComplete);
                        BaseApiData.PostAPIData(apiMethod, offers);

                        var savedOffer = merchantApi.GetOfferDetails(CurrentMerchantID, ContractID);
                        var emailSent = false;
                        if (savedOffer.SelectedOffer != null)
                        {
                            emailSent = savedOffer.SelectedOffer.IsEmailSent;
                        }
                        if (emailSent)
                        {

                            string apiData = string.Format("Contracts/OfferAccpContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
                                CurrentMerchantID, (int)TaskTypes.PQOfferAcceptance, 1, ContractID);
                            BaseApiData.GetAPIResult<object>(apiData, () => new object());
                            base.SetSuccessMessage("Offer Acceptance- Task Completed.");
                        }
                        else
                        {
                            base.SetErrorMessage("Can't complete, Please send email first.");
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }

        public ActionResult AddOffer()
        {
            OfferModel mod = new OfferModel();
            return PartialView("_EditOffer", mod);
        }

        public ActionResult ListOffers()
        {
            List<OfferModel> offers = offerSesssionRepository.GetAll();
            
            if (Session["OferModifiedFlag"] != null)
            {
                if ((bool)Session["OferModifiedFlag"] == true)
                    ViewBag.OfferModifed = true;
            }

            return PartialView("_ListOffers", offers);
        }

        [HttpGet]
        public ActionResult EditOffer(long id)
        {
            var model = offerSesssionRepository.GetByID(id);
            model.IsUpdate = true;
            return PartialView("_EditOffer", model);
        }

        [HttpPost]
        public ActionResult UpdateOffer(OfferModel model)
        {
            if (ModelState.IsValid)
            {
                model.ownedAmount = Math.Round(model.loanAmount * model.proportion, 2);
                model.turn = Math.Round(model.ownedAmount / model.monthlypayment, 3);
                offerSesssionRepository.Update(model);
                SetOfferModifiedFlag(true);
                return RedirectToAction("AddOffer");
            }

            return PartialView("_EditOffer", model);
        }

        public ActionResult SendEmail()
        {
            Pecuniaus.Models.Prequel.Email objEmail = new Models.Prequel.Email();
            var apiMethod = string.Format("merchants/GetEmail?SalesRepId={0}", CurrentMerchantID);
            var model = BaseApiData.GetAPIResult<List<EmailModel>>(apiMethod, () => new List<EmailModel>()) ?? new List<EmailModel>();
            var eml = new List<SelectListItem>();
            objEmail.EmailList = from i in model
                                 select new SelectListItem { Text = i.Email, Value = i.Email };

            return PartialView("_SendEmail", objEmail);
        }

        [HttpPost]
        public ActionResult SendEmail(string[] SelectedEmail, FormCollection frm)
        {
            Utilities.Email.Emailer obj = new Utilities.Email.Emailer();
            if (SelectedEmail.Count() > 0)
            {
                string emailRecipent = String.Join(",", SelectedEmail);
                string emailBody = PrepareBody();
                obj.Send(emailRecipent, "Selected Offers", emailBody, string.Empty);

                //Update Email Send flag in db

                var selOffer = offerSesssionRepository.GetAll().FirstOrDefault(x => x.IsSelected == true);
                if (selOffer != null)
                {
                   
                    var apiMethod = string.Format("creditreport/offers/UpdateOfferEmailFlag/{0}/{1}?isEmailSent={2}&offerId={3}", CurrentMerchantID, ContractID, true, selOffer.offerId);
                    var resp = BaseApiData.PostAPIData<object>(apiMethod, "");
                }

            }
            return RedirectToAction("Index");

        }
        public string PrepareBody()
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();
            using (StreamReader reader = new StreamReader(Server.MapPath(@"..\..\Templates\OfferAcceptanceTemplate.html")))
            {
                body.Append(reader.ReadToEnd());
            }
            if (!String.IsNullOrEmpty(body.ToString()))
            {
                var apiMethod = string.Format("merchants/merchantinfo/{0}", CurrentMerchantID);
                var model = BaseApiData.GetAPIResult<MerchantModel>(apiMethod, () => new MerchantModel()) ?? new MerchantModel();
                model.MerchantID = CurrentMerchantID;

                body.Replace("{XXX}", model.MerchantID.ToString());
                body.Replace("{MerchantName}", model.MerchantName);
                body.Replace("{SaleRep}", model.assignedSalesRep);
                body.Replace("{Owner}", model.OwnerName);
                //body.Replace("{OfferDate}", DateTime.Now.ToShortDateString()); //Dummy
               // body.Replace("{ExpirationDate}", DateTime.Now.ToShortDateString());  //Dummy

                List<OfferModel> objOffers = offerSesssionRepository.GetAll().Where(x => x.IsSelected == true).ToList();
                if (objOffers.Count > 0)
                {
                    int count = 1;
                    System.Text.StringBuilder strOffer = new System.Text.StringBuilder(); ;
                    foreach (OfferModel offer in objOffers)
                    {
                        strOffer.Append("Offer: " + count.ToString() + "<br/>");
                        strOffer.Append("MCA Amount: " + offer.loanAmount + "<br/>");
                        strOffer.Append("Owen Amount: " + offer.ownedAmount + "<br/>");
                        strOffer.Append("Price: " + offer.proportion + "<br/>"); //dummy
                        strOffer.Append("% Retention: " + offer.retention + "<br/>");
                        strOffer.Append("Repayment Time (months): " + offer.turn + "<br/><br/>"); //dummy                       
                        count++;
                    }
                    body.Replace("{OfferList}", strOffer.ToString());
                    body.Replace("{ExpirationDate}",objOffers.Last().offerexpirationDate.ToShortDateString());  //Dummy
                    body.Replace("{OfferDate}", objOffers.Last().offerCreationDate.ToShortDateString()); //Dummy
                }
                else
                    body.Replace("{OfferList}", "No Offer Available");
            }
            return body.ToString();
        }

        private void SetOfferModifiedFlag(bool yes)
        {
            Session["OferModifiedFlag"] = yes;
        }

    }
}