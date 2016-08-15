using System.Web.Http;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace NetIGeo.WebService.Tests
{
    public class ApiControllerCustomization<T> : ICustomization where T : ApiController
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize(new AutoConfiguredMoqCustomization());
            fixture.Customize<T>(c => c.OmitAutoProperties().With(x => x.Request));
        }
    }
}