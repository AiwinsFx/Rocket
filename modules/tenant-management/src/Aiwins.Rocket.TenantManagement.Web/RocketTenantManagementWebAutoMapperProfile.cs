using AutoMapper;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.TenantManagement.Web.Pages.TenantManagement.Tenants;

namespace Aiwins.Rocket.TenantManagement.Web
{
    public class RocketTenantManagementWebAutoMapperProfile : Profile
    {
        public RocketTenantManagementWebAutoMapperProfile()
        {
            //List
            CreateMap<TenantDto, EditModalModel.TenantInfoModel>();

            //CreateModal
            CreateMap<CreateModalModel.TenantInfoModel, TenantCreateDto>()
                .Ignore(x => x.ExtraProperties);

            //EditModal
            CreateMap<EditModalModel.TenantInfoModel, TenantUpdateDto>()
                .Ignore(x => x.ExtraProperties);
        }
    }
}
