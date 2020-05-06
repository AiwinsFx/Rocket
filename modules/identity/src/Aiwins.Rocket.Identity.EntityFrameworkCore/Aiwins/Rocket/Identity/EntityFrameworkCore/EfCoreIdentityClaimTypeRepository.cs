using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.Identity.EntityFrameworkCore {
    public class EfCoreIdentityClaimTypeRepository : EfCoreRepository<IIdentityDbContext, IdentityClaimType, Guid>, IIdentityClaimTypeRepository {
        public EfCoreIdentityClaimTypeRepository (IDbContextProvider<IIdentityDbContext> dbContextProvider) : base (dbContextProvider) {

        }

        public virtual async Task<bool> AnyAsync (string name, Guid? ignoredId = null) {
            return await DbSet
                .WhereIf (ignoredId != null, ct => ct.Id != ignoredId)
                .CountAsync (ct => ct.Name == name) > 0;
        }

        public virtual async Task<List<IdentityClaimType>> GetListAsync (string sorting, int maxResultCount, int skipCount, string filter) {
            var identityClaimTypes = await DbSet
                .WhereIf (!filter.IsNullOrWhiteSpace (),
                    u =>
                    u.Name.Contains (filter)
                )
                .OrderBy (sorting ?? "name desc")
                .PageBy (skipCount, maxResultCount)
                .ToListAsync ();

            return identityClaimTypes;
        }
    }
}