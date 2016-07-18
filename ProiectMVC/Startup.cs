using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProiectMVC.Startup))]
namespace ProiectMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
