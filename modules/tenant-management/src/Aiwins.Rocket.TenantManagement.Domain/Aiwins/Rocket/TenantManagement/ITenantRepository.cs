﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Rocket.TenantManagement {
    public interface ITenantRepository : IBasicRepository<Tenant, Guid> {
        Task<Tenant> FindByNameAsync (
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Tenant FindByName (
            string name,
            bool includeDetails = true
        );

        Tenant FindById (
            Guid id,
            bool includeDetails = true
        );

        Task<List<Tenant>> GetListAsync (
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync (
            string filter = null,
            CancellationToken cancellationToken = default);
    }
}