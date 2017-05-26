using System.Web;
using TemplateProject.Sl.Wcf.App_Start;

namespace TemplateProject.Sl.Wcf
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AutomapperConfig.RegisterMappings();
            AutofacConfig.Register();
        }        
    }
}