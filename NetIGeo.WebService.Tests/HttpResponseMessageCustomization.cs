using Ploeh.AutoFixture;

namespace NetIGeo.WebService.Tests
{
    public class HttpResponseMessageCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            //fixture.Customize<HttpRequestMessage>(c => c.Without(x => x.Content)
            //    .Do(x => x.Properties[Constants.BODY_PROPERTY_NAME] =
            //        fixture.Create<string>()));
        }
    }
}