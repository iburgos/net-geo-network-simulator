namespace NetIGeo.DataAccess.Common
{
    public interface IResultCreator
    {
        Result<T> Create<T>(T contents, bool successful);
    }

    public class ResultCreator : IResultCreator
    {
        public Result<T> Create<T>(T contents, bool successful)
        {
            return new Result<T> {Contents = contents, Success = successful};
        }
    }
}