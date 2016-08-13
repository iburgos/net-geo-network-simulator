using AutoMapper;
using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;
using NetIGeo.Domain.Models;

namespace NetIGeo.Domain.Services
{
    public interface IProjectCreationService
    {
        bool Create(ProjectModel project);
    }

    public class ProjectCreationService : IProjectCreationService
    {
        private readonly IDocumentStorer _documentStorer;
        private readonly IMapper _mapper;

        public ProjectCreationService(IDocumentStorer documentStorer, IMapper mapper)
        {
            _documentStorer = documentStorer;
            _mapper = mapper;
        }

        public bool Create(ProjectModel project)
        {
            var projectDocument = _mapper.Map<ProjectDocument>(project);
            return _documentStorer.Store(projectDocument);
        }
    }
}