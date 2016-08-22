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
    public class DocumentStorerTests
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
        public void Store_DocumentStoreThrowsException_LogsTheException()
        {
            _documentStoreMock.Setup(ds => ds.OpenSession()).Throws(new Exception());
            var sut = _fixture.Create<DocumentStorer>();

            sut.Store(_document);
            _logMock.Verify(l => l.ErrorFormat(It.IsAny<string>(), It.IsAny<Type>(), It.IsAny<Exception>()),
                Times.Once());
        }

        [TestMethod]
        public void Store_Always_CallsStoreOnDocumentSession()
        {
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);
            var sut = _fixture.Create<DocumentStorer>();

            sut.Store(_document);
            _documentSessionMock.Verify(d => d.Store(_document), Times.Once());
        }

        [TestMethod]
        public void Store_StoringIsSuccesful_CallsSaveChanges()
        {
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);
            var sut = _fixture.Create<DocumentStorer>();

            sut.Store(_document);
            _documentSessionMock.Verify(d => d.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Store_StoringThrowsException_DoesNotCallSaveChanges()
        {
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);
            _documentSessionMock.Setup(d => d.Store(_document)).Throws(new Exception());
            var sut = _fixture.Create<DocumentStorer>();

            sut.Store(_document);
            _documentSessionMock.Verify(d => d.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void Store_StoringThrowsException_LogsTheException()
        {
            _documentStoreMock.Setup(ds => ds.OpenSession()).Returns(_documentSessionMock.Object);
            _documentSessionMock.Setup(d => d.Store(_document)).Throws(new Exception());
            var sut = _fixture.Create<DocumentStorer>();

            sut.Store(_document);
            _logMock.Verify(l => l.ErrorFormat(It.IsAny<string>(), It.IsAny<Type>(), It.IsAny<Exception>()),
                Times.Once());
        }
    }
}