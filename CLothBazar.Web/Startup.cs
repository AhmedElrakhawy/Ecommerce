using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CLothBazar.Web.Startup))]
namespace CLothBazar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
