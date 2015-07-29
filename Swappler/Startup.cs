using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Swappler.Startup))]
namespace Swappler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
