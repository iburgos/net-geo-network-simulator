namespace NetIGeo.DataAccess.Common
{
    public class Result<T>
    {
        public T Contents { get; set; }
        public bool Success { get; set; }
    }
}