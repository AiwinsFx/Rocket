using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.FeatureManagement.EntityFrameworkCore {
    public class EfCoreFeatureValueRepository : EfCoreRepository<IFeatureManagementDbContext, FeatureValue, Guid>, IFeatureValueRepository {
        public EfCoreFeatureValueRepository (IDbContextProvider<IFeatureManagementDbContext> dbContextProvider) : base (dbContextProvider) { }

        public virtual async Task<FeatureValue> FindAsync (string name, string providerName, string providerKey) {
            return await DbSet
                .FirstOrDefaultAsync (
                    s => s.Name == name && s.ProviderName == providerName && s.ProviderKey == providerKey
                );
        }

        public virtual async Task<List<FeatureValue>> GetListAsync (string providerName, string providerKey) {
            return await DbSet
                .Where (
                    s => s.ProviderName == providerName && s.ProviderKey == providerKey
                ).ToListAsync ();
        }
    }
}