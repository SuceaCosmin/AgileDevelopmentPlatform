using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgileDevelopmentPlatform.Startup))]
namespace AgileDevelopmentPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
