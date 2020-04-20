using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.BackgroundJobs {
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs> {
        //TODO: 考虑添加工作单元, 本地化...

        public ILogger<BackgroundJob<TArgs>> Logger { get; set; }

        protected BackgroundJob () {
            Logger = NullLogger<BackgroundJob<TArgs>>.Instance;
        }

        public abstract void Execute (TArgs args);
    }
}