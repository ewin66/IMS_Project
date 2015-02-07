using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IMS_Web.Startup))]
namespace IMS_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
