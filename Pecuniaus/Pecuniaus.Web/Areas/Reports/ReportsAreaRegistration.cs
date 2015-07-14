using System.Web.Mvc;
using System.Web.Optimization;
using Pecuniaus.Reports;

namespace Reports
{
    public class ReportsRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "Reports_default",
                url: "Reports/{controller}/{action}/{id}",
                defaults: new { controller = "ReportViewer", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.Reports.Controllers" }

            );
            RegisterBundles();
        }

        private void RegisterBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}