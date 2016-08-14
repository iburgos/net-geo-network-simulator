using System;
using System.Collections.Generic;
using log4net;
using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.Documents;
using Raven.Client;

namespace NetIGeo.DataAccess.RavenDb
{
    public interface IProjectDocumentRetriever
    {
        Result<IEnumerable<ProjectDocument>> Get();
    }

    public class ProjectDocumentRetriever : IProjectDocumentRetriever
    {
        private readonly IDocumentStore _documentStore;
        private readonly ILog _log;

        public ProjectDocumentRetriever(IDocumentStore documentStore, ILog log)
        {
            _documentStore = documentStore;
            _log = log;
        }

        public Result<IEnumerable<ProjectDocument>> Get()
        {
            var document = new List<ProjectDocument>();
            var success = false;
            try
            {
                using (var documentSession = _documentStore.OpenSession())
                {
                    document.AddRange(documentSession.Query<ProjectDocument>());
                    success = true;
                }
            }
            catch (Exception exception)
            {
                _log.ErrorFormat(
                    $"Exception caught while retrieving purchase orders using filter. Exception: {exception}");
            }
            return new Result<IEnumerable<ProjectDocument>> {Contents = document, Success = success};
        }
    }
}