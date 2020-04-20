using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Modularity {
    public interface IPostConfigureServices {
        void PostConfigureServices (ServiceConfigurationContext context);
    }
}