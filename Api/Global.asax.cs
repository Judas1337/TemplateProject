using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Api.DataAccess;
using Api.Logic;
using Api.Utilities;
using Autofac;
using Autofac.Integration.WebApi;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            //GlobalConfiguration.Configure(WebApiConfig.Register);
            var config = GlobalConfiguration.Configuration;

            //Declare the project to return JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));           
           
            // Register routing
            WebApiConfig.Register(config);

            //Register swagger
            SwaggerConfig.Register(config);

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());           

            // Register your Web API controller dependencies.
            builder.RegisterType<ProductLogic>().As<IProductLogic>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            
            //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //Register implementation of IExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            
            //Register implementation of IExceptionLogger
            config.Services.Replace(typeof(IExceptionLogger), new MyExceptionLogger());

            //Register Global Filter for ModelValidation
            config.Filters.Add(new ModelValidationFilter());

            //Ensure configuration 
            config.EnsureInitialized();
        }
    }
}
