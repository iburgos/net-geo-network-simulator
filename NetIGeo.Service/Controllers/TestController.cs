using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetIGeo.Service.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, "Chachi");
        }
    }
}