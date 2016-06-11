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
            if (String.IsNullOrWhiteSpace(ServiceName))
                throw new Exception(nameof(ServiceName));

            if (String.IsNullOrWhiteSpace(ServiceDisplayName))
                throw new Exception(nameof(ServiceDisplayName));

            if (String.IsNullOrWhiteSpace(ServiceDescription))
                throw new Exception(nameof(ServiceDescription));
        }
    }
}