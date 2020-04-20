using System;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    public abstract class RocketTestBaseWithServiceProvider {
        protected abstract IServiceProvider ServiceProvider { get; }

        protected virtual T GetService<T> () {
            return ServiceProvider.GetService<T> ();
        }

        protected virtual T GetRequiredService<T> () {
            return ServiceProvider.GetRequiredService<T> ();
        }
    }
}