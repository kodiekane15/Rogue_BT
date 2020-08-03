using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rogue_BT.Startup))]
namespace Rogue_BT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
