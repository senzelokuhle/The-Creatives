using Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TheCreatives.Models;

namespace TheCreatives
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Seed user into the database
            ApplicationDbContext context = new ApplicationDbContext();
            IdentityHelper.SeedIdentities(context);
        }
    }

}
