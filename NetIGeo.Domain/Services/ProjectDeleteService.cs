using AutoMapper;
using NetIGeo.DataAccess.RavenDb;

namespace NetIGeo.Domain.Services
{
    public interface IProjectDeleteService
    {
        bool Delete(int id);
    }

    public class ProjectDeleteService : IProjectDeleteService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IServiceResultCreator _serviceResultCreator;

        public ProjectDeleteService(IProjectDocumentRepository projectDocumentRepository)
        {
            _projectDocumentRepository = projectDocumentRepository;
        }

        public bool Delete(int id)
        {
            return _projectDocumentRepository.Delete(id);
        }
    }
}