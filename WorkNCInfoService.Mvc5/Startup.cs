using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkNCInfoService.Mvc5.Startup))]
namespace WorkNCInfoService.Mvc5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
