using Aiwins.Rocket.Account.Web.Pages.Account;
using Aiwins.Rocket.Identity;
using AutoMapper;

namespace Aiwins.Rocket.Account.Web
{
    public class RocketAccountWebAutoMapperProfile : Profile
    {
        public RocketAccountWebAutoMapperProfile()
        {
            CreateMap<ProfileDto, PersonalSettingsInfoModel>();
        }
    }
}
