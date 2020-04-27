using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.ObjectMapping;

namespace Aiwins.Rocket.TenantManagement {

    public class TenantStore : ITenantStore, ITransientDependency {
        protected ITenantRepository TenantRepository { get; }
        protected IObjectMapper<RocketTenantManagementDomainModule> ObjectMapper { get; }
        protected ICurrentTenant CurrentTenant { get; }

        public TenantStore (
            ITenantRepository tenantRepository,
            IObjectMapper<RocketTenantManagementDomainModule> objectMapper,
            ICurrentTenant currentTenant) {
            TenantRepository = tenantRepository;
            ObjectMapper = objectMapper;
            CurrentTenant = currentTenant;
        }

        [LocalCache]
        public virtual async Task<TenantConfiguration> FindAsync (string name) {
            using (CurrentTenant.Change (null)) //TODO: No need this if we can implement to define host side (or tenant-independent) entities!
            {
                var tenant = await TenantRepository.FindByNameAsync (name);
                if (tenant == null) {
                    return null;
                }

                return ObjectMapper.Map<Tenant, TenantConfiguration> (tenant);
            }
        }

        [LocalCache]
        public virtual async Task<TenantConfiguration> FindAsync (Guid id) {
            using (CurrentTenant.Change (null)) {
                var tenant = await TenantRepository.FindAsync (id);
                if (tenant == null) {
                    return null;
                }

                return ObjectMapper.Map<Tenant, TenantConfiguration> (tenant);
            }
        }

        [LocalCache]
        public virtual TenantConfiguration Find (string name) {
            using (CurrentTenant.Change (null)) {
                var tenant = TenantRepository.FindByName (name);
                if (tenant == null) {
                    return null;
                }

                return ObjectMapper.Map<Tenant, TenantConfiguration> (tenant);
            }
        }

        [LocalCache]
        public virtual TenantConfiguration Find (Guid id) {
            using (CurrentTenant.Change (null)) {
                var tenant = TenantRepository.FindById (id);
                if (tenant == null) {
                    return null;
                }

                return ObjectMapper.Map<Tenant, TenantConfiguration> (tenant);
            }
        }
    }
}