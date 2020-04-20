using System;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Testing {
    public abstract class RocketIntegratedTest<TStartupModule> : RocketTestBaseWithServiceProvider, IDisposable
    where TStartupModule : IRocketModule {
        protected IRocketApplication Application { get; }

        protected override IServiceProvider ServiceProvider => Application.ServiceProvider;

        protected IServiceProvider RootServiceProvider { get; }

        protected IServiceScope TestServiceScope { get; }

        protected RocketIntegratedTest () {
            var services = CreateServiceCollection ();

            BeforeAddApplication (services);

            var application = services.AddApplication<TStartupModule> (SetRocketApplicationCreationOptions);
            Application = application;

            AfterAddApplication (services);

            RootServiceProvider = CreateServiceProvider (services);
            TestServiceScope = RootServiceProvider.CreateScope ();

            application.Initialize (TestServiceScope.ServiceProvider);
        }

        protected virtual IServiceCollection CreateServiceCollection () {
            return new ServiceCollection ();
        }

        protected virtual void BeforeAddApplication (IServiceCollection services) {

        }

        protected virtual void SetRocketApplicationCreationOptions (RocketApplicationCreationOptions options) {

        }

        protected virtual void AfterAddApplication (IServiceCollection services) {

        }

        protected virtual IServiceProvider CreateServiceProvider (IServiceCollection services) {
            return services.BuildServiceProviderFromFactory ();
        }

        public virtual void Dispose () {
            Application.Shutdown ();
            TestServiceScope.Dispose ();
            Application.Dispose ();
        }
    }
}