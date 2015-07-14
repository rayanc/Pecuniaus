using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using Pecuniaus.Renewal.Repository;
using Pecuniaus.UICore.Controllers;
using System.Web.Mvc.Ajax;
using System.Collections;
using System.IO;
using Pecuniaus.Models;
using Pecuniaus.UICore;
using System.Globalization;
using System.Text;


namespace Pecuniaus.Renewal.Controllers
{
    public class RenewalOfferController : UiBaseController
    {
        MerchantApi merchantApi;
        OfferSesssionRepository offerSesssionRepository;


        public RenewalOfferController()
        {
            merchantApi = new MerchantApi();
            offerSesssionRepository = new OfferSesssionRepository();
        }

        public ActionResult Index()
        {
            MerchantInformationOfferModel model = merchantApi.GetOfferDetails(CurrentMerchantID, ContractID);
            offerSesssionRepository.Set(model.offers);
            //var offers = model.offers.Where(m => m.IsSelected = true).ToList();


            //if (offers != null)
            //{
            //    model.Id = offers[0].Id;
            //}
            //else
            //    model.Id = 0;

            if (!string.IsNullOrEmpty(model.reason))
                model.AddManulMccv = true;
            //if (model != null)
            //    Session["maxvalue"] = model.yearlysales;
            //else
            //    Session["maxvalue"] = "0";


            SetOfferModifiedFlag(false);
            return View(model);

        }



        [HttpPost]
        public ActionResult Index(MerchantInformationOfferModel model, string button, long SelectedOfferId)
        {
            var isComplete = 0;
            if (button == "Complete")            
                isComplete = 1;
            

            //model.TypeOfPropertyID = 1;
            if (ModelState.IsValid)
            {
                model.offers = offerSesssionRepository.GetAll();
                var offers = offerSesssionRepository.GetAll().FirstOrDefault(m => m.Id == SelectedOfferId);
                
                //Update IsSelected value
                if (model.SelectedOfferId > 0)
                {
                    foreach (var item in model.offers)
                    {
                        if (item.Id == model.SelectedOfferId)
                            item.IsSelected = true;
                        else
                            item.IsSelected = false;
                    }
                }
                
                //if (offers.offerexpirationDate < DateTime.Today)
                //{
                //    base.SetSuccessMessage("Expiry - Offer Expired.");
                //}
                if (isComplete == 0)
                {
                    var apiMethod = string.Format("CreditReport/offers/CreateSelect?merchantId={0}&contractId={1}&isCompleted={2}", CurrentMerchantID, ContractID, isComplete);
                    BaseApiData.PostAPIData(apiMethod, model);
                    base.SetSuccessMessage("Data Updated.");
                }
                else
                {
                    var apiMethod = string.Format("CreditReport/offers/CreateSelect?merchantId={0}&contractId={1}&isCompleted={2}", CurrentMerchantID, ContractID, isComplete);
                    BaseApiData.PostAPIData(apiMethod, model);

                    var m = merchantApi.GetOfferDetails(CurrentMerchantID, ContractID);
                    if (m.IsOffersEmailSent)
                    {
                        string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
                            CurrentMerchantID, (int)TaskTypes.RWOfferCreation, 3, ContractID);
                        BaseApiData.GetAPIResult<object>(apiData, () => new object());

                        var offerNotes = CreateOfferNote(CurrentMerchantID, ContractID);
                        //Save Notes

                        var notmodel = new NotesModel
                        {
                            MerchantId = base.CurrentMerchantID,
                            Note = offerNotes,
                            WorkFlowId = 3,
                            NoteTypeId = 550001,
                            ContractId = ContractID,
                            ScreenName = "Offer Creation"
                        };

                        ApiHelper.BaseApiData.PostAPIData("notes/save", notmodel);

                        base.SetSuccessMessage("Offer - Task Completed.");
                    }
                    else
                    {
                        base.SetErrorMessage("Can't complete, Please send email first.");
                    }
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult SendEmail()
        {
            Pecuniaus.Models.Prequel.Email objEmail = new Pecuniaus.Models.Prequel.Email();
            var apiMethod = string.Format("merchants/GetEmail?SalesRepId={0}", CurrentMerchantID);
            var model = BaseApiData.GetAPIResult<List<EmailModel>>(apiMethod, () => new List<EmailModel>()) ?? new List<EmailModel>();
            var eml = new List<SelectListItem>();
            objEmail.EmailList = from i in model
                                 select new SelectListItem { Text = i.Email, Value = i.Email };

            return PartialView("_SendEmail", objEmail);
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

                var apiMethod = string.Format("creditreport/offers/UpdateOfferCreationEmailFlag/{0}/{1}?isEmailSent={2}", CurrentMerchantID, ContractID, true);
                var resp = BaseApiData.PostAPIData<object>(apiMethod, "");
            }
            return RedirectToAction("Index");

        }

        public string PrepareBody()
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();
            using (StreamReader reader = new StreamReader(Server.MapPath(@"..\..\Templates\OfferTemplate.html")))
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
                //body.Replace("{ExpirationDate}", DateTime.Now.ToShortDateString());  //Dummy

                List<OfferModel> objOffers = offerSesssionRepository.GetAll();
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
                }
                else
                    body.Replace("{OfferList}", "No Offer Available");
                body.Replace("{ExpirationDate}", objOffers.Last().offerexpirationDate.ToShortDateString());
                body.Replace("{OfferDate}", objOffers.Last().offerCreationDate.ToShortDateString()); //Dummy
            }

