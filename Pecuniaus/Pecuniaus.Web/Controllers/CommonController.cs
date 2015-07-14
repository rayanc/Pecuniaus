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
    public class CommonController : BaseController
    {
        public ActionResult Search()
        {
            SearchModel objSM = new SearchModel();

            objSM.TaskNameList = new CommonFunctions().RetrieveGeneralTypes("1006");
            objSM.TaskStatusList = new CommonFunctions().RetrieveGeneralTypes("1007");

            return PartialView("Search", objSM);
        }
        public ActionResult SearchResult(string MerchantInfo, string TaskType, string TaskStatus)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/search?statusid=" + TaskStatus + "&tasktype=" + TaskType + "&merchantId=" + MerchantInfo);
            objRequest.Method = "Get";

            IList<MerchantsModel> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<MerchantsModel>>(reader.ReadToEnd());
            }

            return PartialView(objBE);
        }
        public JsonResult SearchResultInJson(string Query)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "merchants/search?" + Query);
            objRequest.Method = "Get";

            IList<MerchantsModel> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<MerchantsModel>>(reader.ReadToEnd());
            }

            return Json(objBE, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Notes(char Mode = 'T')
        {
            TempData["NotesMode"] = Mode;
            TempData.Keep();

            string apiQuery = null;
            if (Mode.Equals('T'))
                apiQuery = "notes/retrivenotes/temp?merchantid=1";
            if (Mode.Equals('P'))
                apiQuery = "notes/retrivenotes?merchantid=1";

            IList<NotesModel> objNotes = base.GetAPIResult<IList<NotesModel>>(apiQuery, () => new List<NotesModel>());

            return PartialView("Notes", objNotes);
        }
        public ActionResult AddNote()
        {
            TempData.Keep();
            IEnumerable<GeneralModel> NoteTypes = new CommonFunctions().RetrieveGeneralTypes("1011");
            ViewBag.NoteTypes = new SelectList(NoteTypes, "keyId", "description");

            return PartialView("AddNote");
        }
        public ActionResult SaveNote(NotesModel objNotes)
        {
            //NotesModel objNotes = new NotesModel() { noteTypeId = 500001, contractId = 0, merchantId = 1, note = "Test Note", screenName = "Test Screen Name", workFlowId = 0 };

            //base.PostAPIData("notes/save/temp", objNotes);

            string NotesMethodURI = null;
            char NotesMode = Convert.ToChar(TempData["NotesMode"]);

            if (NotesMode == 'T')
                NotesMethodURI = "notes/save/temp";
            else if (NotesMode == 'P')
                NotesMethodURI = "notes/save";

            using (var client = new HttpClient())
            {
                string baseAddress = System.Configuration.ConfigurationManager.AppSettings["APIURI"];
                client.BaseAddress = new Uri(baseAddress);
                var response = client.PostAsJsonAsync(NotesMethodURI, objNotes).Result;
            }

            return Notes(NotesMode);
        }
        public ActionResult RecentVisits()
        {
            IList<RecentlyVisitedModel> RecentVisits = base.GetAPIResult<IList<RecentlyVisitedModel>>(string.Format( "users/recent/merchants/{0}", base.CurrentUserID), () => new List<RecentlyVisitedModel>());
            return PartialView("RecentVisits", RecentVisits.Take(5));
        }
        public ActionResult AddRecentVisit(string MerchantID)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "users/recent/save/" + new SessionManager().CurrentUserID + "/" + MerchantID);
            objRequest.Method = "Post";
            objRequest.ContentType = "application/xml";
            objRequest.ContentLength = 0;
            var response = objRequest.GetResponse();

            return new EmptyResult();
        }
    }
}