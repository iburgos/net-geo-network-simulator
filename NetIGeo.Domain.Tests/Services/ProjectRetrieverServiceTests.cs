using System.Collections.Generic;

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
    public class ProjectRetrieverServiceTests
    {
        private IFixture _fixture;
        private Mock<IMapper> _mapperMock;
        private Mock<IProjectDocumentRepository> _projectDocumentRepositoryMock;
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
        public void Get_WithoutInputParameters_CallsProjectDocumentRepository()
        {
            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get();

            _projectDocumentRepositoryMock.Verify(retriever => retriever.GetAll(), Times.Once());
        }

        [TestMethod]
        public void Get_WithoutInputParameters_CallsServiceResultCreator()
        {
            var documentResult = _fixture.Create<Result<IEnumerable<ProjectDocument>>>();
            _projectDocumentRepositoryMock.Setup(retriever => retriever.GetAll()).Returns(documentResult);
            var modelResult = _fixture.CreateMany<ProjectModel>();
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProjectModel>>(documentResult.Contents))
                       .Returns(modelResult);

            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get();

            _serviceResultCreatorMock.Verify(creator => creator.Create(modelResult, documentResult.Success),
                Times.Once());
        }

        [TestMethod]
        public void Get_WithoutInputParameters_CallsMapper()
        {
            var documentResult = _fixture.Create<Result<IEnumerable<ProjectDocument>>>();
            _projectDocumentRepositoryMock.Setup(retriever => retriever.GetAll()).Returns(documentResult);

            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get();

            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ProjectModel>>(documentResult.Contents), Times.Once());
        }

        [TestMethod]
        public void Get_WithInputParameter_CallsProjectDocumentRepository()
        {
            var id = _fixture.Create<int>();

            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get(id);

            _projectDocumentRepositoryMock.Verify(retriever => retriever.Get(id), Times.Once());
        }

        [TestMethod]
        public void Get_WithInputParameters_CallsServiceResultCreator()
        {
            var id = _fixture.Create<int>();
            var documentResult = _fixture.Create<Result<ProjectDocument>>();
            _projectDocumentRepositoryMock.Setup(retriever => retriever.Get(id)).Returns(documentResult);
            var modelResult = _fixture.Create<ProjectModel>();
            _mapperMock.Setup(mapper => mapper.Map<ProjectModel>(documentResult.Contents))
                       .Returns(modelResult);

            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get(id);

            _serviceResultCreatorMock.Verify(creator => creator.Create(modelResult, documentResult.Success),
                Times.Once());
        }

        [TestMethod]
        public void Get_WithInputParameters_CallsMapper()
        {
            var id = _fixture.Create<int>();
            var documentResult = _fixture.Create<Result<ProjectDocument>>();
            _projectDocumentRepositoryMock.Setup(retriever => retriever.Get(id)).Returns(documentResult);

            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get(id);

            _mapperMock.Verify(mapper => mapper.Map<ProjectModel>(documentResult.Contents), Times.Once());
        }
    }
}