(function () {

    var l = rocket.localization.getResource('RocketTenantManagement');
    var _tenantAppService = aiwins.rocket.tenantManagement.tenant;

    var _editModal = new rocket.ModalManager(rocket.appPath + 'TenantManagement/Tenants/EditModal');
    var _createModal = new rocket.ModalManager(rocket.appPath + 'TenantManagement/Tenants/CreateModal');
    var _featuresModal = new rocket.ModalManager(rocket.appPath + 'FeatureManagement/FeatureManagementModal');
    var _connectionStringsModal = new rocket.ModalManager({
        viewUrl: rocket.appPath + 'TenantManagement/Tenants/ConnectionStringsModal',
        modalClass: 'TenantConnectionStringManagement'
    });

    $(function () {

        var _$wrapper = $('#TenantsWrapper');

        var _dataTable = _$wrapper.find('table').DataTable(rocket.libs.datatables.normalizeConfiguration({
            order: [[1, "asc"]],
            processing: true,
            paging: true,
            scrollX: true,
            serverSide: true,
            ajax: rocket.libs.datatables.createAjax(_tenantAppService.getList),
            columnDefs: [
                {
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: rocket.auth.isGranted('RocketTenantManagement.Tenants.Update'),
                                    action: function (data) {
                                        _editModal.open({
                                            id: data.record.id
                                        });
                                    }
                                },
	                            {
                                    text: l('ConnectionStrings'),
                                    visible: rocket.auth.isGranted('RocketTenantManagement.Tenants.ManageConnectionStrings'),
		                            action: function (data) {
			                            _connectionStringsModal.open({
				                            id: data.record.id
			                            });
		                            }
	                            },
                                {
                                    text: l('Features'),
                                    visible: rocket.auth.isGranted('RocketTenantManagement.Tenants.ManageFeatures'),
                                    action: function (data) {
                                        _featuresModal.open({
                                            providerName: 'T',
                                            providerKey: data.record.id
                                        });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: rocket.auth.isGranted('RocketTenantManagement.Tenants.Delete'),
                                    confirmMessage: function (data) { return l('TenantDeletionConfirmationMessage', data.record.name)},
                                    action: function (data) {
                                        _tenantAppService
                                            .delete(data.record.id)
                                            .then(function () {
                                                _dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    data: "name"
                }
            ]
        }));

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateTenant]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

})();
