using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace NetIGeo.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );

            config.Services.Add(typeof(IExceptionLogger), new Log4NetExceptionLogger());
        }
    }
}