using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using MongoDB.Driver;

namespace Aiwins.Rocket.MongoDB {
    public abstract class RocketMongoDbContext : IRocketMongoDbContext, ITransientDependency {
        public IMongoModelSource ModelSource { get; set; }

        public IMongoDatabase Database { get; private set; }

        protected internal virtual void CreateModel (IMongoModelBuilder modelBuilder) {

        }

        public virtual void InitializeDatabase (IMongoDatabase database) {
            Database = database;
        }

        public virtual IMongoCollection<T> Collection<T> () {
            return Database.GetCollection<T> (GetCollectionName<T> ());
        }

        protected virtual string GetCollectionName<T> () {
            return GetEntityModel<T> ().CollectionName;
        }

        protected virtual IMongoEntityModel GetEntityModel<TEntity> () {
            var model = ModelSource.GetModel (this).Entities.GetOrDefault (typeof (TEntity));

            if (model == null) {
                throw new RocketException ("Could not find a model for given entity type: " + typeof (TEntity).AssemblyQualifiedName);
            }

            return model;
        }
    }
}