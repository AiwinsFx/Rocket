using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Rocket.IdentityServer.Clients
{
    public interface IClientRepository : IBasicRepository<Client, Guid>
    {
        Task<Client> FindByClientIdAsync(
            [NotNull] string clientId,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );

        Task<List<Client>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<string>> GetAllDistinctAllowedCorsOriginsAsync(CancellationToken cancellationToken = default);

        Task<bool> CheckClientIdExistAsync(
            string clientId,
            Guid? expectedId = null,
            CancellationToken cancellationToken = default
        );
    }
}