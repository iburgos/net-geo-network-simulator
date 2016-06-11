using System;
using Microsoft.Practices.Unity;
using Topshelf;
using UnityLog4NetExtension.Log4Net;

namespace NetIGeo.Service.Bootstrappers
{
    public class Bootstrapper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<Log4NetExtension>();
            Container.RegisterType<ServiceStarter>();
        }
    }
}