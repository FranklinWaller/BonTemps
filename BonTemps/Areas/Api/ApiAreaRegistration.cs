using System.Web.Mvc;
using System.Web.Routing;

namespace BonTemps.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{code}",
                new { code = UrlParameter.Optional },
                new[] { "BonTemps.Areas.Api.Controllers" }
            );
        }
    }
}