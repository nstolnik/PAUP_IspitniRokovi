using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IspitniRokovi.Startup))]
namespace IspitniRokovi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}