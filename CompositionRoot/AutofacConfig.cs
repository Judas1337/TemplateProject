using Autofac;
using WebApiTemplateProject.Bll;
using WebApiTemplateProject.Bll.Contract.Bll.Interface;
using WebApiTemplateProject.Bll.Contract.Dal.Interface;
using WebApiTemplateProject.Dal.WebApi;


namespace WebApiTemplateProject.CompositionRoot
{
    /// <summary>
    /// Register all dependencies for Bll and Dal layers
    /// </summary>
    public class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            // Register your Web API controller dependencies.
            builder.RegisterType<ProductLogic>().As<IProductLogic>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
        }
    }
}
