using System.Web.Http;
using WebApiTemplateProject.Sl.WebApi.App_Start;

namespace WebApiTemplateProject.Sl.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SwaggerConfig.Register);
            GlobalConfiguration.Configure(AutofacConfig.Register);
            GlobalConfiguration.Configuration.EnsureInitialized();

            AutomapperConfig.RegisterMappings();
        }
    }
}
