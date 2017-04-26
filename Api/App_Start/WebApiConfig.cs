using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights.Extensibility;
using WebApiTemplateProject.Utilities.Concurrency;
using WebApiTemplateProject.Utilities.ExceptionHandler;
using WebApiTemplateProject.Utilities.Filter;
using WebApiTemplateProject.Utilities.Logger;
using WebApiTemplateProject.Utilities.MessageHandler;

namespace WebApiTemplateProject.Api.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Application insights telemetry is not interesting for us in debug so it's disabled
            TelemetryConfiguration.Active.DisableTelemetry = true;

            //Routing will be correspond to the specified Route attribute for each method
            config.MapHttpAttributeRoutes();
            //Declare the project to return JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));  

            var executionContextValueProvider = new ExecutionContextValueProvider();

            RegisterServices(config, executionContextValueProvider);
            RegisterFilters(config);
            RegisterMessageHandlers(config, executionContextValueProvider);
        }

        #region Registration Methods
        private static void RegisterServices(HttpConfiguration config, IExecutionContextValueProvider executionContextValueProvider)
        {
            //Register implementation of IExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //Register implementation of IExceptionLogger
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger(executionContextValueProvider));
        }

        private static void RegisterFilters(HttpConfiguration config)
        {
            //Register Global Filter for ModelValidation
            config.Filters.Add(new ModelValidationFilter());
        }

        private static void RegisterMessageHandlers(HttpConfiguration config, IExecutionContextValueProvider executionContextValueProvider)
        {
            config.MessageHandlers.Add(new CorrelationHandler(executionContextValueProvider));
            config.MessageHandlers.Add(new MessageLoggingHandler(executionContextValueProvider));
        }
        #endregion
    }
}
