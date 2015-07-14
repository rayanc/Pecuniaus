using System.Web.Mvc;
//using System.Web.Optimization;


namespace Contract
{
    public class RenewalRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Renewal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Renewal_default",
                url: "Renewal/{controller}/{action}/{id}",
                defaults: new { controller = "Merchant", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.Renewal.Controllers" }

            );
            //RegisterBundles();
        }
       
        //private void RegisterBundles()
        //{
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}       

    }
}