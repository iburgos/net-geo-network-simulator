using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetIGeo.WebService;
using NetIGeo.WebService.Controllers;

namespace NetIGeo.Domain.Tests.Integration
{
    [TestClass]
    public class BootstrappingTests
    {
        [TestMethod]
        public void Test()
        {
            var container = new UnityContainer();
            var settings = new Settings();
            container.AddExtension(new Bootstrapper(settings));
            container.Resolve<ProjectController>();
        }
    }
}