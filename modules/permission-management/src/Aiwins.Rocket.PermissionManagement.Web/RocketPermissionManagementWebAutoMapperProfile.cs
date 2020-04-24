using AutoMapper;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.PermissionManagement.Web.Pages.RocketPermissionManagement;

namespace Aiwins.Rocket.PermissionManagement.Web
{
    public class RocketPermissionManagementWebAutoMapperProfile : Profile
    {
        public RocketPermissionManagementWebAutoMapperProfile()
        {
            CreateMap<PermissionGroupDto, PermissionManagementModal.PermissionGroupViewModel>().Ignore(p=>p.IsAllPermissionsGranted);

            CreateMap<PermissionGrantInfoDto, PermissionManagementModal.PermissionGrantInfoViewModel>()
                .ForMember(p => p.Depth, opts => opts.Ignore());

            CreateMap<ProviderInfoDto, PermissionManagementModal.ProviderInfoViewModel>();
        }
    }
}
