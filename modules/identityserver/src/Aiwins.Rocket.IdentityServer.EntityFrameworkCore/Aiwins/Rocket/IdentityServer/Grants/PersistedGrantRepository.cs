using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.IdentityServer.Grants {
    public class PersistentGrantRepository : EfCoreRepository<IIdentityServerDbContext, PersistedGrant, Guid>, IPersistentGrantRepository {
        public PersistentGrantRepository (IDbContextProvider<IIdentityServerDbContext> dbContextProvider) : base (dbContextProvider) {

        }

        public virtual async Task<PersistedGrant> FindByKeyAsync (
            string key,
            CancellationToken cancellationToken = default) {
            return await DbSet
                .FirstOrDefaultAsync (x => x.Key == key, GetCancellationToken (cancellationToken));
        }

        public virtual async Task<List<PersistedGrant>> GetListBySubjectIdAsync (
            string subjectId,
            CancellationToken cancellationToken = default) {
            return await DbSet
                .Where (x => x.SubjectId == subjectId)
                .ToListAsync (GetCancellationToken (cancellationToken));
        }

        public virtual async Task<List<PersistedGrant>> GetListByExpirationAsync (
            DateTime maxExpirationDate,
            int maxResultCount,
            CancellationToken cancellationToken = default) {
            return await DbSet
                .Where (x => x.Expiration != null && x.Expiration < maxExpirationDate)
                .OrderBy (x => x.ClientId)
                .Take (maxResultCount)
                .ToListAsync (GetCancellationToken (cancellationToken));
        }

        public virtual async Task DeleteAsync (
            string subjectId,
            string clientId,
            CancellationToken cancellationToken = default) {
            await DeleteAsync (
                x => x.SubjectId == subjectId && x.ClientId == clientId,
                cancellationToken : GetCancellationToken (cancellationToken)
            );
        }

        public virtual async Task DeleteAsync (
            string subjectId,
            string clientId,
            string type,
            CancellationToken cancellationToken = default) {
            await DeleteAsync (
                x => x.SubjectId == subjectId && x.ClientId == clientId && x.Type == type,
                cancellationToken : GetCancellationToken (cancellationToken)
            );
        }
    }
}