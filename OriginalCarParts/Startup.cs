using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OriginalCarParts.Startup))]
namespace OriginalCarParts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
