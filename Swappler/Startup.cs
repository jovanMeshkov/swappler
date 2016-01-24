using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Swappler.Startup))]
namespace Swappler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
