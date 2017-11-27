using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MissionAuthen.Startup))]
namespace MissionAuthen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
