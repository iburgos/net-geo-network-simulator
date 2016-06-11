using Microsoft.Practices.Unity;
using UnityLog4NetExtension.Log4Net;

namespace NetIGeo.Service.Bootstrappers
{
    public class Bootstrapper : UnityContainerExtension
    {
        private readonly Settings _settings;

        public Bootstrapper(Settings settings)
        {
            _settings = settings;
        }

        protected override void Initialize()
        {
            Container.AddNewExtension<Log4NetExtension>();
            Container.RegisterType<ServiceStarter>();
            Container.RegisterType<IHttpSelfHostConfigurationWrapper, HttpSelfHostConfigurationWrapper>(
                new InjectionConstructor(_settings.BaseUrl));
        }
    }
}