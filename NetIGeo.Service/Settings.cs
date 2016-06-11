using System;
using NetIGeo.Common;

namespace NetIGeo.Service
{
    public class Settings : SettingsLoader
    {
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceDescription { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}