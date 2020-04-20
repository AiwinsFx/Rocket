using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Modularity {
    public interface IRocketModule {
        void ConfigureServices (ServiceConfigurationContext context);
    }
}