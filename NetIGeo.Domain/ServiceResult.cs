namespace NetIGeo.Domain
{
    public class ServiceResult<T>
    {
        public T Contents { get; set; }
        public bool Success { get; set; }
    }
}