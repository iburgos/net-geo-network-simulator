using Microsoft.Practices.Unity;
using NetIGeo.Domain.Services;

namespace NetIGeo.Domain.Extensions
{
    public class DomainExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IProjectCreationService, ProjectCreationService>();
            Container.RegisterType<IProjectUpdateService, ProjectUpdateService>();
            Container.RegisterType<IProjectRetrieverService, ProjectRetrieverService>();
            Container.RegisterType<IProjectDeleteService, ProjectDeleteService>();
            Container.RegisterType<IServiceResultCreator, ServiceResultCreator>();
            Container.RegisterType<IDateTimeProvider, DateTimeProvider>();
        }
    }
}