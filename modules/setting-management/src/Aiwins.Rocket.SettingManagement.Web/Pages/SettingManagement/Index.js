(function ($) {
    var l = rocket.localization.getResource('RocketSettingManagement');

    $(document).on("RocketSettingSaved", function () {
        rocket.notify.success(l("SuccessfullySaved"));
    });
})(jQuery);