using System.Collections.Generic;

using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.Documents;

namespace NetIGeo.DataAccess.RavenDb
{
    public interface IProjectDocumentRepository
    {
        Result<ProjectDocument> Create(ProjectDocument projectDocument);
        Result<IEnumerable<ProjectDocument>> GetAll();
        Result<ProjectDocument> Get(int id);
    }

    public class ProjectDocumentRepository : IProjectDocumentRepository
    {
        private const string DOCUMENT_TYPE = "ProjectDocuments";
        private readonly IDocumentRetriever _documentRetriever;
        private readonly IDocumentStorer _documentStorer;
        private readonly IResultCreator _resultCreator;

        public ProjectDocumentRepository(IDocumentStorer documentStorer,
                                         IDocumentRetriever documentRetriever,
                                         IResultCreator resultCreator)
        {
            _documentStorer = documentStorer;
            _documentRetriever = documentRetriever;
            _resultCreator = resultCreator;
        }

        public Result<ProjectDocument> Create(ProjectDocument projectDocument)
        {
            return _resultCreator.Create(projectDocument, _documentStorer.Store(projectDocument));
        }

        public Result<IEnumerable<ProjectDocument>> GetAll()
        {
            var result = _documentRetriever.GetAll<ProjectDocument>();
            return _resultCreator.Create(result.Contents, result.Success);
        }

        public Result<ProjectDocument> Get(int id)
        {
            var result = _documentRetriever.Get<ProjectDocument>(id, DOCUMENT_TYPE);
            return _resultCreator.Create(result.Contents, result.Success);
        }
    }
}