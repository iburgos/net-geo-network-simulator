using log4net.Config;
using Microsoft.Practices.Unity;
using NetIGeo.Service.Bootstrappers;
using Topshelf;
using Topshelf.Unity;

namespace NetIGeo.Service
{
    public class Program
    {
        public static void Main()
        {
            XmlConfigurator.Configure();
            var settings = new Settings();
            var container = new UnityContainer();


            container.AddNewExtension<Bootstrapper>();

            HostFactory.Run(c =>
            {
                c.UseUnityContainer(container);
                c.UseLog4Net();

                c.SetServiceName(settings.ServiceName);
                c.SetDisplayName(settings.ServiceDisplayName);
                c.SetDescription(settings.ServiceDescription);

                c.Service<ServiceStarter>(s =>
                {
                    s.ConstructUsingUnityContainer();
                    s.WhenStarted(pcs => pcs.Start());
                    s.WhenStopped(_ => { });
                });
            });
            System.Console.ReadKey();
        }
    }
}