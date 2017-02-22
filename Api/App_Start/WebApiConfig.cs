using System.Web.Http;

namespace WebApiTemplateProject.Api.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Routing will be correspond to the specified Route attribute for each method
            config.MapHttpAttributeRoutes();
        }
    }
}
