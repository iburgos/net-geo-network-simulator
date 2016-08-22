using System;

using log4net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using Raven.Client;

namespace NetIGeo.DataAccess.Test.RavenDb
{
    [TestClass]
    public class DocumentRetrieverTests
    {
        private IDocument _document;
        private Mock<IDocumentSession> _documentSessionMock;
        private Mock<IDocumentStore> _documentStoreMock;
        private IFixture _fixture;
        private Mock<ILog> _logMock;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _documentStoreMock = _fixture.Freeze<Mock<IDocumentStore>>();
            _documentSessionMock = _fixture.Freeze<Mock<IDocumentSession>>();
            _logMock = _fixture.Freeze<Mock<ILog>>();

            _document = _fixture.Create<IDocument>();
        }

        [TestMethod]
        public void GetAll_Always_CallsQueryOnDocumentSession()
        {
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);

            var sut = _fixture.Create<DocumentRetriever>();
            sut.GetAll<GenericParameterHelper>();

            _documentSessionMock.Verify(d => d.Query<GenericParameterHelper>(), Times.Once());
        }

        [TestMethod]
        public void GetAll_QueringThrowsException_LogsTheException()
        {
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);
            _documentSessionMock.Setup(d => d.Query<GenericParameterHelper>()).Throws(new Exception());

            var sut = _fixture.Create<DocumentRetriever>();
            sut.GetAll<GenericParameterHelper>();

            _logMock.Verify(l => l.ErrorFormat(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void Get_Always_CallsLoadOnDocumentSession()
        {
            var id = _fixture.Create<int>();
            var documentType = _fixture.Create<string>();
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);

            var sut = _fixture.Create<DocumentRetriever>();
            sut.Get<GenericParameterHelper>(id, documentType);

            _documentSessionMock.Verify(d => d.Load<GenericParameterHelper>($"{documentType}/{id}"), Times.Once());
        }

        [TestMethod]
        public void Get_LoadingThrowsException_LogsTheException()
        {
            var id = _fixture.Create<int>();
            var documentType = _fixture.Create<string>();
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);
            _documentSessionMock.Setup(d => d.Load<GenericParameterHelper>($"{documentType}/{id}"))
                                .Throws(new Exception());

            var sut = _fixture.Create<DocumentRetriever>();
            sut.Get<GenericParameterHelper>(id, documentType);

            _logMock.Verify(l => l.ErrorFormat(It.IsAny<string>()), Times.Once());
        }
    }
}