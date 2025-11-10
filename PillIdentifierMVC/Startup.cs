using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PillIdentifierMVC.Startup))]
namespace PillIdentifierMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
