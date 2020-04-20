using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Uow.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aiwins.Rocket.EntityFrameworkCore {
    [DependsOn (typeof (RocketDddDomainModule))]
    public class RocketEntityFrameworkCoreModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketDbContextOptions> (options => {
                options.PreConfigure (rocketDbContextConfigurationContext => {
                    rocketDbContextConfigurationContext.DbContextOptions
                        .ConfigureWarnings (warnings => {
                            warnings.Ignore (CoreEventId.LazyLoadOnDisposedContextWarning);
                        });
                });
            });

            context.Services.TryAddTransient (typeof (IDbContextProvider<>), typeof (UnitOfWorkDbContextProvider<>));
        }
    }
}