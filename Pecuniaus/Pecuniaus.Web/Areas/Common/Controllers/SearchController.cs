using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using Pecuniaus.UICore.Controllers;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using Pecuniaus.UICore;
using System.Web;
using System;

namespace Pecuniaus.Common.Controllers
{
    public class SearchController : UiBaseController
    {
        public ActionResult Index(long workFlowID, string searchType, bool showSf = false)
        {
            
            var model = new SearchModel
            {
                WorkFlowID = workFlowID,
                SearchType = searchType,
                TaskNameList = CommonFunctions.GetTaskNames(),
                TaskStatusList = CommonFunctions.GetTaskStatuses(),
                ShowSF = showSf,
                Users = GetUsers()

            };

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Search"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["Search"];
                model.MerchantIdName = cookie.Values["Name"];
                model.ContractId = cookie.Values["ContId"];
                model.TaskNameId= cookie.Values["Task"];
                model.TaskStatusId = cookie.Values["Status"];
                model.UserId= cookie.Values["User"];
           
            }

            return PartialView("_Search", model);
        }

        public ActionResult SearchResult(string name = "", string task = "", string status = "",
            long workFlowID = 0, int showTemp = 0, string searchtype = "", string contractId = "", int? userId = null)
        {
            HttpCookie cookie = new HttpCookie("Search");
            cookie.Values["Name"] = name;
            cookie.Values["ContId"] = contractId;
            cookie.Values["Task"] = task;
            cookie.Values["Status"] = status;
            cookie.Values["User"] = userId.ToString();
            
            cookie.Expires = DateTime.Now.AddDays(1);
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            if (showTemp == 0)
            {
                //   var apiMethod = string.Format("merchants/searchResult?merchantId={0}&businessName=&rnc=&legalName=&ownerName=&contractId=&workflowId=&statusId=&processornbr=&processorName=&tasktype=&showTemp=0", ID);
                //var apiMethod = string.Format("merchants/searchResult?workflowId={0}&showTemp=0&tasktype={1}&statusId={2}&merchantId={3}&SearchType={4}", workFlowID, task, status, merchantid, searchtype);
                StringBuilder qry = new StringBuilder();
                qry.AppendFormat("merchants/searchResult?workflowId={0}", workFlowID);

                qry.Append("&showTemp=0");
                if (!string.IsNullOrEmpty(task))
                    qry.AppendFormat("&tasktype={0}", task);

                if (!string.IsNullOrEmpty(status))
                    qry.AppendFormat("&statusId={0}", status);

                qry.AppendFormat("&SearchType={0}", searchtype);

                long merchantID;
                if (long.TryParse(name, out merchantID))
                {
                    qry.AppendFormat("&merchantId={0}", merchantID);

                }
                else if (!string.IsNullOrEmpty(name))
                {
                    qry.AppendFormat("&businessName={0}", name);
                }
                if (!string.IsNullOrEmpty(contractId))
                {
                    qry.AppendFormat("&contractId={0}", contractId);
                }

                if (userId.HasValue)
                    qry.AppendFormat("&AssignedUserId={0}", userId);
                else
                    qry.AppendFormat("&AssignedUserId={0}", CurrentUserID);



                var model = BaseApiData.GetAPIResult<IList<SearchResultModel>>(qry.ToString(), () => new List<SearchResultModel>());
                return PartialView("_SearchResult", model);
            }
            else
            {
                var apiMethod = string.Format("merchants/searchResult?showTemp={0}&SearchType={1}", showTemp, "T");

                var model = BaseApiData.GetAPIResult<IList<SearchResultModel>>(apiMethod, () => new List<SearchResultModel>());
                return PartialView("_SearchResult", model);
            }
        }

        public ActionResult SrchRedirect(long merchantId, long ttId = 0, long contractId = 0)
        {
            var apiMethod = string.Format("merchants/merchantinfo/{0}?tasktypeId={1}&contractId={2}", merchantId, ttId, contractId);

            var model = BaseApiData.GetAPIResult<MerchantModel>(apiMethod, () => new MerchantModel()) ?? new MerchantModel();

            Pecuniaus.UICore.SessionHelper.SetContractID(model.ContractId);
            Pecuniaus.UICore.SessionHelper.SetCurrentMerchant(merchantId);

            if (merchantId > 0)
                Pecuniaus.ApiHelper.RecentVisit.Add(merchantId, SessionHelper.CurrentUserID);

            var redirectURL = GetPageLink(model.taskTypeId);
            if (!string.IsNullOrEmpty(redirectURL))
            {
                return Redirect(redirectURL);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsolutePath);

            return Redirect("/");
        }


        protected IEnumerable<SelectListItem> GetUsers()
        {
            var users = new UsersApi().GetAllUsers();
            return from c in users
                   select new SelectListItem
                   {
                       Text = c.FirstName + " " + c.LastName,
                       Value = c.Id.ToString(),
                   };
        }

    }
}