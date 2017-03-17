using System.Net.Http.Headers;
using System.Web.Http;
using WebApiTemplateProject.Api.App_Start;

namespace WebApiTemplateProject.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Declare the project to return JSON instead of XML
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
          
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SwaggerConfig.Register);
            GlobalConfiguration.Configure(AutofacConfig.Register);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
