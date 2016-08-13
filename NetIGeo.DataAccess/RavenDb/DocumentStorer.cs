using System;
using log4net;
using NetIGeo.DataAccess.Documents;
using Raven.Client;

namespace NetIGeo.DataAccess.RavenDb
{
    public interface IDocumentStorer
    {
        bool Store(IDocument document);
    }

    public class DocumentStorer : IDocumentStorer
    {
        private readonly IDocumentStore _documentStore;
        private readonly ILog _log;

        public DocumentStorer(IDocumentStore documentStore, ILog log)
        {
            _documentStore = documentStore;
            _log = log;
        }

        public bool Store(IDocument document)
        {
            var successful = false;

            try
            {
                using (var documentSession = _documentStore.OpenSession())
                {
                    documentSession.Store(document);
                    documentSession.SaveChanges();
                }
                successful = true;
            }
            catch (Exception exception)
            {
                _log.ErrorFormat("Exception caught while inserting document of type {0}. Exception: {1}",
                    document.GetType(), exception);
            }

            return successful;
        }
    }
}