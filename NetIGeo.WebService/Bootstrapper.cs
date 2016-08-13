using System.Web.Http;
using AutoMapper;
using Microsoft.Practices.Unity;
using NetIGeo.DataAccess.Bootstrappers;
using NetIGeo.Domain;
using NetIGeo.Domain.Extensions;
using Unity.WebApi;
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
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container);
            Container.AddNewExtension<Log4NetExtension>();
            Container.AddNewExtension<DomainExtension>();
            Container.AddExtension(new RavenDbExtension(_settings.RavenDbLocation,
                _settings.RavenDbDatabase));

            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMissingTypeMaps = true;
                config.AddProfile(new DomainProfile(config));
            });

            Container.RegisterInstance(mappingConfiguration.CreateMapper());
        }
    }
}