using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.BackgroundWorkers;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.IdentityServer.Tokens
{
    public class TokenCleanupBackgroundWorker : AsyncPeriodicBackgroundWorkerBase
    {
        protected TokenCleanupOptions Options { get; }

        public TokenCleanupBackgroundWorker(
            RocketTimer timer, 
            IServiceScopeFactory serviceScopeFactory,
            IOptions<TokenCleanupOptions> options)
            : base(
                timer, 
                serviceScopeFactory)
        {
            Options = options.Value;
            timer.Period = Options.CleanupPeriod;
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            await workerContext
                .ServiceProvider
                .GetRequiredService<TokenCleanupService>()
                .CleanAsync()
                ;
        }
    }
}
