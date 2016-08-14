using Microsoft.Practices.Unity;
using NetIGeo.Domain.Services;

namespace NetIGeo.Domain.Extensions
{
    public class DomainExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IProjectCreationService, ProjectCreationService>();
            Container.RegisterType<IProjectRetrieverService, ProjectRetrieverService>();
            Container.RegisterType<IServiceResultCreator, ServiceResultCreator>();
        }
    }
}