using System.Collections.Generic;
using AutoMapper;
using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.Documents;

namespace NetIGeo.DataAccess.RavenDb
{
    public interface IProjectDocumentRepository
    {
        Result<ProjectDocument> Create(ProjectDocument projectDocument);
        Result<IEnumerable<ProjectDocument>> GetAll();
        Result<ProjectDocument> Get(int id);
        bool Delete(int id);
    }

    public class ProjectDocumentRepository : IProjectDocumentRepository
    {
        private readonly IDocumentDeleter _documentDeleter;
        private readonly IDocumentRetriever _documentRetriever;
        private readonly IDocumentStorer _documentStorer;
        private readonly IMapper _mapper;
        private readonly IResultCreator _resultCreator;

        public ProjectDocumentRepository(IDocumentStorer documentStorer,
            IDocumentRetriever documentRetriever,
            IDocumentDeleter documentDeleter,
            IResultCreator resultCreator,
            IMapper mapper)
        {
            _documentStorer = documentStorer;
            _documentRetriever = documentRetriever;
            _documentDeleter = documentDeleter;
            _resultCreator = resultCreator;
            _mapper = mapper;
        }

        public Result<ProjectDocument> Create(ProjectDocument projectDocument)
        {
            var storerResult = _documentStorer.Store(projectDocument);
            return _resultCreator.Create(_mapper.Map<ProjectDocument>(storerResult.Contents), storerResult.Success);
        }

        public Result<IEnumerable<ProjectDocument>> GetAll()
        {
            var result = _documentRetriever.GetAll<ProjectDocument>();
            return _resultCreator.Create(result.Contents, result.Success);
        }

        public Result<ProjectDocument> Get(int id)
        {
            var result = _documentRetriever.Get<ProjectDocument>(id,
                RavenDbConstants.PROJECT_DOCUMENT_TYPE);
            return _resultCreator.Create(result.Contents, result.Success);
        }

        public bool Delete(int id)
        {
            return _documentDeleter.Delete<ProjectDocument>(id, RavenDbConstants.PROJECT_DOCUMENT_TYPE);
        }
    }
}