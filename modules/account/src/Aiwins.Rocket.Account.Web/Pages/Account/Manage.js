(function ($) {

    var l = rocket.localization.getResource('RocketAccount');

    var _profileService = aiwins.rocket.identity.profile;

    $("#ChangePasswordForm").submit(function (e) {
        e.preventDefault();

        if (!$("#ChangePasswordForm").valid()) {
            return false;
        }

        var input = $("#ChangePasswordForm").serializeFormToObject().changePasswordInfoModel;

        if (input.newPassword != input.newPasswordConfirm || input.currentPassword == '') {
            rocket.message.error(l("NewPasswordConfirmFailed"));
            return;
        }

        if (input.currentPassword == '') {
            return;
        }

        _profileService.changePassword(
            input
        ).then(function (result) {
            rocket.message.success(l("PasswordChanged"));
        });

    });

    $("#PersonalSettingsForm").submit(function (e) {
        e.preventDefault();

        if (!$("#PersonalSettingsForm").valid()) {
            return false;
        }

        var input = $("#PersonalSettingsForm").serializeFormToObject().personalSettingsInfoModel;

        _profileService.update(
            input
        ).then(function (result) {
            rocket.notify.success(l("PersonalSettingsSaved"));
        });

    });

})(jQuery);