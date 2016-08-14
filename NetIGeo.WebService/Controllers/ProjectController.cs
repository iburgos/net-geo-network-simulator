using System.Web.Http;
using AutoMapper;
using NetIGeo.Domain.Models;
using NetIGeo.Domain.Services;
using NetIGeo.WebService.Dtos;

namespace NetIGeo.WebService.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IProjectCreationService _projectCreationService;

        public ProjectController(IProjectCreationService projectCreationService,
            IMapper mapper)
        {
            _projectCreationService = projectCreationService;
            _mapper = mapper;
        }

        public IHttpActionResult Get()
        {
            return InternalServerError();
        }

        [HttpPost]
        public IHttpActionResult New([FromBody] ProjectDto project)
        {
            IHttpActionResult result = InternalServerError();

            if (project != null)
            {
                var projectModel = _mapper.Map<ProjectModel>(project);
                var serviceResult = _projectCreationService.Create(projectModel);
                if (serviceResult.Success)
                {
                    result = Ok(_mapper.Map<ProjectDto>(serviceResult.Contents));
                }
            }

            return result;
        }
    }
}