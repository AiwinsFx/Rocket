﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.MongoDB;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Aiwins.Rocket.PermissionManagement.MongoDB {
    public class MongoPermissionGrantRepository : MongoDbRepository<IPermissionManagementMongoDbContext, PermissionGrant, Guid>, IPermissionGrantRepository {
        public MongoPermissionGrantRepository (IMongoDbContextProvider<IPermissionManagementMongoDbContext> dbContextProvider) : base (dbContextProvider) {

        }

        public virtual async Task<PermissionGrant> FindAsync (
            string name,
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default) {
            return await GetMongoQueryable ()
                .FirstOrDefaultAsync (s =>
                    s.Name == name &&
                    s.ProviderName == providerName &&
                    s.ProviderKey == providerKey,
                    GetCancellationToken (cancellationToken)
                );
        }

        public virtual async Task<List<PermissionGrant>> GetListAsync (
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default) {
            return await GetMongoQueryable ()
                .Where (s =>
                    s.ProviderName == providerName &&
                    s.ProviderKey == providerKey
                ).ToListAsync (GetCancellationToken (cancellationToken));
        }
    }
}