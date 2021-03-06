﻿using System.Threading.Tasks;
using Aiwins.Rocket.Identity.Settings;
using Aiwins.Rocket.ObjectExtending;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Aiwins.Rocket.Identity {
    [Authorize]
    public class ProfileAppService : IdentityAppServiceBase, IProfileAppService {
        protected IdentityUserManager UserManager { get; }

        public ProfileAppService (IdentityUserManager userManager) {
            UserManager = userManager;
        }

        public virtual async Task<ProfileDto> GetAsync () {
            return ObjectMapper.Map<IdentityUser, ProfileDto> (
                await UserManager.GetByIdAsync (CurrentUser.GetId ())
            );
        }

        public virtual async Task<ProfileDto> UpdateAsync (UpdateProfileDto input) {
            var user = await UserManager.GetByIdAsync (CurrentUser.GetId ());

            if (await SettingProvider.IsTrueAsync (IdentitySettingNames.User.IsUserNameUpdateEnabled)) {
                (await UserManager.SetUserNameAsync (user, input.UserName)).CheckErrors ();
            }

            if (await SettingProvider.IsTrueAsync (IdentitySettingNames.User.IsPhoneNumberUpdateEnabled)) {
                (await UserManager.SetPhoneNumberAsync (user, input.PhoneNumber)).CheckErrors ();
            }

            if (await SettingProvider.IsTrueAsync (IdentitySettingNames.User.IsEmailUpdateEnabled)) {
                (await UserManager.SetEmailAsync (user, input.Email)).CheckErrors ();
            }

            user.Name = input.Name;
            user.Surname = input.Surname;

            input.MapExtraPropertiesTo (user);

            (await UserManager.UpdateAsync (user)).CheckErrors ();

            await CurrentUnitOfWork.SaveChangesAsync ();

            return ObjectMapper.Map<IdentityUser, ProfileDto> (user);
        }

        public virtual async Task ChangePasswordAsync (ChangePasswordInput input) {
            var currentUser = await UserManager.GetByIdAsync (CurrentUser.GetId ());
            (await UserManager.ChangePasswordAsync (currentUser, input.CurrentPassword, input.NewPassword)).CheckErrors ();
        }
    }
}