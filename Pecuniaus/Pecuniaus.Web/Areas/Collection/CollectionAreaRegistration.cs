using System.Web.Mvc;

namespace Collection
{
    public class CollectionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Collection";
            }
        }
        
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Collection_default",
                url: "Collection/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.Collection.Controllers" }

            );
        }
    }
}