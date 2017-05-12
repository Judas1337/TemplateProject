using Autofac;
using TemplateProject.Bll;
using TemplateProject.Bll.Contract.Bll.Interface;
using TemplateProject.Bll.Contract.Dal;
using TemplateProject.Dal.WebApi;


namespace TemplateProject.CompositionRoot
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
