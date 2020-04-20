using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Linq;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Threading
{
    public class RocketThreadingModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IAsyncQueryableExecuter>(DefaultAsyncQueryableExecuter.Instance);
            context.Services.AddSingleton<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);
            context.Services.AddSingleton(typeof(IAmbientScopeProvider<>), typeof(AmbientDataContextAmbientScopeProvider<>));
        }
    }
}
