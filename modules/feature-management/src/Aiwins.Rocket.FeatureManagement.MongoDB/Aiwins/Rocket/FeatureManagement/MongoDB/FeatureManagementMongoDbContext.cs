﻿using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.FeatureManagement.MongoDB {
    [ConnectionStringName (FeatureManagementDbProperties.ConnectionStringName)]
    public class FeatureManagementMongoDbContext : RocketMongoDbContext, IFeatureManagementMongoDbContext {
        public IMongoCollection<FeatureValue> FeatureValues => Collection<FeatureValue> ();

        protected override void CreateModel (IMongoModelBuilder modelBuilder) {
            base.CreateModel (modelBuilder);

            modelBuilder.ConfigureFeatureManagement ();
        }
    }
}