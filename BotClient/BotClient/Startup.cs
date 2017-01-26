using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BotClient.Startup))]
namespace BotClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
