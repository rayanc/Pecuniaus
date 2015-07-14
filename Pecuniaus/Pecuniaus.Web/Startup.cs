using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(Pecuniaus.Web.Startup))]
namespace Pecuniaus.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}