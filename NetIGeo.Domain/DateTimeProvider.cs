using System;

namespace NetIGeo.Domain
{
    public interface IDateTimeProvider
    {
        DateTime NowUTC();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime NowUTC()
        {
            return DateTime.Now.ToUniversalTime();
        }
    }
}