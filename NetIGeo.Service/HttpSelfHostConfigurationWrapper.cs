using System.Web.Http;
using System.Web.Http.SelfHost;

namespace NetIGeo.Service
{
    public interface IHttpSelfHostConfigurationWrapper
    {
        HttpSelfHostConfiguration Create();
    }

    public class HttpSelfHostConfigurationWrapper : IHttpSelfHostConfigurationWrapper
    {
        private readonly string _baseUrl;

        public HttpSelfHostConfigurationWrapper(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public HttpSelfHostConfiguration Create()
        {
            var config = new HttpSelfHostConfiguration(_baseUrl);
            config.DependencyResolver.BeginScope();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            return config;
        }
    }
}