using System.Collections.Generic;
using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;

namespace NetIGeo.Domain.Services
{
    public interface IProjectRetrieverService
    {
        ServiceResult<IEnumerable<ProjectDocument>> Get();
    }

    public class ProjectRetrieverService : IProjectRetrieverService
    {
        private readonly IProjectDocumentRetriever _projectDocumentRetriever;
        private readonly IServiceResultCreator _serviceResultCreator;

        public ProjectRetrieverService(IProjectDocumentRetriever projectDocumentRetriever,
            IServiceResultCreator serviceResultCreator)
        {
            _projectDocumentRetriever = projectDocumentRetriever;
            _serviceResultCreator = serviceResultCreator;
        }

        public ServiceResult<IEnumerable<ProjectDocument>> Get()
        {
            var result = _projectDocumentRetriever.Get();
            return _serviceResultCreator.Create(result.Contents, result.Success);
        }
    }
}