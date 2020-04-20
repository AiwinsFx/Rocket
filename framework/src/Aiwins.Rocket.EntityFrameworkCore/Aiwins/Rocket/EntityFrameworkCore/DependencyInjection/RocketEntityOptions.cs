using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Domain.Entities;
using JetBrains.Annotations;

namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    public class RocketEntityOptions<TEntity>
        where TEntity : IEntity {
            public static RocketEntityOptions<TEntity> Empty { get; } = new RocketEntityOptions<TEntity> ();

            public Func<IQueryable<TEntity>, IQueryable<TEntity>> DefaultWithDetailsFunc { get; set; }
        }

    public class RocketEntityOptions {
        private readonly IDictionary<Type, object> _options;

        public RocketEntityOptions () {
            _options = new Dictionary<Type, object> ();
        }

        public RocketEntityOptions<TEntity> GetOrNull<TEntity> ()
        where TEntity : IEntity {
            return _options.GetOrDefault (typeof (TEntity)) as RocketEntityOptions<TEntity>;
        }

        public void Entity<TEntity> ([NotNull] Action<RocketEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntity {
            Check.NotNull (optionsAction, nameof (optionsAction));

            optionsAction (
                _options.GetOrAdd (
                    typeof (TEntity),
                    () => new RocketEntityOptions<TEntity> ()
                ) as RocketEntityOptions<TEntity>
            );
        }
    }
}