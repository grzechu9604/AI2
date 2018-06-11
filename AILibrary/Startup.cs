using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AILibrary.Startup))]
namespace AILibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
