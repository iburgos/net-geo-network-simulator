using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;
using NetIGeo.Domain.Models;
using NetIGeo.Domain.Services;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace NetIGeo.Domain.Tests.Services
{
    [TestClass]
    public class ProjectCreationServiceTests
    {
        private Mock<IDocumentStorer> _documentStorerMock;
        private IFixture _fixture;
        private Mock<IMapper> _mapperMock;
        private Mock<IServiceResultCreator> _serviceResultCreatorMock;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _documentStorerMock = _fixture.Freeze<Mock<IDocumentStorer>>();
            _mapperMock = _fixture.Freeze<Mock<IMapper>>();
            _serviceResultCreatorMock = _fixture.Freeze<Mock<IServiceResultCreator>>();
        }

        [TestMethod]
        public void Create_Always_MapsProjectDocument()
        {
            var project = _fixture.Create<ProjectModel>();

            var sut = _fixture.Create<ProjectCreationService>();
            sut.Create(project);

            _mapperMock.Verify(mapper => mapper.Map<ProjectDocument>(project), Times.Once());
        }

        [TestMethod]
        public void Create_Always_CallsDocumentStorer()
        {
            var project = _fixture.Create<ProjectModel>();

            var sut = _fixture.Create<ProjectCreationService>();
            sut.Create(project);

            _documentStorerMock.Verify(storer => storer.Store(It.IsAny<ProjectDocument>()), Times.Once());
        }

        [TestMethod]
        public void Create_Always_CallsServiceResultCreator()
        {
            var project = _fixture.Create<ProjectModel>();

            var sut = _fixture.Create<ProjectCreationService>();
            sut.Create(project);

            _serviceResultCreatorMock.Verify(creator => creator.Create(project, It.IsAny<bool>()), Times.Once());
        }
    }
}