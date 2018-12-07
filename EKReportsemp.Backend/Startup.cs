using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EKReportsemp.Backend.Startup))]
namespace EKReportsemp.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
