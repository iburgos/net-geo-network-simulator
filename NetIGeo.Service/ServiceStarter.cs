using System.Web.Http.SelfHost;

namespace NetIGeo.Service
{
    public class ServiceStarter
    {
        private readonly IHttpSelfHostConfigurationWrapper _httpSelfHostConfigurationWrapper;

        public ServiceStarter(IHttpSelfHostConfigurationWrapper httpSelfHostConfigurationWrapper)
        {
            _httpSelfHostConfigurationWrapper = httpSelfHostConfigurationWrapper;
        }

        public void Start()
        {
            using (var server = new HttpSelfHostServer(_httpSelfHostConfigurationWrapper.Create()))
            {
                server.OpenAsync().Wait();
            }
        }
    }
}