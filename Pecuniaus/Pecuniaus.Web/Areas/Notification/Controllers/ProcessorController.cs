using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pecuniaus.ApiHelper;
using Pecuniaus.Notification.Models;
namespace Pecuniaus.Notification.Controllers
{
    public class ProcessorController : BaseController
    {
        //
        // GET: /Processor/
        public ActionResult Index()
        {
            ProcessorModel processorModel = new ProcessorModel();
            processorModel.LstProcessors = GetAllProcessors();
            return PartialView(processorModel);
        }
        [HttpPost]
        public ActionResult AddUpdateProcessor(string submitButton, ProcessorModel processorModel)
        {  
            if (ModelState.IsValid)
            {
                JsonResult result = CheckProcessorExist(processorModel.Name, processorModel.ProcessorId);
                dynamic obj = new ExpandoObject();
                obj.isDuplicate = null;
                obj = result.Data;
                if (!obj.isDuplicate)
                {
                    if (processorModel.ProcessorCode==null)
                    {
                        processorModel.ProcessorCode = string.Empty;
                    }
                    if (processorModel.Description == null)
                    {
                        processorModel.Description = string.Empty;
                    }

                    AddUpdateProcessor(processorModel);
                    if (ModelState.IsValid)
                    {
                        return RedirectToAction("AddProcessor");
                    }
                }
                
            }
            return PartialView("_AddProcessor", processorModel);
        }

        [HttpGet]
        public ActionResult EditProcessor(long id)
        { 
            ProcessorModel procModel = new ProcessorModel();
            List<ProcessorModel> LstProcessors = GetAllProcessors();
            if (LstProcessors != null && LstProcessors.Count > 0)
            {
                procModel = (from proc in LstProcessors
                              where proc.ProcessorId == id
                              select proc).SingleOrDefault();
               
                procModel.IsUpdate = true;     
            }
            return PartialView("_AddProcessor", procModel);
        }

        [HttpGet]
        public ActionResult ListProcessors() 
        {
            List<ProcessorModel> lstProcessors = GetAllProcessors();
            return PartialView("_ListProcessors", lstProcessors);
        }

        [HttpGet]
        public ActionResult AddProcessor() 
        {
            return PartialView("_AddProcessor", new ProcessorModel());
        }
        public List<ProcessorModel> GetAllProcessors()
        {
            List<ProcessorModel> lstProcessors = null;
            var apiMethod = "notification/GetAllProcessors";
            IList<ProcessorModel> objBE;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + apiMethod);
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<ProcessorModel>>(reader.ReadToEnd());
                lstProcessors = objBE.ToList();
            }
            return  lstProcessors;
        }

        public void AddUpdateProcessor(ProcessorModel obj)
        {
            var apiMethod = "notification/AddUpdateProcessor";
            var response = BaseApiData.PutAPIData<ProcessorModel>(apiMethod, obj);
            if (response.StatusCode == HttpStatusCode.OK)
                if (obj.ProcessorId == 0)
                {
                    base.SetSuccessMessage(Pecuniaus.Resources.Notification.Messages.ProcessorAddSuccess);
                }
                else
                {
                    base.SetSuccessMessage(Pecuniaus.Resources.Notification.Messages.ProcessorUpdateSuccess);
                }

            else
                base.SetSuccessMessage(Pecuniaus.Resources.Notification.Messages.ProcessorUpdateFailure);
        }

        [HttpGet]
        public JsonResult CheckProcessorExist(string processorName, int processorID)
        {
            bool isDuplicate = false; 
            ProcessorModel processor = new ProcessorModel();
            processor.ProcessorId = processorID;
            processor.Name = processorName;
            var apiMethod = "notification/CheckProcessorExist";
            var response = BaseApiData.PostAPIData<ProcessorModel>(apiMethod, processor);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }
           
            var jsonData = new { isDuplicate };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

	}
}