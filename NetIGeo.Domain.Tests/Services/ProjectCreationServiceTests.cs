using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetIGeo.DataAccess.Common;
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
        private Mock<IProjectDocumentRepository> _projectDocumentRepositoryMock;
        private IFixture _fixture;
        private Mock<IMapper> _mapperMock;
        private Mock<IServiceResultCreator> _serviceResultCreatorMock;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _projectDocumentRepositoryMock = _fixture.Freeze<Mock<IProjectDocumentRepository>>();
            _mapperMock = _fixture.Freeze<Mock<IMapper>>();
            _serviceResultCreatorMock = _fixture.Freeze<Mock<IServiceResultCreator>>();
        }

        [TestMethod]
        public void Create_Always_MapsProjectDocumentFromProjectModel()
        {
            var project = _fixture.Create<ProjectModel>();

            var sut = _fixture.Create<ProjectCreationService>();
            sut.Create(project);

            _mapperMock.Verify(mapper => mapper.Map<ProjectDocument>(project), Times.Once());
        }

        [TestMethod]
        public void Create_Always_MapsProjectModelFromProjectDocument()
        {
            var projectModel = _fixture.Create<ProjectModel>();
            var projectDocumentToStore = _fixture.Create<ProjectDocument>();
            var projectDocumentStored = _fixture.Create<ProjectDocument>();
            var storeResult =
                _fixture.Build<Result<ProjectDocument>>().With(result => result.Contents, projectDocumentStored).Create();
            _mapperMock.Setup(mapper => mapper.Map<ProjectDocument>(projectModel)).Returns(projectDocumentToStore);
            _projectDocumentRepositoryMock.Setup(storer => storer.Create(projectDocumentToStore)).Returns(storeResult);

            var sut = _fixture.Create<ProjectCreationService>();
            sut.Create(projectModel);

            _mapperMock.Verify(mapper => mapper.Map<ProjectModel>(projectDocumentStored), Times.Once());
        }

        [TestMethod]
        public void Create_Always_CallsDocumentStorer()
        {
            var project = _fixture.Create<ProjectModel>();

            var sut = _fixture.Create<ProjectCreationService>();
            sut.Create(project);

            _projectDocumentRepositoryMock.Verify(storer => storer.Create(It.IsAny<ProjectDocument>()), Times.Once());
        }

        [TestMethod]
        public void Create_Always_CallsServiceResultCreator()
        {
            var projectModel = _fixture.Create<ProjectModel>();
            var projectModelStored = _fixture.Create<ProjectModel>();
            var projectDocumentToStore = _fixture.Create<ProjectDocument>();
            var projectDocumentStored = _fixture.Create<ProjectDocument>();
            var storeResult =
                _fixture.Build<Result<ProjectDocument>>().With(result => result.Contents, projectDocumentStored).Create();

            _mapperMock.Setup(mapper => mapper.Map<ProjectDocument>(projectModel)).Returns(projectDocumentToStore);
            _projectDocumentRepositoryMock.Setup(storer => storer.Create(projectDocumentToStore)).Returns(storeResult);
            _mapperMock.Setup(mapper => mapper.Map<ProjectModel>(projectDocumentStored)).Returns(projectModelStored);

            var sut = _fixture.Create<ProjectCreationService>();
            sut.Create(projectModel);

            _serviceResultCreatorMock.Verify(creator => creator.Create(projectModelStored, It.IsAny<bool>()), Times.Once());
        }
    }
}