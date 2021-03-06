﻿using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.TenantManagement.MongoDB {
    [ConnectionStringName (RocketTenantManagementDbProperties.ConnectionStringName)]
    public class TenantManagementMongoDbContext : RocketMongoDbContext, ITenantManagementMongoDbContext {
        public IMongoCollection<Tenant> Tenants => Collection<Tenant> ();

        protected override void CreateModel (IMongoModelBuilder modelBuilder) {
            base.CreateModel (modelBuilder);

            modelBuilder.ConfigureTenantManagement ();
        }
    }
}