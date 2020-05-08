using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.IdentityServer.IdentityResources {
    public class IdentityResourceRepository : EfCoreRepository<IIdentityServerDbContext, IdentityResource, Guid>, IIdentityResourceRepository {
        public IdentityResourceRepository (IDbContextProvider<IIdentityServerDbContext> dbContextProvider) : base (dbContextProvider) {

        }

        public virtual async Task<List<IdentityResource>> GetListByScopesAsync (
            string[] scopeNames,
            bool includeDetails = false,
            CancellationToken cancellationToken = default) {
            var query = from identityResource in DbSet.IncludeDetails (includeDetails)
            where scopeNames.Contains (identityResource.Name)
            select identityResource;

            return await query.ToListAsync (GetCancellationToken (cancellationToken));
        }

        public override IQueryable<IdentityResource> WithDetails () {
            return GetQueryable ().IncludeDetails ();
        }

        public virtual async Task<List<IdentityResource>> GetListAsync (string sorting, int skipCount, int maxResultCount,
            bool includeDetails = false, CancellationToken cancellationToken = default) {
            return await DbSet
                .IncludeDetails (includeDetails)
                .OrderBy (sorting ?? "name desc")
                .PageBy (skipCount, maxResultCount)
                .ToListAsync (GetCancellationToken (cancellationToken));
        }

        public virtual async Task<IdentityResource> FindByNameAsync (
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default) {
            return await DbSet
                .IncludeDetails (includeDetails)
                .Where (x => x.Name == name)
                .FirstOrDefaultAsync (GetCancellationToken (cancellationToken));
        }

        public virtual async Task<bool> CheckNameExistAsync (string name, Guid? expectedId = null, CancellationToken cancellationToken = default) {
            return await DbSet.AnyAsync (ir => ir.Id != expectedId && ir.Name == name, cancellationToken : cancellationToken);
        }
    }
}