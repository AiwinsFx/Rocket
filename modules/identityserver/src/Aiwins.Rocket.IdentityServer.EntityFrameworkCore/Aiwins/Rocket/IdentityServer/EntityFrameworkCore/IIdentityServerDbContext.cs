using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.ApiResources;
using Aiwins.Rocket.IdentityServer.Clients;
using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.IdentityServer.Grants;
using Aiwins.Rocket.IdentityServer.IdentityResources;

namespace Aiwins.Rocket.IdentityServer.EntityFrameworkCore
{
    [ConnectionStringName(RocketIdentityServerDbProperties.ConnectionStringName)]
    public interface IIdentityServerDbContext : IEfCoreDbContext
    {
        DbSet<ApiResource> ApiResources { get; set; }

        DbSet<ApiSecret> ApiSecrets { get; set; }

        DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

        DbSet<ApiScope> ApiScopes { get; set; }

        DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

        DbSet<IdentityResource> IdentityResources { get; set; }

        DbSet<IdentityClaim> IdentityClaims { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<ClientGrantType> ClientGrantTypes { get; set; }

        DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

        DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

        DbSet<ClientScope> ClientScopes { get; set; }

        DbSet<ClientSecret> ClientSecrets { get; set; }

        DbSet<ClientClaim> ClientClaims { get; set; }

        DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

        DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        DbSet<ClientProperty> ClientProperties { get; set; }

        DbSet<PersistedGrant> PersistedGrants { get; set; }

        DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    }
}
