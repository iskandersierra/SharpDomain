using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestMVCApp.Startup))]
namespace TestMVCApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
