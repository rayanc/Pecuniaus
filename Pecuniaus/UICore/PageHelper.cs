using System.Web;
using System.Xml;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pecuniaus.UICore
{
    public static class PageHelper
    {
        private static readonly string xmlFileName = "~/App_Data/TaskPages.xml";

        public static bool HasAccess(string controller, string action, string area)
        {
            var rv = true; // Must be False, True for testing
            var allPermissions = SessionHelper.GetPermisssions();
            var pageId = GetPageID(controller, action, area);
            var permission = allPermissions.FirstOrDefault(a => a.PageId == pageId);
            if (permission != null)
            {
                rv = permission.Read;
            }
            return rv;
        }

        private static long GetPageID(string controller, string action, string area)
        {
            long rv = 0;
            var task = FindPageNode(action, controller, area);
            if (task != null)
            {
                long.TryParse(task["PageID"].InnerText, out rv);
            }
            return rv;
        }

   
        public static string GetPageName(string action, string controller, string area)
        {
            string rv = "";
            var doc = GetPagesDoc();

            var task = FindPageNode(action, controller, area);
            if (task != null)
            {
                rv = task["Name"].InnerText;
            }
            return rv;
        }

        public static int GetWorkflowId(string action, string controller, string area)
        {
            int rv = 0;
        
            var task = FindPageNode(action, controller, area);

            if (task != null)
            {
                int.TryParse(task["WorkflowID"].InnerText, out rv);
            }

            if (rv == 0)
            {
                switch (area.ToLower())
                {
                    case "contract":
                        rv = 2;
                        break;
                    case "merchantprofile":
                        rv = 1;
                        break;
                    case "finance":
                        rv = 7;
                        break;
                    case "collection":
                        rv = 6;
                        break;
                    case "renewal":
                        rv = 3;
                        break;
                    case "":
                        rv = 1;
                        break;
                }
            }
            return rv;
        }


        public static string GetPageLink(long taskTypeID, RequestContext requestContext)
        {
            string rv = "";
            var doc = GetPagesDoc();
            XmlNode task;
            XmlElement root = doc.DocumentElement;
            task = root.SelectSingleNode(string.Format("/Tasks/Task[TaskTypeID={0}]", taskTypeID));
            if (task != null)
            {
                var controller = task["Controller"].InnerText;
                var action = task["Action"].InnerText;
                var area = task["Area"].InnerText;
                UrlHelper u = new UrlHelper(requestContext);
                string url = u.Action(action, controller, new { area = area });
                return url;
            }
            return rv;
        }

        private static XmlDocument GetPagesDoc()
        {
            string fileName = HttpContext.Current.Server.MapPath(xmlFileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return doc;
        }

        private static XmlNode FindPageNode(string action, string controller, string area)
        {
            var doc = GetPagesDoc();
            XmlNode task;
            XmlElement root = doc.DocumentElement;
            return root.SelectSingleNode(string.Format("/Tasks/Task[Action='{0}' and Controller='{1}' and Area='{2}'] ",
                action.ToLower(), controller.ToLower(), area.ToLower()));
        }

    }
}
