using System;
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
    /// <summary>
    /// Configuration class responsible for WebApi specific configurations.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers WebApi specific components.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            //Application insights telemetry is not interesting for us in debug so it's disabled
            TelemetryConfiguration.Active.DisableTelemetry = true;

            //Routing will be correspond to the specified Route attribute for each method
            config.MapHttpAttributeRoutes();
            //Declare the project to return JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            var correlationIdValueProvider = CorrelationIdValueProvider.Instance;

            RegisterServices(config, correlationIdValueProvider);
            RegisterFilters(config);
            RegisterMessageHandlers(config, correlationIdValueProvider);
        }

        #region Registration Methods
        private static void RegisterServices(HttpConfiguration config, ICorrelationIdValueProvider<Guid?> correlationIdValueProvider)
        {
            //Register implementation of IExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler(correlationIdValueProvider));

            //Register implementation of IExceptionLogger
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger(correlationIdValueProvider));
        }

        private static void RegisterFilters(HttpConfiguration config)
        {
            //Register Global Filter for ModelValidation
            config.Filters.Add(new ModelValidationFilter());
        }

        private static void RegisterMessageHandlers(HttpConfiguration config, ICorrelationIdValueProvider<Guid?> correlationIdValueProvider)
        {
            config.MessageHandlers.Add(new CorrelationHandler(correlationIdValueProvider));
            config.MessageHandlers.Add(new MessageLoggingHandler(correlationIdValueProvider));
        }
        #endregion
    }
}
