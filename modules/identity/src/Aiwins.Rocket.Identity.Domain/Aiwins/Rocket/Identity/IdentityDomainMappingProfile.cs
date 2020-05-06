using Aiwins.Rocket.Users;
using AutoMapper;

namespace Aiwins.Rocket.Identity {
    public class IdentityDomainMappingProfile : Profile {
        public IdentityDomainMappingProfile () {
            CreateMap<IdentityUser, UserEto> ();
            CreateMap<IdentityClaimType, IdentityClaimTypeEto> ();
            CreateMap<IdentityRole, IdentityRoleEto> ();
        }
    }
}