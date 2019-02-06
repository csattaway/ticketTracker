using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ticketTracker.Startup))]
namespace ticketTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
