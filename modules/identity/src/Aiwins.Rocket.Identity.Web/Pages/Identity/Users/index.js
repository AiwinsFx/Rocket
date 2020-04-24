(function ($) {

    var l = rocket.localization.getResource('RocketIdentity');

    var _identityUserAppService = aiwins.rocket.identity.identityUser;
    var _editModal = new rocket.ModalManager(rocket.appPath + 'Identity/Users/EditModal');
    var _createModal = new rocket.ModalManager(rocket.appPath + 'Identity/Users/CreateModal');
    var _permissionsModal = new rocket.ModalManager(rocket.appPath + 'RocketPermissionManagement/PermissionManagementModal');

    $(function () {

        var _$wrapper = $('#IdentityUsersWrapper');
        var _$table = _$wrapper.find('table');
        var _dataTable = _$table.DataTable(rocket.libs.datatables.normalizeConfiguration({
            order: [[1, "asc"]],
			processing: true,
			serverSide: true,
            scrollX: true,
			paging: true,
            ajax: rocket.libs.datatables.createAjax(_identityUserAppService.getList),
            columnDefs: [
                {
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: rocket.auth.isGranted('RocketIdentity.Users.Update'),
                                    action: function (data) {
                                        _editModal.open({
                                            id: data.record.id
                                        });
                                    }
                                },
                                {
                                    text: l('Permissions'),
                                    visible: rocket.auth.isGranted('RocketIdentity.Users.ManagePermissions'),
                                    action: function (data) {
                                        _permissionsModal.open({
                                            providerName: 'U',
                                            providerKey: data.record.id
                                        });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: rocket.auth.isGranted('RocketIdentity.Users.Delete'),
                                    confirmMessage: function (data) { return l('UserDeletionConfirmationMessage', data.record.userName); },
                                    action: function (data) {
                                        _identityUserAppService
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
                    data: "userName"
                },
                {
                    data: "email"
                },
                {
                    data: "phoneNumber"
                }
            ]
        }));

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateUser]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

})(jQuery);
