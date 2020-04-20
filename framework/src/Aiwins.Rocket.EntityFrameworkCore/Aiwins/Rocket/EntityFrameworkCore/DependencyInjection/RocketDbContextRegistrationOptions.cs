using System;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    public class RocketDbContextRegistrationOptions : RocketCommonDbContextRegistrationOptions, IRocketDbContextRegistrationOptionsBuilder {
        public Dictionary<Type, object> RocketEntityOptions { get; }

        public RocketDbContextRegistrationOptions (Type originalDbContextType, IServiceCollection services) : base (originalDbContextType, services) {
            RocketEntityOptions = new Dictionary<Type, object> ();
        }

        public void Entity<TEntity> (Action<RocketEntityOptions<TEntity>> optionsAction) where TEntity : IEntity {
            Services.Configure<RocketEntityOptions> (options => {
                options.Entity (optionsAction);
            });
        }
    }
}