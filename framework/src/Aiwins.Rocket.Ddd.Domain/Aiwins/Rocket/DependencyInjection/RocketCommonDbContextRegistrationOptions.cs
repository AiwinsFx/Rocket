using System;
using System.Collections.Generic;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.DependencyInjection {
    public abstract class RocketCommonDbContextRegistrationOptions : IRocketCommonDbContextRegistrationOptionsBuilder {
        public Type OriginalDbContextType { get; }

        public IServiceCollection Services { get; }

        public List<Type> ReplacedDbContextTypes { get; }

        public Type DefaultRepositoryDbContextType { get; protected set; }

        public Type DefaultRepositoryImplementationType { get; private set; }

        public Type DefaultRepositoryImplementationTypeWithoutKey { get; private set; }

        public bool RegisterDefaultRepositories { get; private set; }

        public bool IncludeAllEntitiesForDefaultRepositories { get; private set; }

        public Dictionary<Type, Type> CustomRepositories { get; }

        public bool SpecifiedDefaultRepositoryTypes => DefaultRepositoryImplementationType != null && DefaultRepositoryImplementationTypeWithoutKey != null;

        protected RocketCommonDbContextRegistrationOptions (Type originalDbContextType, IServiceCollection services) {
            OriginalDbContextType = originalDbContextType;
            Services = services;
            DefaultRepositoryDbContextType = originalDbContextType;
            CustomRepositories = new Dictionary<Type, Type> ();
            ReplacedDbContextTypes = new List<Type> ();
        }

        public IRocketCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext> () {
            return ReplaceDbContext (typeof (TOtherDbContext));
        }

        public IRocketCommonDbContextRegistrationOptionsBuilder ReplaceDbContext (Type otherDbContextType) {
            if (!otherDbContextType.IsAssignableFrom (OriginalDbContextType)) {
                throw new RocketException ($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {otherDbContextType.AssemblyQualifiedName}!");
            }

            ReplacedDbContextTypes.Add (otherDbContextType);

            return this;
        }

        public IRocketCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories (bool includeAllEntities = false) {
            RegisterDefaultRepositories = true;
            IncludeAllEntitiesForDefaultRepositories = includeAllEntities;

            return this;
        }

        public IRocketCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories (Type defaultRepositoryDbContextType, bool includeAllEntities = false) {
            if (!defaultRepositoryDbContextType.IsAssignableFrom (OriginalDbContextType)) {
                throw new RocketException ($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {defaultRepositoryDbContextType.AssemblyQualifiedName}!");
            }

            DefaultRepositoryDbContextType = defaultRepositoryDbContextType;

            return AddDefaultRepositories (includeAllEntities);
        }

        public IRocketCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories<TDefaultRepositoryDbContext> (bool includeAllEntities = false) {
            return AddDefaultRepositories (typeof (TDefaultRepositoryDbContext), includeAllEntities);
        }

        public IRocketCommonDbContextRegistrationOptionsBuilder AddRepository<TEntity, TRepository> () {
            AddCustomRepository (typeof (TEntity), typeof (TRepository));

            return this;
        }

        public IRocketCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClasses (
            Type repositoryImplementationType,
            Type repositoryImplementationTypeWithoutKey
        ) {
            Check.NotNull (repositoryImplementationType, nameof (repositoryImplementationType));
            Check.NotNull (repositoryImplementationTypeWithoutKey, nameof (repositoryImplementationTypeWithoutKey));

            DefaultRepositoryImplementationType = repositoryImplementationType;
            DefaultRepositoryImplementationTypeWithoutKey = repositoryImplementationTypeWithoutKey;

            return this;
        }

        private void AddCustomRepository (Type entityType, Type repositoryType) {
            if (!typeof (IEntity).IsAssignableFrom (entityType)) {
                throw new RocketException ($"Given entityType is not an entity: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEntity<>).AssemblyQualifiedName}.");
            }

            if (!typeof (IRepository).IsAssignableFrom (repositoryType)) {
                throw new RocketException ($"Given repositoryType is not a repository: {entityType.AssemblyQualifiedName}. It must implement {typeof(IBasicRepository<>).AssemblyQualifiedName}.");
            }

            CustomRepositories[entityType] = repositoryType;
        }

        public bool ShouldRegisterDefaultRepositoryFor (Type entityType) {
            if (!RegisterDefaultRepositories) {
                return false;
            }

            if (CustomRepositories.ContainsKey (entityType)) {
                return false;
            }

            if (!IncludeAllEntitiesForDefaultRepositories && !typeof (IAggregateRoot).IsAssignableFrom (entityType)) {
                return false;
            }

            return true;
        }
    }
}