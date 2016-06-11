using log4net;

namespace NetIGeo.Service
{
    public class ServiceStarter
    {
        private readonly ILog _log;

        public ServiceStarter(ILog log)
        {
            _log = log;
        }

        public void Start()
        {
            _log.Debug("Esto esta funcionando");
        }
    }
}