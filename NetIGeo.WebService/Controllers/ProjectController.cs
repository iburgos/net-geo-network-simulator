using System.Web.Http;
using NetIGeo.WebService.Dtos;

namespace NetIGeo.WebService.Controllers
{
    public class ProjectController : ApiController
    {
        public IHttpActionResult Get()
        {
            return InternalServerError();
        }

        [HttpPost]
        public IHttpActionResult New([FromBody] ProjectDto project)
        {
            return InternalServerError();
        }
    }
}