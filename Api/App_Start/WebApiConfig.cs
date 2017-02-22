using System.Web.Http;

namespace Api.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            /*Enable attribute routing, eg attribute with routing info above methods
             *Example: 
             * [Route("{id:int})]
             * public GetById(int id){}
             */
            config.MapHttpAttributeRoutes();

            //This routetemplate will respond to http://adress/api/controller/method/inputParameter
            //Example  http://localhost:61837/api/products/getproduct/1
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );           
        }
    }
}
