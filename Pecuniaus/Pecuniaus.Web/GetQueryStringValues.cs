
using Pecuniaus.UICore;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web;
using System;
namespace Pecuniaus.Web
{
    public class GetQueryStringValues : IActionFilter, IMvcFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //long _mid;
            //var mid = filterContext.HttpContext.Request["merchantid"] ?? "";

            //if (long.TryParse(mid, out _mid))
            //{
            //    string currentPage = filterContext.HttpContext.Request.Url.PathAndQuery.ToLower();
            //    if (filterContext.HttpContext.Session["_Cur_Page"] == null)
            //    {
            //        filterContext.HttpContext.Session["_Cur_Page"] = currentPage;
            //    }

            //    if ((string)filterContext.HttpContext.Session["_Cur_Page"] != currentPage)
            //    {
            //        Pecuniaus.ApiHelper.RecentVisit.Add(_mid, SessionHelper.CurrentUserID);
            //        filterContext.HttpContext.Session["_Cur_Page"] = currentPage;
            //    }
            //}
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //long _mid;
            //var mid = filterContext.HttpContext.Request["merchantid"] ?? "";

            //if (long.TryParse(mid, out _mid))
            //{
            //    SessionHelper.SetCurrentMerchant(_mid);
            //}
            var resetMerchant = filterContext.HttpContext.Request["rstmrchnt"] ?? "";

            if (!string.IsNullOrEmpty(resetMerchant))
            {
                SessionHelper.SetCurrentMerchant(0);
                HttpContext.Current.Session["dupmerchantid"] = "0";
            }

            var merchantid = filterContext.HttpContext.Request["merchantid"] ?? "";

            if (!string.IsNullOrEmpty(merchantid))
            {
                SessionHelper.SetCurrentMerchant(Convert.ToInt64(merchantid));
            }

            var dupmerchantid = filterContext.HttpContext.Request["dupmerchantid"] ?? "";

            if (!string.IsNullOrEmpty(dupmerchantid))
            {
                SessionHelper.SetCurrentMerchant(Convert.ToInt64(dupmerchantid));
            }

            if (filterContext.HttpContext.Request["culture"] != null)
            {
                int culture;
                if (int.TryParse(filterContext.HttpContext.Request["culture"], out culture))
                {
                    filterContext.HttpContext.Session["_Cur_Culture"] = culture;
                }
            }

            if (filterContext.HttpContext.Session["_Cur_Culture"] != null)
            {
                int culture = (int)filterContext.HttpContext.Session["_Cur_Culture"];

                string cultureName = string.Empty;

                switch (culture)
                {
                    case 1:
                        cultureName = "es-US";
                        break;
                    case 2:
                        cultureName = "es-DO";
                        break;
                }

                if (!string.IsNullOrEmpty(cultureName))
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
                }
            }

        }

        public bool AllowMultiple
        {
            get { return false; }
        }

        public int Order
        {
            get { return Filter.DefaultOrder; }
        }
    }
}