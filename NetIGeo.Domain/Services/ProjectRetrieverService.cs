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
        private readonly IProjectDocumentRetriever _projectDocumentRetriever;
        private readonly IServiceResultCreator _serviceResultCreator;

        public ProjectRetrieverService(IProjectDocumentRetriever projectDocumentRetriever,
            IServiceResultCreator serviceResultCreator,
            IMapper mapper)
        {
            _projectDocumentRetriever = projectDocumentRetriever;
            _serviceResultCreator = serviceResultCreator;
            _mapper = mapper;
        }

        public ServiceResult<IEnumerable<ProjectModel>> Get()
        {
            var result = _projectDocumentRetriever.Get();
            return _serviceResultCreator.Create(_mapper.Map<IEnumerable<ProjectModel>>(result.Contents), result.Success);
        }

        public ServiceResult<ProjectModel> Get(int id)
        {
            var result = _projectDocumentRetriever.Get(id);
            return _serviceResultCreator.Create(_mapper.Map<ProjectModel>(result.Contents), result.Success);
        }
    }
}