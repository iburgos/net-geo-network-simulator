using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetIGeo.Domain;
using NetIGeo.Domain.Models;
using NetIGeo.Domain.Services;
using NetIGeo.WebService.Controllers;
using Ploeh.AutoFixture;

namespace NetIGeo.WebService.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTests
    {
        private IFixture _fixture;
        private Mock<IProjectCreationService> _projectCreationServiceMock;
        private Mock<IProjectRetrieverService> _projectRetrieverServiceMock;


        [TestInitialize]
        public void Initialize()
        {
            _fixture =
                new Fixture().Customize(new ApiControllerCustomization<ProjectController>())
                    .Customize(new HttpResponseMessageCustomization());
            _projectRetrieverServiceMock = _fixture.Freeze<Mock<IProjectRetrieverService>>();
            _projectCreationServiceMock = _fixture.Freeze<Mock<IProjectCreationService>>();
        }

        [TestMethod]
        public void Get_WithoutParameters_ReturnsHttpActionResultMessage()
        {
            var sut = _fixture.Create<ProjectController>();
            var result = sut.Get();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void Get_WithoutParameters_CallsProjectRetrieverService()
        {
            var sut = _fixture.Create<ProjectController>();
            sut.Get();
            _projectRetrieverServiceMock.Verify(service => service.Get(), Times.Once());
        }

        [TestMethod]
        public void Get_WithParameterId_ReturnsHttpActionResultMessage()
        {
            var id = _fixture.Create<int>();
            var sut = _fixture.Create<ProjectController>();
            var result = sut.Get(id);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void Get_WithParameterId_CallsProjectRetrieverService()
        {
            var id = _fixture.Create<int>();
            var sut = _fixture.Create<ProjectController>();
            sut.Get(id);
            _projectRetrieverServiceMock.Verify(service => service.Get(id), Times.Once());
        }

        [TestMethod]
        public void Get_RetrieverServiceFails_ReturnsNotFoundResult()
        {
            var id = _fixture.Create<int>();
            _projectRetrieverServiceMock.Setup(service => service.Get(id))
                .Returns(new ServiceResult<ProjectModel> {Success = false});
            var sut = _fixture.Create<ProjectController>();
            var result = sut.Get(id);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}