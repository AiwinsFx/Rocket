using AutoMapper;

namespace Aiwins.Rocket.Identity {
    public class RocketIdentityApplicationModuleAutoMapperProfile : Profile {
        public RocketIdentityApplicationModuleAutoMapperProfile () {
            CreateMap<IdentityUser, IdentityUserDto> ()
                .MapExtraProperties ();

            CreateMap<IdentityRole, IdentityRoleDto> ()
                .MapExtraProperties ();

            CreateMap<IdentityUser, ProfileDto> ()
                .MapExtraProperties ();
        }
    }
}