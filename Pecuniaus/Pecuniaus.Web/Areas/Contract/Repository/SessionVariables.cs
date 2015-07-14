using Pecuniaus.UICore;
using System.Web;

namespace Pecuniaus.Contract.Repository
{
    public static class SessionVariables
    {
      [System.Obsolete("This property is depricated. Use New Property:- Pecuniaus.UICore.SessionHelper.CurrentMerchantID")]
        public static long CurrentMerchantID
        {
            get
            {
                return SessionHelper.CurrentMerchantID;
                ////long _mid=0;
                //if (HttpContext.Current.Session["_Cur_MerchantID"] != null)
                //{
                //    _mid = (long)HttpContext.Current.Session["_Cur_MerchantID"];
                //}

                //if (_mid == 0) //fix required 
                //    _mid = 10;

                //return _mid;

            }
        }
    }
}