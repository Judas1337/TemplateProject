using System.Web.Http;
using System.Web.Http.ExceptionHandling;
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
            config.MapHttpAttributeRoutes();  //Routing will be correspond to the specified Route attribute for each method
            RegisterServices(config);
            RegisterFilters(config);
            RegisterMessageHandlers(config);
        }

        #region Registration Methods
        private static void RegisterServices(HttpConfiguration config)
        {
            //Register implementation of IExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //Register implementation of IExceptionLogger
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
        }

        private static void RegisterFilters(HttpConfiguration config)
        {
            //Register Global Filter for ModelValidation
            config.Filters.Add(new ModelValidationFilter());
        }

        private static void RegisterMessageHandlers(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new CorrelationHandler(new ExecutionContextValueProvider()));
        }
        #endregion
    }
}
