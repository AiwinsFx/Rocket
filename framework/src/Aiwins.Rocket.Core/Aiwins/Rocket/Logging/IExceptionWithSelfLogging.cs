using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.Logging {
    public interface IExceptionWithSelfLogging {
        void Log (ILogger logger);
    }
}