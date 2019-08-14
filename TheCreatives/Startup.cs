using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheCreatives.Startup))]
namespace TheCreatives
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
