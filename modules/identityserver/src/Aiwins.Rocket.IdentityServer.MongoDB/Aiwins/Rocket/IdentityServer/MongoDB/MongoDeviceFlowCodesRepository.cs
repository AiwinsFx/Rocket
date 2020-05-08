using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.MongoDB;
using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Aiwins.Rocket.IdentityServer.MongoDB {
    public class MongoDeviceFlowCodesRepository:
        MongoDbRepository<IRocketIdentityServerMongoDbContext, DeviceFlowCodes, Guid>, IDeviceFlowCodesRepository {
            public MongoDeviceFlowCodesRepository (
                IMongoDbContextProvider<IRocketIdentityServerMongoDbContext> dbContextProvider) : base (dbContextProvider) {

            }

            public virtual async Task<DeviceFlowCodes> FindByUserCodeAsync (
                string userCode,
                CancellationToken cancellationToken = default) {
                return await GetMongoQueryable ()
                    .FirstOrDefaultAsync (d => d.UserCode == userCode, GetCancellationToken (cancellationToken));
            }

            public virtual async Task<DeviceFlowCodes> FindByDeviceCodeAsync (string deviceCode, CancellationToken cancellationToken = default) {
                return await GetMongoQueryable ()
                    .FirstOrDefaultAsync (d => d.DeviceCode == deviceCode, GetCancellationToken (cancellationToken));
            }

            public virtual async Task<List<DeviceFlowCodes>> GetListByExpirationAsync (
                DateTime maxExpirationDate,
                int maxResultCount,
                CancellationToken cancellationToken = default) {
                return await GetMongoQueryable ()
                    .Where (x => x.Expiration != null && x.Expiration < maxExpirationDate)
                    .OrderBy (x => x.ClientId)
                    .Take (maxResultCount)
                    .ToListAsync (GetCancellationToken (cancellationToken));
            }
        }
}