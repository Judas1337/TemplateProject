using TemplateProject.Sl.WebApi.Model;

namespace TemplateProject.Sl.WebApi.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, Bll.Contract.Bll.Model.Product>().ReverseMap();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}