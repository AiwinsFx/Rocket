using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.IdentityServer.Devices {
    public class DeviceFlowCodesRepository : EfCoreRepository<IIdentityServerDbContext, DeviceFlowCodes, Guid>,
        IDeviceFlowCodesRepository {
            public DeviceFlowCodesRepository (IDbContextProvider<IIdentityServerDbContext> dbContextProvider) : base (dbContextProvider) {

            }

            public virtual async Task<DeviceFlowCodes> FindByUserCodeAsync (
                string userCode,
                CancellationToken cancellationToken = default) {
                return await DbSet
                    .FirstOrDefaultAsync (d => d.UserCode == userCode, GetCancellationToken (cancellationToken));
            }

            public virtual async Task<DeviceFlowCodes> FindByDeviceCodeAsync (
                string deviceCode,
                CancellationToken cancellationToken = default) {
                return await DbSet
                    .FirstOrDefaultAsync (d => d.DeviceCode == deviceCode, GetCancellationToken (cancellationToken));
            }

            public virtual async Task<List<DeviceFlowCodes>> GetListByExpirationAsync (DateTime maxExpirationDate, int maxResultCount,
                CancellationToken cancellationToken = default) {
                return await DbSet
                    .Where (x => x.Expiration != null && x.Expiration < maxExpirationDate)
                    .OrderBy (x => x.ClientId)
                    .Take (maxResultCount)
                    .ToListAsync (GetCancellationToken (cancellationToken));
            }
        }
}