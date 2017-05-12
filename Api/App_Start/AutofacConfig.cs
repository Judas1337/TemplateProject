using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WebApiTemplateProject.Api.DataAccess;
using WebApiTemplateProject.Api.Logic;

namespace WebApiTemplateProject.Api.App_Start
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Create an Autofac ContainerBuilder
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register your Web API controller dependencies.
            builder.RegisterType<ProductLogic>().As<IProductLogic>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

            //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}