            return body.ToString();
        }

        [HttpPost]
        public ActionResult GenratePDF(string email)
        {
            List<OfferModel> offers = offerSesssionRepository.GetAll();
            return new Rotativa.PartialViewAsPdf("_ListOffer", offers) { FileName = "OfferPdf.pdf" };
        }


        public ActionResult ListOffers()
        {
            List<OfferModel> offers = offerSesssionRepository.GetAll();

            if (Session["OferModifiedFlag"] != null)
            {
                if ((bool)Session["OferModifiedFlag"] == true)
                    ViewBag.OfferModifed = true;
            }
            return PartialView("_ListOffer", offers);
        }

        [HttpGet]
        public ActionResult AddOffer()
        {
            OfferModel mod = new OfferModel();
            //mod.MaxAnnualSalesAmount = Convert.ToDouble(Session["maxvalue"].ToString());
            //mod.MaxOwnedAmount = Convert.ToDouble(Session["maxvalue"].ToString()) * 2;
            return PartialView("_AddOffer", mod);
        }

        [HttpPost]
        public ActionResult AddOffer(OfferModel model)
        {
            if (ModelState.IsValid)
            {

                SetOfferModifiedFlag(true);
                offerSesssionRepository.AddOffer(model);
                return RedirectToAction("AddOffer");
            }
            return PartialView("_AddOffer", model);
        }

        [HttpGet]
        public ActionResult EditOffer(long id)
        {
            var model = offerSesssionRepository.GetByID(id);
            //model.MaxAnnualSalesAmount = Convert.ToDouble(Session["maxvalue"].ToString());
            //model.MaxOwnedAmount = Convert.ToDouble(Session["maxvalue"].ToString()) * 2;
            model.IsUpdate = true;
            return PartialView("_AddOffer", model);
        }

        private void SetOfferModifiedFlag(bool yes)
        {
            Session["OferModifiedFlag"] = yes;
        }
        [HttpPost]
        public ActionResult UpdateOffer(OfferModel model)
        {
            if (ModelState.IsValid)
            {
                SetOfferModifiedFlag(true);
                offerSesssionRepository.Update(model);
                return RedirectToAction("AddOffer");
            }

            return PartialView("_AddOffer", model);
        }

        [HttpPost]
        public ActionResult DelOffer(long id)
        {
            offerSesssionRepository.DelOffer(id);

            return Json("OK");
        }

        public string CreateOfferNote(long merchantId, long contractId)
        {
            var mo = merchantApi.GetMerchantOffer(merchantId, contractId);

            StringBuilder sNotes = new StringBuilder();

            sNotes.AppendLine("Merchant Info:");
            sNotes.AppendLine(string.Format("ID# {0}", mo.Merchant.MerchantId));
            sNotes.AppendLine(string.Format("Merchant Name: {0}", mo.Merchant.MerchantName));
            sNotes.AppendLine(string.Format("Entity Type: {0}", mo.Merchant.EntityType));
            sNotes.AppendLine(string.Format("Business Address: {0}", mo.Merchant.Address.FullAdderssString));
            sNotes.AppendLine(string.Format("Business Phone Number: {0}", mo.Merchant.Address.phone1));
            //sNotes.Append("Business Address: lojackrd@gmail.com");
            sNotes.AppendLine(string.Format("Rented or Owned: {0}", mo.Merchant.RentFlag == "200002" ? "Owned" : "Rented"));
            sNotes.AppendLine(string.Format("Rent Amount: {0:N2}", mo.Merchant.RentedAmount));
            sNotes.AppendLine(string.Format("Industry Type: {0}", mo.Merchant.IndustryType));
            sNotes.AppendLine(string.Format("Business Start Date: {0:d}", mo.Merchant.BusinessStartDate));
            sNotes.AppendLine(string.Format("Authorized Owner Name: {0}  {1}", mo.Owner.FirstName, mo.Owner.LastName));
            sNotes.AppendLine(string.Format("Authorized Owner Address: Ave. {0}", mo.Owner.Address.FullAdderssString));
            //sNotes.Append("Processors and start date: Visanet – 22-12-2005 / Cardnet – 14-01-2006");

            sNotes.AppendLine("Contract Info: ");
            sNotes.AppendLine(string.Format("Contract Type: {0}", mo.Contract.Type));
            sNotes.AppendLine(string.Format("Contract #: {0}", mo.Contract.ContractId));
            //sNotes.Append("Score: B700");
            //sNotes.Append("Score Date: 28-11-2014");
            sNotes.AppendLine(string.Format("MCCV Average: {0:N2}", mo.Merchant.AvgMccv));
            //sNotes.Append("Ticket Average: 95");
            //sNotes.Append("Average per transaction: 1,805.75");
            //sNotes.Append("Total Gross Sales Volume:");
            //sNotes.Append("RD$3,538,741.68 - 05/06/2012 10:23");
            //sNotes.Append("RD$3,538,741.68 - 04/06/2012 15:03");
            //sNotes.Append("RD$3,960,000.00 - 27/01/2012 09:20");

            sNotes.AppendLine("Saved Offers:");
            var idx = 1;
            foreach (var offer in mo.Offers)
            {
                sNotes.AppendLine(string.Format("Offer {0}: ", idx));
                sNotes.AppendLine(string.Format("MCA Amount: {0}", offer.loanAmount));
                sNotes.AppendLine(string.Format("Repayment Amount: {0}", offer.ownedAmount));
                sNotes.AppendLine(string.Format("Price: {0}", offer.ownedAmount - offer.loanAmount));
                sNotes.AppendLine(string.Format("Retention %: {0}", offer.retention));
                sNotes.AppendLine(string.Format("Turn (months): {0}", offer.turn));
                idx++;
            }


            //sNotes.Append("MCCV Average: 171,547.3");
            //sNotes.Append("From Gross Sales: 8.74%");

            //sNotes.Append("Offer 2: ");
            //sNotes.Append("MCA Amount: 305,000.00");
            //sNotes.Append("Repayment Amount: 423,950.00");
            //sNotes.Append("Price: 1.39");
            //sNotes.Append("Retention %: 12%");
            //sNotes.Append("Turn (months): 8.02");

            //sNotes.Append("MCCV Average: 171,547.3");
            //sNotes.Append("From Gross Sales: 7.74%");
            return sNotes.ToString();
        }
    }
}