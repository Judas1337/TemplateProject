using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApiTemplateProject.Api.App_Start;
using WebApiTemplateProject.Utilities.ExceptionHandler;
using WebApiTemplateProject.Utilities.Filter;
using WebApiTemplateProject.Utilities.HttpMessageHandler;
using WebApiTemplateProject.Utilities.Logger;

namespace WebApiTemplateProject.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Declare the project to return JSON instead of XML
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

           // Register routing
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Register swagger
            GlobalConfiguration.Configure(SwaggerConfig.Register);

            //Configure Autofac Dependency container
            GlobalConfiguration.Configure(AutofacConfig.Register);

            //Register implementation of IExceptionHandler
            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //Register implementation of IExceptionLogger
            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());

            //Register Global Filter for ModelValidation
            GlobalConfiguration.Configuration.Filters.Add(new ModelValidationFilter());

            //Register MessageHandlers
            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorrelationHandler());

            //Ensure configuration 
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
