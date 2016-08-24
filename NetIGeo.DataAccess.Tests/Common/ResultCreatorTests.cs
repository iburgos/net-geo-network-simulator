using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetIGeo.DataAccess.Common;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace NetIGeo.DataAccess.Tests.Common
{
    [TestClass]
    public class ResultCreatorTests
    {
        private IFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Create_Always_MapsContentAndSuccessParameters()
        {
            var contents = _fixture.Create<GenericParameterHelper>();
            var success = _fixture.Create<bool>();
            var expectedResult = _fixture.Build<Result<GenericParameterHelper>>()
                .With(result => result.Contents, contents)
                .With(result => result.Success, success).Create();

            var sut = _fixture.Create<ResultCreator>();
            var createdResult = sut.Create(contents, success);

            createdResult.ShouldBeEquivalentTo(expectedResult);
        }
    }
}