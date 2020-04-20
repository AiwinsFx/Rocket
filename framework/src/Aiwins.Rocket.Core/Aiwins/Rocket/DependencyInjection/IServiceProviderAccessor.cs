using System;

namespace Aiwins.Rocket.DependencyInjection {
    public interface IServiceProviderAccessor {
        IServiceProvider ServiceProvider { get; }
    }
}