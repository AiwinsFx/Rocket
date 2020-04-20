using Aiwins.Rocket.Modularity;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Hangfire {
    public class RocketHangfireModule : RocketModule {
        private BackgroundJobServer _backgroundJobServer;

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddHangfire (configuration => {
                context.Services.ExecutePreConfiguredActions (configuration);
            });
        }

        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            var options = context.ServiceProvider.GetRequiredService<IOptions<RocketHangfireOptions>> ().Value;
            _backgroundJobServer = options.BackgroundJobServerFactory.Invoke (context.ServiceProvider);
        }

        public override void OnApplicationShutdown (ApplicationShutdownContext context) {
            //TODO: Rocket may provide two methods for application shutdown: OnPreApplicationShutdown & OnApplicationShutdown
            _backgroundJobServer.SendStop ();
            _backgroundJobServer.Dispose ();
        }
    }
}