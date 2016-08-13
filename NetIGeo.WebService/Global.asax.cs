using System.Web;
using System.Web.Http;
using log4net.Config;
using Microsoft.Practices.Unity;

namespace NetIGeo.WebService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            var container = new UnityContainer();
            container.AddNewExtension<Bootstrapper>();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}