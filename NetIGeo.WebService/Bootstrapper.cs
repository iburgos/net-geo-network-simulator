using Microsoft.Practices.Unity;
using NetIGeo.DataAccess.Bootstrappers;
using UnityLog4NetExtension.Log4Net;

namespace NetIGeo.WebService
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
            Container.AddExtension(new RavenDbExtension(_settings.RavenDbLocation,
                _settings.RavenDbDatabase));
        }
    }
}