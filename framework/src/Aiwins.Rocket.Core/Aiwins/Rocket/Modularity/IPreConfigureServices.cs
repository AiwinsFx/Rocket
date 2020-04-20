using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Modularity {
    public interface IPreConfigureServices {
        void PreConfigureServices (ServiceConfigurationContext context);
    }
}