using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Pecuniaus.UICore;
using Pecuniaus.UICore.Controllers;
using Pecuniaus.Utilities.Email;
using System.Configuration;

namespace Pecuniaus.Common.Controllers
{
    public class NotesController : UiBaseController
    {
        //
        // GET: /Notes/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string SearchCriteria = "", int WorkflowId = -1, int SesionWorkFlowID = 0)
        {
            //if (SesionWorkFlowID > 0 && WorkflowId == -1)
            //    WorkflowId = SesionWorkFlowID;

            char mode = 'P';
            string apiQuery;
            if (mode.Equals('T'))
                apiQuery = "notes/retrivenotes/temp?merchantid=" + UICore.SessionHelper.CurrentMerchantID;
            else
            {
                apiQuery = "notes/retrivenotes?merchantid=" + UICore.SessionHelper.CurrentMerchantID;

                if (SearchCriteria != "" || WorkflowId > 0)
                {
                    apiQuery = apiQuery + "&$filter=";
                    if (SearchCriteria != "")
                        apiQuery = apiQuery + "substringof('" + SearchCriteria + "', note)";
                    if (WorkflowId > 0)
                    {
                        if (SearchCriteria != "")
                            apiQuery = apiQuery + " and ";

                        apiQuery = apiQuery + "workFlowId eq " + WorkflowId;
                    }
                }
            }

            var objNotes = BaseApiData.GetAPIResult<IList<NotesModel>>(apiQuery, () => new List<NotesModel>());
            ViewBag.WorkflowTypes = GetWorkflowTypes();
            return PartialView("_List", objNotes);
        }

        public ActionResult Add(char mode, long merchantId, int WorkflowID, long ContractID, string ScreenName)
        {

            var model = new NotesModel
            {
                MerchantId = merchantId,
                NoteTypes = LoadNotesTypes(),
                WorkFlowId = WorkflowID,
                ContractId = ContractID,
                ScreenName = ScreenName
            };

            model.NoteTypeId = SetDefaultSelected(WorkflowID);

            TempData["NotesMode"] = mode;
            return PartialView("_Add", model);
        }

        public ActionResult ProcessNote(string submitButton, NotesModel objNotes)
        {
            //Quick fix for Merchant and contractid
            if (objNotes.MerchantId == 0)
                objNotes.MerchantId = Pecuniaus.UICore.SessionHelper.CurrentMerchantID;
            if (objNotes.ContractId == 0)
                objNotes.ContractId = Pecuniaus.UICore.SessionHelper.ContractID;

            ActionResult ar = PartialView("_Add", objNotes);
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Save":
                        ar = SaveNote(objNotes);
                        break;
                    case "SaveandSend":
                        ar = SendNotesEmail(objNotes);
                        break;
                }
            }
            return ar;
        }

        public ActionResult SendNotesEmail(NotesModel objNotes)
        {

            var apiMethod = string.Format("merchants/GetEmail?SalesRepId={0}", CurrentMerchantID);
            var model = BaseApiData.GetAPIResult<List<EmailModel>>(apiMethod, () => new List<EmailModel>()) ?? new List<EmailModel>();
            var eml = new List<SelectListItem>();
            //Pecuniaus.Models.Prequel.Email 
            objNotes.EmailList = from i in model
                                 select new SelectListItem { Text = i.Email, Value = i.Email };

            objNotes.NoteTypes = LoadNotesTypes();
            return PartialView("_SendNotesEmail", objNotes);
        }

        public ActionResult SaveNote(NotesModel objNotes)
        {
            //NotesModel objNotes = new NotesModel() { noteTypeId = 500001, contractId = 0, merchantId = 1, note = "Test Note", screenName = "Test Screen Name", workFlowId = 0 };

            //base.PostAPIData("notes/save/temp", objNotes);

            string NotesMethodURI = null;
            char NotesMode = Convert.ToChar(TempData["NotesMode"]);
            objNotes.InsertUserId = CurrentUserID;
            if (NotesMode == 'T')
                NotesMethodURI = "notes/save/temp";
            else if (NotesMode == 'P')
                NotesMethodURI = "notes/save";
            ApiHelper.BaseApiData.PostAPIData(NotesMethodURI, objNotes);

            //using (var client = new HttpClient())
            //{
            //    string baseAddress = System.Configuration.ConfigurationManager.AppSettings["APIURI"];
            //    client.BaseAddress = new Uri(baseAddress);
            //    var response = client.PostAsJsonAsync(NotesMethodURI, objNotes).Result;
            //}

            return List();
        }

        public ActionResult SaveandSendNote(string[] SelectedEmail, NotesModel objNotes)
        {

            string NotesMethodURI = null;
            char NotesMode = Convert.ToChar(TempData["NotesMode"]);
            objNotes.InsertUserId = CurrentUserID;

            if (NotesMode == 'T')
                NotesMethodURI = "notes/save/temp";
            else if (NotesMode == 'P')
                NotesMethodURI = "notes/save";
            ApiHelper.BaseApiData.PostAPIData(NotesMethodURI, objNotes);

            //Send Note Email
            string NotesLetterTemplateFileName = ConfigurationManager.AppSettings["NotesLetterTemplate"];
            string Path = Server.MapPath("~") + "Templates";
            string EmailBody = System.IO.File.ReadAllText(Path + "\\" + NotesLetterTemplateFileName).Replace("<NoteText>", objNotes.Note.ToString());

            string emailRecipent = String.Join(",", SelectedEmail);
            new Emailer().Send(emailRecipent, "New Note Added", EmailBody, "");

            return List();
        }
        private IEnumerable<SelectListItem> GetWorkflowTypes()
        {
            var query = "gen/retrieve/1014";
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            IEnumerable<SelectListItem> Items = from c in docTypes
                                                select new SelectListItem
                                                {
                                                    Text = c.Description,
                                                    Value = c.KeyId.ToString()
                                                };

            Items = Items.Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable());

            return Items;
        }
        private IEnumerable<SelectListItem> LoadNotesTypes()
        {
            var docTypes = CommonFunctions.GetNotesTypes();
            IEnumerable<SelectListItem> Items = from c in docTypes
                                                select new SelectListItem
                                                {
                                                    Text = c.Description,
                                                    Value = c.KeyId.ToString()
                                                };

            return Items;
        }
        public Int64 SetDefaultSelected(int WorkflowID)
        {
            Int64 DefValue = 0;
            switch (WorkflowID)
            {
                case 1:
                    DefValue = 500001;
                    break;
                case 2:
                    DefValue = 510001;
                    break;
                case 3:
                    DefValue = 520001;
                    break;
                case 0:
                    DefValue = 530001;
                    break;
                default:
                    DefValue = 530001;
                    break;
            }

            return DefValue;
        }

        [HttpPost]
        public void AddVeriCall(string notes)
        {
            var model = new NotesModel
            {
                MerchantId = base.CurrentMerchantID,
                Note = notes,
                WorkFlowId = 2,
                NoteTypeId = 510001,
                ContractId = ContractID,
                ScreenName = "Verification Call"
            };
            ApiHelper.BaseApiData.PostAPIData("notes/save", model);
        }
    }
}