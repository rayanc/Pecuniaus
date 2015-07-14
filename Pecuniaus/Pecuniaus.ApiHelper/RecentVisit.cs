
namespace Pecuniaus.ApiHelper
{
    public static class RecentVisit
    {
        public static void Add(long merchantID, long userID)
        {
            var apiQuery= "users/recent/save/" + userID + "/" + merchantID;
       
            BaseApiData.PostAPIData(apiQuery, null);

            //HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "users/recent/save/" + new SessionManager().CurrentUserID + "/" + MerchantID);
            //     objRequest.Method = "Post";
            //     objRequest.ContentType = "application/xml";
            //     objRequest.ContentLength = 0;
            //     var response = objRequest.GetResponse();
        }
    }
}
