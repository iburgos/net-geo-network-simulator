using System;
using System.Collections.Generic;

using log4net;

using NetIGeo.DataAccess.Common;

using Raven.Client;

namespace NetIGeo.DataAccess.RavenDb
{
    public interface IDocumentRetriever
    {
        Result<IEnumerable<T>> GetAll<T>();
        Result<T> Get<T>(int id, string documentType);
    }

    public class DocumentRetriever : IDocumentRetriever
    {
        private readonly IDocumentStore _documentStore;
        private readonly ILog _log;

        public DocumentRetriever(IDocumentStore documentStore, ILog log)
        {
            _documentStore = documentStore;
            _log = log;
        }

        public Result<IEnumerable<T>> GetAll<T>()
        {
            var document = new List<T>();
            var success = false;
            try
            {
                using(var documentSession = _documentStore.OpenSession())
                {
                    document.AddRange(documentSession.Query<T>());
                    success = true;
                }
            }
            catch(Exception exception)
            {
                _log.ErrorFormat(
                    $"Exception caught while retrieving purchase orders using filter. Exception: {exception}");
            }
            return new Result<IEnumerable<T>> {Contents = document, Success = success};
        }

        public Result<T> Get<T>(int id, string documentType)
        {
            var result = new Result<T>();
            try
            {
                using(var documentSession = _documentStore.OpenSession())
                {
                    var document = documentSession.Load<T>($"{documentType}/{id}");
                    result.Success = document != null;
                    result.Contents = document;
                }
            }
            catch(Exception exception)
            {
                _log.ErrorFormat(
                    $"Exception caught while retrieving purchase orders using filter. Exception: {exception}");
            }
            return result;
        }
    }
}