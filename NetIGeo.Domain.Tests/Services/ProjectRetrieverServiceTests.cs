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
        private Mock<IProjectDocumentRetriever> _projectDocumentRetrieverMock;
        private Mock<IServiceResultCreator> _serviceResultCreatorMock;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _projectDocumentRetrieverMock = _fixture.Freeze<Mock<IProjectDocumentRetriever>>();
            _mapperMock = _fixture.Freeze<Mock<IMapper>>();
            _serviceResultCreatorMock = _fixture.Freeze<Mock<IServiceResultCreator>>();
        }

        [TestMethod]
        public void Get_Always_CallsProjectDocumentRetriever()
        {
            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get();

            _projectDocumentRetrieverMock.Verify(retriever => retriever.Get(), Times.Once());
        }

        [TestMethod]
        public void Get_Always_CallsServiceResultCreator()
        {
            var documentResult = _fixture.Create<Result<IEnumerable<ProjectDocument>>>();
            _projectDocumentRetrieverMock.Setup(retriever => retriever.Get()).Returns(documentResult);
            var modelResult = _fixture.CreateMany<ProjectModel>();
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProjectModel>>(documentResult.Contents))
                .Returns(modelResult);

            var sut = _fixture.Create<ProjectRetrieverService>();
            sut.Get();

            _serviceResultCreatorMock.Verify(creator => creator.Create(modelResult, documentResult.Success), Times.Once());
        }
    }
}