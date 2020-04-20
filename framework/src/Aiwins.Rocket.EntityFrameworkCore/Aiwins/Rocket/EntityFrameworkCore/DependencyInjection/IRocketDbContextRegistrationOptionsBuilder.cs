using System;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities;
using JetBrains.Annotations;

namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    public interface IRocketDbContextRegistrationOptionsBuilder : IRocketCommonDbContextRegistrationOptionsBuilder {
        void Entity<TEntity> ([NotNull] Action<RocketEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntity;
    }
}