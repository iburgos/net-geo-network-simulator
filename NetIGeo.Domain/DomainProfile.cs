using AutoMapper;

namespace NetIGeo.Domain
{
    public class DomainProfile : Profile
    {
        private readonly IMapperConfigurationExpression _mapperConfiguration;

        public DomainProfile(IMapperConfigurationExpression mapperConfiguration)
        {
            _mapperConfiguration = mapperConfiguration;
        }

        protected override void Configure()
        {
            CreateMissingTypeMaps = true;
        }
    }
}