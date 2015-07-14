using System;

namespace Pecuniaus.Web.HelperClasses
{
    public class SessionManager
    {
        [System.Obsolete("This property is depricated. Use New Property:- Pecuniaus.UICore.SessionHelper.CurrentMerchantID")]
        public string CurrentMerchantID { get { return Convert.ToString(System.Web.HttpContext.Current.Session["CurrentMerchantID"]); } set { System.Web.HttpContext.Current.Session["CurrentMerchantID"] = value; } }

        [System.Obsolete("This property is depricated. Use New Property:- Pecuniaus.UICore.SessionHelper.CurrentUserID")]
        public string CurrentUserID { get { return Convert.ToString(System.Web.HttpContext.Current.Session["CurrentUserID"]); } set { System.Web.HttpContext.Current.Session["CurrentUserID"] = value; } }
    }
}