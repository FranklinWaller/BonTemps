using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarksEvilPracticum.Startup))]
namespace MarksEvilPracticum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
