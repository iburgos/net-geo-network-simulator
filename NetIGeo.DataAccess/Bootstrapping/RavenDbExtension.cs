using Microsoft.Practices.Unity;
using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.RavenDb;
using Raven.Client;
using Raven.Client.Document;

namespace NetIGeo.DataAccess.Bootstrappers
{
    public class RavenDbExtension : UnityContainerExtension
    {
        private readonly string _ravenDbDatabase;
        private readonly string _ravenDbLocation;

        public RavenDbExtension(string ravenDbLocation, string ravenDbDatabase)
        {
            _ravenDbLocation = ravenDbLocation;
            _ravenDbDatabase = ravenDbDatabase;
        }
        protected override void Initialize()
        {
            var documentStore = new DocumentStore
            {
                Url = _ravenDbLocation,
                DefaultDatabase = _ravenDbDatabase
            }.Initialize();

            Container.RegisterInstance(documentStore, new ContainerControlledLifetimeManager());
            Container.RegisterType<IDocumentStorer, DocumentStorer>();
            Container.RegisterType<IResultCreator, ResultCreator>();
        }
    }
}