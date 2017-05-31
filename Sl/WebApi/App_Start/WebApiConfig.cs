using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using TemplateProject.Sl.WebApi.ExceptionHandler;
using TemplateProject.Sl.WebApi.Filter;
using TemplateProject.Sl.WebApi.MessageHandler;
using TemplateProject.Utilities.Concurrency;

namespace TemplateProject.Sl.WebApi.App_Start
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
            //Routing will be correspond to the specified Route attribute for each method
            config.MapHttpAttributeRoutes();
            //Declare the project to return JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //If a null value exists, ignore value
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            var correlationIdValueProvider = CorrelationIdProvider.Instance;
           
            //RegisterFilters(config);
            RegisterServices(config);
            RegisterMessageHandlers(config, correlationIdValueProvider);
        }

        #region Registration Methods
        private static void RegisterServices(HttpConfiguration config)
        {
            //Register implementation of IExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }

        private static void RegisterFilters(HttpConfiguration config)
        {
            //Register Global Filter for ModelValidation
            config.Filters.Add(new ModelValidationFilter());
        }

        private static void RegisterMessageHandlers(HttpConfiguration config, ICorrelationIdProvider correlationIdProvider)
        {
            config.MessageHandlers.Add(new CorrelationHandler(correlationIdProvider));
            config.MessageHandlers.Add(new MessageLoggingHandler(correlationIdProvider));
        }
        #endregion
    }
}
