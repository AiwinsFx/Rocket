using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    public class RocketDbContextConfigurerAction : IRocketDbContextConfigurer {
        [NotNull]
        public Action<RocketDbContextConfigurationContext> Action { get; }

        public RocketDbContextConfigurerAction ([NotNull] Action<RocketDbContextConfigurationContext> action) {
            Check.NotNull (action, nameof (action));

            Action = action;
        }

        public void Configure (RocketDbContextConfigurationContext context) {
            Action.Invoke (context);
        }
    }

    public class RocketDbContextConfigurerAction<TDbContext> : RocketDbContextConfigurerAction
    where TDbContext : RocketDbContext<TDbContext> {
        public RocketDbContextConfigurerAction ([NotNull] Action<RocketDbContextConfigurationContext> action) : base (action) { }
    }
}