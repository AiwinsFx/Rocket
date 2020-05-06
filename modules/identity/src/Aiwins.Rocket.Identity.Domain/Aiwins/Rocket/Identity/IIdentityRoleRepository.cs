using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Rocket.Identity {
    public interface IIdentityRoleRepository : IBasicRepository<IdentityRole, Guid> {
        Task<IdentityRole> FindByNormalizedNameAsync (
            string normalizedRoleName,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityRole>> GetListAsync (
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityRole>> GetDefaultOnesAsync (
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );
    }
}