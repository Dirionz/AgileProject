using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgileProject.Startup))]
namespace AgileProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
