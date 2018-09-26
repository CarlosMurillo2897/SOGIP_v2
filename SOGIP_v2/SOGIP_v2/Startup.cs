using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOGIP_v2.Startup))]
namespace SOGIP_v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
