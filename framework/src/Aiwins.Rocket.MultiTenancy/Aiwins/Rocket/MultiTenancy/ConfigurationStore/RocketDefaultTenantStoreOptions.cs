namespace Aiwins.Rocket.MultiTenancy.ConfigurationStore {
    public class RocketDefaultTenantStoreOptions {
        public TenantConfiguration[] Tenants { get; set; }

        public RocketDefaultTenantStoreOptions () {
            Tenants = new TenantConfiguration[0];
        }
    }
}