using System;
using System.Collections.Generic;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public class RocketDbContextOptions {
        internal List<Action<RocketDbContextConfigurationContext>> DefaultPreConfigureActions { get; set; }

        internal Action<RocketDbContextConfigurationContext> DefaultConfigureAction { get; set; }

        internal Dictionary<Type, List<object>> PreConfigureActions { get; set; }

        internal Dictionary<Type, object> ConfigureActions { get; set; }

        public RocketDbContextOptions () {
            DefaultPreConfigureActions = new List<Action<RocketDbContextConfigurationContext>> ();
            PreConfigureActions = new Dictionary<Type, List<object>> ();
            ConfigureActions = new Dictionary<Type, object> ();
        }

        public void PreConfigure ([NotNull] Action<RocketDbContextConfigurationContext> action) {
            Check.NotNull (action, nameof (action));

            DefaultPreConfigureActions.Add (action);
        }

        public void Configure ([NotNull] Action<RocketDbContextConfigurationContext> action) {
            Check.NotNull (action, nameof (action));

            DefaultConfigureAction = action;
        }

        public void PreConfigure<TDbContext> ([NotNull] Action<RocketDbContextConfigurationContext<TDbContext>> action)
        where TDbContext : RocketDbContext<TDbContext> {
            Check.NotNull (action, nameof (action));

            var actions = PreConfigureActions.GetOrDefault (typeof (TDbContext));
            if (actions == null) {
                PreConfigureActions[typeof (TDbContext)] = actions = new List<object> ();
            }

            actions.Add (action);
        }

        public void Configure<TDbContext> ([NotNull] Action<RocketDbContextConfigurationContext<TDbContext>> action)
        where TDbContext : RocketDbContext<TDbContext> {
            Check.NotNull (action, nameof (action));

            ConfigureActions[typeof (TDbContext)] = action;
        }
    }
}