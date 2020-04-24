var rocket = rocket || {};
(function ($) {
    rocket.modals = rocket.modals || {};

    rocket.modals.FeatureManagement = function () {

        $('.FeatureValueCheckbox').change(function () {
            if (this.checked) {
                $(this).val("true");
            }
            else {
                $(this).val("false");
            }
        });

    };
})(jQuery);