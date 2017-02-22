using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApiTemplateProject.Api.App_Start;
using WebApiTemplateProject.Utilities.ExceptionHandler;
using WebApiTemplateProject.Utilities.Filter;
using WebApiTemplateProject.Utilities.Logger;

namespace WebApiTemplateProject.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            var config = GlobalConfiguration.Configuration;

            //Declare the project to return JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Register routing
            WebApiConfig.Register(config);

            //Register swagger
            SwaggerConfig.Register(config);

            //Configure Autofac Dependency container
            AutofacConfig.Register(config);

            //Register implementation of IExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //Register implementation of IExceptionLogger
            config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());

            //Register Global Filter for ModelValidation
            config.Filters.Add(new ModelValidationFilter());

            //Ensure configuration 
            config.EnsureInitialized();
        }
    }
}
