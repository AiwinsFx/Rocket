using AutoMapper;

namespace Aiwins.Rocket.TenantManagement
{
    public class RocketTenantManagementApplicationAutoMapperProfile : Profile
    {
        public RocketTenantManagementApplicationAutoMapperProfile()
        {
            CreateMap<Tenant, TenantDto>()
                .MapExtraProperties();
        }
    }
}