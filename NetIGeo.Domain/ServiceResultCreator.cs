namespace NetIGeo.Domain
{
    public interface IServiceResultCreator
    {
        ServiceResult<T> Create<T>(T contents, bool successful);
    }

    public class ServiceResultCreator : IServiceResultCreator
    {
        public ServiceResult<T> Create<T>(T contents, bool successful)
            => new ServiceResult<T> {Contents = contents, Success = successful};
    }
}