using System.Collections.Generic;

using AutoMapper;

using NetIGeo.DataAccess.RavenDb;
using NetIGeo.Domain.Models;

namespace NetIGeo.Domain.Services
{
    public interface IProjectRetrieverService
    {
        ServiceResult<IEnumerable<ProjectModel>> Get();
        ServiceResult<ProjectModel> Get(int id);
    }

    public class ProjectRetrieverService : IProjectRetrieverService
    {
        private readonly IMapper _mapper;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IServiceResultCreator _serviceResultCreator;

        public ProjectRetrieverService(IProjectDocumentRepository projectDocumentRepository,
                                       IServiceResultCreator serviceResultCreator,
                                       IMapper mapper)
        {
            _projectDocumentRepository = projectDocumentRepository;
            _serviceResultCreator = serviceResultCreator;
            _mapper = mapper;
        }

        public ServiceResult<IEnumerable<ProjectModel>> Get()
        {
            var repositoryResult = _projectDocumentRepository.GetAll();
            return _serviceResultCreator.Create(_mapper.Map<IEnumerable<ProjectModel>>(repositoryResult.Contents),
                repositoryResult.Success);
        }

        public ServiceResult<ProjectModel> Get(int id)
        {
            var repositoryResult = _projectDocumentRepository.Get(id);
            return _serviceResultCreator.Create(_mapper.Map<ProjectModel>(repositoryResult.Contents),
                repositoryResult.Success);
        }
    }
}