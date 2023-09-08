using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(M4_Project.Startup))]
namespace M4_Project
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
