using AutoMapper;

using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;
using NetIGeo.Domain.Models;

namespace NetIGeo.Domain.Services
{
    public interface IProjectCreationService
    {
        ServiceResult<ProjectModel> Create(ProjectModel project);
    }

    public class ProjectCreationService : IProjectCreationService
    {
        private readonly IDocumentStorer _documentStorer;
        private readonly IMapper _mapper;
        private readonly IServiceResultCreator _serviceResultCreator;

        public ProjectCreationService(IDocumentStorer documentStorer,
                                      IMapper mapper,
                                      IServiceResultCreator serviceResultCreator)
        {
            _documentStorer = documentStorer;
            _mapper = mapper;
            _serviceResultCreator = serviceResultCreator;
        }

        public ServiceResult<ProjectModel> Create(ProjectModel project)
        {
            var storerResult = _documentStorer.Store(_mapper.Map<ProjectDocument>(project));
            return _serviceResultCreator.Create(_mapper.Map<ProjectModel>(storerResult.Contents), storerResult.Success);
        }
    }
}