(function($) {

    var tenantSwitchModal = new abp.ModalManager(abp.appPath + 'Rocket/MultiTenancy/TenantSwitchModal');

    $(function() {
        $('#RocketTenantSwitchLink').click(function(e) {
            e.preventDefault();
            tenantSwitchModal.open();
        });

        tenantSwitchModal.onResult(function() {
            location.reload();
        });
    });

})(jQuery);