using System.Web.Mvc;

namespace TestMVCApp.Areas.SystemAccessing
{
    public class SystemAccessingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SystemAccessing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SystemAccessing_default",
                "SystemAccessing/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}