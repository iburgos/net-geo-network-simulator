using System.Web.Http;
using NetIGeo.WebService;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace NetIGeo.WebService
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "NetIGeo.WebService"); })
                .EnableSwaggerUi(c => { });
        }
    }
}