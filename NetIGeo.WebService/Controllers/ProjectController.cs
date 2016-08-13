using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetIGeo.WebService.Controllers
{
    public class ProjectController : ApiController
    {
        public IHttpActionResult Get()
        {
            return InternalServerError();
        }
    }
}
