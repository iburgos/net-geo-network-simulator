using System;
using System.Collections.Generic;

using log4net;

using NetIGeo.DataAccess.Common;

using Raven.Client;

namespace NetIGeo.DataAccess.RavenDb
{
    public interface IDocumentDeleter
    {
        bool Delete<T>(int id, string documentType);
    }

    public class DocumentDeleter : IDocumentDeleter
    {
        private readonly IDocumentStore _documentStore;
        private readonly ILog _log;

        public DocumentDeleter(IDocumentStore documentStore, ILog log)
        {
            _documentStore = documentStore;
            _log = log;
        }

        public bool Delete<T>(int id, string documentType)
        {
            var success = false;
            try
            {
                using(var documentSession = _documentStore.OpenSession())
                {
                    var document = documentSession.Load<T>($"{documentType}/{id}");
                    documentSession.Delete<T>(document);
                    documentSession.SaveChanges();
                    success = true;
                }
            }
            catch(Exception exception)
            {
                _log.ErrorFormat(
                    $"Exception caught while retrieving purchase orders using filter. Exception: {exception}");
            }
            return success;
        }
    }
}