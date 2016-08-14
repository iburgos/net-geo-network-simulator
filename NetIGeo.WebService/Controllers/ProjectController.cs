using System.Collections.Generic;
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
        private readonly IProjectRetrieverService _projectRetrieverService;

        public ProjectController(IProjectCreationService projectCreationService,
            IProjectRetrieverService projectRetrieverService,
            IMapper mapper)
        {
            _projectCreationService = projectCreationService;
            _projectRetrieverService = projectRetrieverService;
            _mapper = mapper;
        }

        public IHttpActionResult Get()
        {
            IHttpActionResult result = InternalServerError();

            var serviceResult = _projectRetrieverService.Get();
            if (serviceResult.Success)
                result = Ok(_mapper.Map<IEnumerable<ProjectDto>>(serviceResult.Contents));

            return result;
        }

        [HttpPost]
        public
        IHttpActionResult New(
            [FromBody] ProjectDto project)
        {
            IHttpActionResult result = InternalServerError();

            if (
                project
                != null)
            {
                var projectModel = _mapper.Map<ProjectModel>(project);
                var serviceResult = _projectCreationService.Create(projectModel);
                if (serviceResult.Success)
                    result = Ok(_mapper.Map<ProjectDto>(serviceResult.Contents));
            }
            else
                result = BadRequest();

            return result;
        }
    }
}