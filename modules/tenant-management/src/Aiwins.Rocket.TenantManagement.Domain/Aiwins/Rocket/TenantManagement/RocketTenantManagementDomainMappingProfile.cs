using AutoMapper;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.TenantManagement
{
    public class RocketTenantManagementDomainMappingProfile : Profile
    {
        public RocketTenantManagementDomainMappingProfile()
        {
            CreateMap<Tenant, TenantConfiguration>()
                .ForMember(ti => ti.ConnectionStrings, opts =>
                {
                    opts.MapFrom((tenant, ti) =>
                    {
                        var connStrings = new ConnectionStrings();

                        foreach (var connectionString in tenant.ConnectionStrings)
                        {
                            connStrings[connectionString.Name] = connectionString.Value;
                        }

                        return connStrings;
                    });
                });

            CreateMap<Tenant, TenantEto>();
        }
    }
}
