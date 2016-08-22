using AutoMapper;

using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;
using NetIGeo.Domain.Models;

namespace NetIGeo.Domain.Services
{
    public interface IProjectUpdateService
    {
        ServiceResult<ProjectModel> Update(ProjectModel project);
    }

    public class ProjectUpdateService : IProjectUpdateService
    {
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IServiceResultCreator _serviceResultCreator;

        public ProjectUpdateService(IProjectDocumentRepository projectDocumentRepository,
                                    IServiceResultCreator serviceResultCreator,
                                    IMapper mapper,
                                    IDateTimeProvider dateTimeProvider)
        {
            _projectDocumentRepository = projectDocumentRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _serviceResultCreator = serviceResultCreator;
        }

        public ServiceResult<ProjectModel> Update(ProjectModel project)
        {
            project.UpdateDate = _dateTimeProvider.NowUTC();
            Result<ProjectDocument> storerResult =
                _projectDocumentRepository.Create(_mapper.Map<ProjectDocument>(project));
            return _serviceResultCreator.Create(_mapper.Map<ProjectModel>(storerResult.Contents), storerResult.Success);
        }
    }
}