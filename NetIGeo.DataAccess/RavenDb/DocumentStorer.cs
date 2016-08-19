using System;
using log4net;

using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.Documents;
using Raven.Client;

namespace NetIGeo.DataAccess.RavenDb
{
    public interface IDocumentStorer
    {
        Result<IDocument> Store(IDocument document);
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

        public Result<IDocument> Store(IDocument document)
        {
            IDocument result = document;
            bool success = false;
            try
            {
                using (var documentSession = _documentStore.OpenSession())
                {
                    documentSession.Store(document);
                    documentSession.SaveChanges();
                    success = true;
                }
            }
            catch (Exception exception)
            {
                _log.ErrorFormat("Exception caught while inserting document of type {0}. Exception: {1}",
                    document.GetType(), exception);
            }

            return new Result<IDocument> { Contents = document, Success = success};
        }
    }
}