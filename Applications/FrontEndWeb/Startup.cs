using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrontEndWeb.Startup))]
namespace FrontEndWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
