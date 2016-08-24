using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetIGeo.DataAccess.RavenDb;
using NetIGeo.Domain.Services;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace NetIGeo.Domain.Tests.Services
{
    [TestClass]
    public class ProjectDeleteServiceTests
    {
        private IFixture _fixture;

        private Mock<IProjectDocumentRepository> _projectDocumentRepositoryMock;


        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _projectDocumentRepositoryMock = _fixture.Freeze<Mock<IProjectDocumentRepository>>();
        }

        [TestMethod]
        public void Delete_Always_CallsProjectDocumentRepositoryDelete()
        {
            var id = _fixture.Create<int>();

            var sut = _fixture.Create<ProjectDeleteService>();
            sut.Delete(id);

            _projectDocumentRepositoryMock.Verify(repository => repository.Delete(id), Times.Once());
        }

        [TestMethod]
        public void Delete_IfProjectDocumentRepositorySucceeds_ReturnsTrue()
        {
            var id = _fixture.Create<int>();
            _projectDocumentRepositoryMock.Setup(repository => repository.Delete(id)).Returns(true);

            var sut = _fixture.Create<ProjectDeleteService>();
            bool result = sut.Delete(id);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void Delete_IfProjectDocumentRepositoryDoesNotSucceed_ReturnsFalse()
        {
            var id = _fixture.Create<int>();
            _projectDocumentRepositoryMock.Setup(repository => repository.Delete(id)).Returns(false);

            var sut = _fixture.Create<ProjectDeleteService>();
            bool result = sut.Delete(id);

            result.Should().BeFalse();
        }
    }
}