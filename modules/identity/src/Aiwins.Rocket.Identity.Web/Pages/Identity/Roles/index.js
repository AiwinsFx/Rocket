(function ($) {

    var l = rocket.localization.getResource('RocketIdentity');

    var _identityRoleAppService = aiwins.rocket.identity.identityRole;
    var _permissionsModal = new rocket.ModalManager(rocket.appPath + 'RocketPermissionManagement/PermissionManagementModal');
    var _editModal = new rocket.ModalManager(rocket.appPath + 'Identity/Roles/EditModal');
    var _createModal = new rocket.ModalManager(rocket.appPath + 'Identity/Roles/CreateModal');

    $(function () {

        var _$wrapper = $('#IdentityRolesWrapper');
        var _$table = _$wrapper.find('table');

        var _dataTable = _$table.DataTable(rocket.libs.datatables.normalizeConfiguration({
            order: [[1, "asc"]],
            searching: false,
            processing: true,
            serverSide: true,
            scrollX: true,
            paging: true,
            ajax: rocket.libs.datatables.createAjax(_identityRoleAppService.getList),
            columnDefs: [
                {
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: rocket.auth.isGranted('RocketIdentity.Roles.Update'),
                                    action: function (data) {
                                        _editModal.open({
                                            id: data.record.id
                                        });
                                    }
                                },
                                {
                                    text: l('Permissions'),
                                    visible: rocket.auth.isGranted('RocketIdentity.Roles.ManagePermissions'),
                                    action: function (data) {
                                        _permissionsModal.open({
                                            providerName: 'R',
                                            providerKey: data.record.name
                                        });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: function (data) {
                                        return !data.isStatic && rocket.auth.isGranted('RocketIdentity.Roles.Delete'); //TODO: Check permission
                                    },
                                    confirmMessage: function (data) { return l('RoleDeletionConfirmationMessage', data.record.name)},
                                    action: function (data) {
                                        _identityRoleAppService
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
                    data: "name",
                    render: function (data, type, row) {
                        var name = '<span>' + data + '</span>';
                        if (row.isDefault) {
                            name += '<span class="badge badge-pill badge-success ml-1">' + l('DisplayName:IsDefault') + '</span>';
                        }
                        if (row.isPublic) {
                            name += '<span class="badge badge-pill badge-info ml-1">' + l('DisplayName:IsPublic') + '</span>';
                        }
                        return name;
                    }
                }
            ]
        }));

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateRole]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

})(jQuery);
