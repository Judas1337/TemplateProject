using Autofac;
using Autofac.Integration.Wcf;

namespace TemplateProject.Sl.Wcf.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ProductService>().As<IProductService>();
            CompositionRoot.AutofacConfig.Register(builder);
           
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}