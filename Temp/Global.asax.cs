﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using Temp.Models;
using Temp.Logic;
using Temp.DataAccess;

namespace Temp
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

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());           

            // Register your Web API controller dependencies.
            builder.RegisterType<ProductLogic>().As<IProductLogic>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            
            //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //Ensure configuration 
            config.EnsureInitialized();
        }
    }
}
