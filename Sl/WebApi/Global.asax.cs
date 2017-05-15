using System.Web.Http;
using TemplateProject.Sl.WebApi.App_Start;

namespace TemplateProject.Sl.WebApi
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
