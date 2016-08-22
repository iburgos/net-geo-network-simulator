using System.Collections.Generic;

using AutoMapper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace NetIGeo.DataAccess.Test.RavenDb
{
    [TestClass]
    public class ProjectDocumentRepositoryTests
    {
        private Mock<IDocumentRetriever> _documentRetrieverMock;
        private Mock<IDocumentStorer> _documentStorerMock;
        private IFixture _fixture;
        private Mock<IMapper> _mapperMock;
        private Mock<IResultCreator> _resultCreatorMock;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _documentStorerMock = _fixture.Freeze<Mock<IDocumentStorer>>();
            _mapperMock = _fixture.Freeze<Mock<IMapper>>();
            _documentRetrieverMock = _fixture.Freeze<Mock<IDocumentRetriever>>();
            _resultCreatorMock = _fixture.Freeze<Mock<IResultCreator>>();
        }

        [TestMethod]
        public void Create_Always_CallsDocumentStorer()
        {
            var project = _fixture.Create<ProjectDocument>();

            var sut = _fixture.Create<ProjectDocumentRepository>();
            sut.Create(project);

            _documentStorerMock.Verify(storer => storer.Store(project), Times.Once());
        }

        [TestMethod]
        public void Create_Always_MapsStorerResultsToProjectDocument()
        {
            var project = _fixture.Create<ProjectDocument>();
            Result<IDocument> storerResult = _fixture.Create<Result<IDocument>>();
            _documentStorerMock.Setup(storer => storer.Store(project)).Returns(storerResult);

            var sut = _fixture.Create<ProjectDocumentRepository>();
            sut.Create(project);

            _mapperMock.Verify(mapper => mapper.Map<ProjectDocument>(storerResult.Contents), Times.Once());
        }

        [TestMethod]
        public void Create_Always_CreatesResult()
        {
            var project = _fixture.Create<ProjectDocument>();
            Result<IDocument> storerResult = _fixture.Create<Result<IDocument>>();
            _documentStorerMock.Setup(storer => storer.Store(project)).Returns(storerResult);
            var mappedResult = _fixture.Create<ProjectDocument>();
            _mapperMock.Setup(mapper => mapper.Map<ProjectDocument>(storerResult.Contents)).Returns(mappedResult);

            var sut = _fixture.Create<ProjectDocumentRepository>();
            sut.Create(project);

            _resultCreatorMock.Verify(creator => creator.Create(mappedResult, storerResult.Success), Times.Once());
        }

        [TestMethod]
        public void GetAll_Always_CallsDocumentRetriever()
        {
            var sut = _fixture.Create<ProjectDocumentRepository>();
            sut.GetAll();

            _documentRetrieverMock.Verify(retriever => retriever.GetAll<ProjectDocument>(), Times.Once());
        }

        [TestMethod]
        public void GetAll_Always_CreatesResult()
        {
            var retrieverResult = _fixture.Create<Result<IEnumerable<ProjectDocument>>>();
            _documentRetrieverMock.Setup(retriever => retriever.GetAll<ProjectDocument>()).Returns(retrieverResult);

            var sut = _fixture.Create<ProjectDocumentRepository>();
            sut.GetAll();

            _resultCreatorMock.Verify(creator => creator.Create(retrieverResult.Contents, retrieverResult.Success),
                Times.Once());
        }

        [TestMethod]
        public void Get_Always_CallsDocumentRetriever()
        {
            var id = _fixture.Create<int>();

            var sut = _fixture.Create<ProjectDocumentRepository>();
            sut.Get(id);

            _documentRetrieverMock.Verify(retriever => retriever.Get<ProjectDocument>(id, RavenDbConstants.PROJECT_DOCUMENT_TYPE), Times.Once());
        }

        [TestMethod]
        public void Get_Always_CreatesResult()
        {
            var id = _fixture.Create<int>();
            var retrieverResult = _fixture.Create<Result<ProjectDocument>>();
            _documentRetrieverMock.Setup(retriever => retriever.Get<ProjectDocument>(id, RavenDbConstants.PROJECT_DOCUMENT_TYPE)).Returns(retrieverResult);

            var sut = _fixture.Create<ProjectDocumentRepository>();
            sut.Get(id);

            _resultCreatorMock.Verify(creator => creator.Create(retrieverResult.Contents, retrieverResult.Success),
                Times.Once());
        }
    }
}