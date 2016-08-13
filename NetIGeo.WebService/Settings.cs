using System;
using NetIGeo.Common;

namespace NetIGeo.WebService
{
    public class Settings : SettingsLoader
    {
        public string RavenDbLocation { get; set; }
        public string RavenDbDatabase { get; set; }
        
        protected override void Validate()
        {
            if (String.IsNullOrWhiteSpace(RavenDbLocation))
                throw new Exception(nameof(RavenDbLocation));

            if (String.IsNullOrWhiteSpace(RavenDbDatabase))
                throw new Exception(nameof(RavenDbDatabase));
        }
    }
}