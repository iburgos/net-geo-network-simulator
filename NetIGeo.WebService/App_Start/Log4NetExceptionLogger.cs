using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace NetIGeo.WebService
{
    public class Log4NetExceptionLogger : ExceptionLogger
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(Log4NetExceptionLogger));

        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            _logger.Error("An unhandled exception occurred.", context.Exception);
            await base.LogAsync(context, cancellationToken);
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Error("An unhandled exception occurred.", context.Exception);
            base.Log(context);
        }
    }
}