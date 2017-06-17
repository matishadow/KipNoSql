using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kip.Web.Startup))]
namespace Kip.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
