using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.Finance.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using Pecuniaus.UICore.Controllers;

namespace Pecuniaus.Finance.Controllers
{
    public class MerchantFinanceController : UiBaseController
    {
        #region ActionResults
        public ActionResult Index()
        {
            var model = new SearchFinanceModel();
            Session["Financemodel"] = null;
            model.activityType = GetActivityType();
            model.processorCompany = GetProcessorNames();
            model.addFinance = new AddFinanceModel();
            model.addFinance.activityType = model.activityType;
            model.addFinance.processorCompany = model.processorCompany;
            Session["ActivityType"] = model.activityType;
            Session["Processor"] = model.processorCompany;
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            return View(model);
        }

        public PartialViewResult SearchGrid()
        {
            var model = new SearchFinanceModel();
            if (Session["Financemodel"] != null)
            {
                model = (SearchFinanceModel)Session["Financemodel"];
            }
            return (PartialView("_SearchGrid", model));
        }

        [HttpPost]
        public ActionResult Index(SearchFinanceModel model, string command)
        {
            switch (command)
            {
                case "search":
                    Session["Finance"] = null;

                    IEnumerable<FinanceModel> objBE;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = client.PostAsJsonAsync("finance/SearchFinance", model).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResult = response.Content.ReadAsStringAsync().Result;
                            objBE = JsonConvert.DeserializeObject<IEnumerable<FinanceModel>>(jsonResult);
                            model.finance = objBE;
                            if (!string.IsNullOrEmpty(jsonResult) && !jsonResult.Equals("[]"))
                            {
                                GridView gv = new GridView();
                                gv.DataSource = objBE;
                                gv.DataBind();
                                Session["Finance"] = gv;
                            }
                        }
                    }
                    model.activityType = (IEnumerable<SelectListItem>)Session["ActivityType"];
                    model.processorCompany = (IEnumerable<SelectListItem>)Session["Processor"];
                    model.addFinance = new AddFinanceModel();
                    model.addFinance.activityType = (IEnumerable<SelectListItem>)Session["ActivityType"];
                    model.addFinance.processorCompany = (IEnumerable<SelectListItem>)Session["Processor"];
                    Session["Financemodel"] = model;
                    return View(model);
                case "excel":
                    if (Session["Finance"] != null)
                    {
                        ExportToExcel((GridView)Session["Finance"], "Finance.xls");
                        ViewBag.SuccessMsg = Pecuniaus.Resources.Finance.Finance.ExportSuccessfully;
                    }
                    model.activityType = (IEnumerable<SelectListItem>)Session["ActivityType"];
                    model.processorCompany = (IEnumerable<SelectListItem>)Session["Processor"];
                    model.addFinance = new AddFinanceModel();
                    model.addFinance.activityType = (IEnumerable<SelectListItem>)Session["ActivityType"];
                    model.addFinance.processorCompany = (IEnumerable<SelectListItem>)Session["Processor"];
                    return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddFinance(SearchFinanceModel model)
        {
            Session["Finance"] = null;
            IEnumerable<FinanceModel> objBE;
            using (var client = new HttpClient())
            {
                model.addFinance.insertUserId = base.CurrentUserID;
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("finance/SaveFinanceActivity", model.addFinance).Result;
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(jsonResult) && !jsonResult.Equals("[]"))
                    {
                        objBE = JsonConvert.DeserializeObject<IEnumerable<FinanceModel>>(jsonResult);
                        model.addFinance.finance = objBE;
                    }
                    else
                        model.addFinance.finance = null;
                }
                model.activityType = (IEnumerable<SelectListItem>)Session["ActivityType"];
                model.processorCompany = (IEnumerable<SelectListItem>)Session["Processor"];
                model.addFinance.activityType = (IEnumerable<SelectListItem>)Session["ActivityType"];
                model.addFinance.processorCompany = (IEnumerable<SelectListItem>)Session["Processor"];
                ViewBag.SuccessMsg = Pecuniaus.Resources.Finance.Finance.FinanceAddedSuccessfully;
            }
            return View("Index", model);
        }

        #endregion

        #region Methods

        private IEnumerable<SelectListItem> GetActivityType()
        {
            var docTypes = BaseApiData.GetAPIResult<IList<FinanceDD>>("finance/RetrieveActivityType", () => new List<FinanceDD>());

            return from iList in docTypes
                   select new SelectListItem
                   {
                       Text = iList.value,
                       Value = iList.keyId.ToString()
                   };
        }

        private IEnumerable<SelectListItem> GetProcessorNames()
        {
            var docTypes = BaseApiData.GetAPIResult<IList<FinanceDD>>("finance/RetrieveProcessorName", () => new List<FinanceDD>());

            return from iList in docTypes
                   select new SelectListItem
                   {
                       Text = iList.value,
                       Value = iList.keyId.ToString()
                   };
        }

        private void ExportToExcel(GridView gvExcel, string fileName)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Date";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Processor";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Retention";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Price";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Capital";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Income through Processor";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Other Income";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Type of Activity";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Contract Number";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Note";
            HeaderRow.Cells.Add(HeaderCell);

            gvExcel.Controls[0].Controls.AddAt(0, HeaderRow);
            gvExcel.Controls[0].Controls.RemoveAt(1);

            //Export to Excel
            GridView ExcelGridView = gvExcel;
            HttpContext curContext = System.Web.HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            curContext.Response.Charset = "";
            curContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            curContext.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            ExcelGridView.RenderControl(htw);
            byte[] byteArray = Encoding.ASCII.GetBytes(sw.ToString());
            MemoryStream mStream = new MemoryStream(byteArray);
            StreamReader sReader = new StreamReader(mStream, Encoding.ASCII);
            curContext.Response.Write(sReader.ReadToEnd());
            curContext.Response.End();
            sw = null;
            htw = null;
            mStream = null;
            sReader = null;
            gvExcel = null;
        }

       [HttpPost]
        public JsonResult GetFundedContract(Int64 MerchantId)
        {
            var apiMethod = "finance/RetrieveFundedContract?MerchantId=" + MerchantId;
            AddFinanceModel modelList = new AddFinanceModel();
            string ContractId = string.Empty;
            var obj = BaseApiData.GetAPIResult<IList<AddFinanceModel>>(apiMethod, () => new List<AddFinanceModel>());
            if (obj.Count > 0 && obj[0].contractId > 0)
            {
               modelList = obj.Select(x =>
                        new AddFinanceModel()
                        {
                                   contractId = x.contractId,
                                   contractTypeId=x.contractTypeId,
                                   TransferAmount=x.TransferAmount,
                                   pendingAmount=x.pendingAmount, 
                                   processorId=x.processorId
                        }).ToList().First();           
            }
        return Json(modelList, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult DelActivity(long id)
        {
            var apiMethod = string.Format("finance/DeleteActivity/" + id);
            var response = BaseApiData.PutAPIData(apiMethod, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var model = new SearchFinanceModel();
                if (Session["Financemodel"] != null)
                {
                    model = (SearchFinanceModel)Session["Financemodel"];
                    var filterlist = from f in model.finance
                                     where f.ActivityId != id
                                     select f;
                    model.finance = filterlist;
                    Session["Financemodel"] = model;
                }
                ViewBag.SuccessMsg = "Finance Activity Deleteded Successfully";
            }
            return Json("OK");
        }

        public ActionResult creditTransfer()
        {
            return PartialView("_creditTransfer");
        }
        #endregion
    }
}