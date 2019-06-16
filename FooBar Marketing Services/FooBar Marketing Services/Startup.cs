using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FooBar_Marketing_Services.Startup))]
namespace FooBar_Marketing_Services
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
