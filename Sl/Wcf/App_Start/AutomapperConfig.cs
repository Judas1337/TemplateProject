using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateProject.Sl.Wcf.Model;

namespace TemplateProject.Sl.Wcf.App_Start
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