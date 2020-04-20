using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.BackgroundJobs {
    public abstract class AsyncBackgroundJob<TArgs> : IAsyncBackgroundJob<TArgs> {
        //TODO: 考虑添加工作单元, 本地化..

        public ILogger<AsyncBackgroundJob<TArgs>> Logger { get; set; }

        protected AsyncBackgroundJob () {
            Logger = NullLogger<AsyncBackgroundJob<TArgs>>.Instance;
        }

        public abstract Task ExecuteAsync (TArgs args);
    }
}