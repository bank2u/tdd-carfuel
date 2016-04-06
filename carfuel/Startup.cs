using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarFuel.Startup))]
namespace CarFuel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
