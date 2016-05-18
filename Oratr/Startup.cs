using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Oratr.Startup))]
namespace Oratr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
