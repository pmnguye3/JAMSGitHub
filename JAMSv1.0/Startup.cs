using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JAMSv1._0.Startup))]
namespace JAMSv1._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
