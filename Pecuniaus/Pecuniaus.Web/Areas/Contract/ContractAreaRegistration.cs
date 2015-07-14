using Pecuniaus.Contract;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Contract
{
    public class ContractRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Contract";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Contract_default",
                url: "Contract/{controller}/{action}/{id}",
                defaults: new { controller = "DocumentScan", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.Contract.Controllers" }

            );
            RegisterBundles();
        }
       
        private void RegisterBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }       

    }
}