$(function () {

    var l = rocket.localization.getResource('Docs');

    var _createModal = new rocket.ModalManager({
        viewUrl: rocket.appPath + 'Docs/Admin/Projects/Create',
        modalClass: 'projectCreate'
    });

    var _editModal = new rocket.ModalManager({
        viewUrl: rocket.appPath + 'Docs/Admin/Projects/Edit',
        modalClass: 'projectEdit'
    });

    var _pullModal = new rocket.ModalManager({
        viewUrl: rocket.appPath + 'Docs/Admin/Projects/Pull',
        modalClass: 'projectPull'
    });


    var _dataTable = $('#ProjectsTable').DataTable(rocket.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        scrollX: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[2, "desc"]],
        ajax: rocket.libs.datatables.createAjax(aiwins.docs.admin.projectsAdmin.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: rocket.auth.isGranted('Docs.Admin.Projects.Update'),
                                action: function (data) {
                                    _editModal.open({
                                        Id: data.record.id
                                    });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: rocket.auth.isGranted('Docs.Admin.Projects.Delete'),
                                confirmMessage: function (data) { return l('ProjectDeletionWarningMessage') },
                                action: function (data) {
                                    aiwins.docs.admin.projectsAdmin
                                        .delete(data.record.id)
                                        .then(function () {
                                            _dataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l('Pull'),
                                visible: rocket.auth.isGranted('Docs.Admin.Documents'),
                                action: function (data) {
                                    _pullModal.open({
                                        Id: data.record.id
                                    });
                                }
                            },
                            {
                                text: l('ClearCache'),
                                visible: rocket.auth.isGranted('Docs.Admin.Documents'),
                                confirmMessage: function (data) { return l('ClearCacheConfirmationMessage', data.record.name); },
                                action: function (data) {
                                    aiwins.docs.admin.documentsAdmin
                                        .clearCache({ projectId: data.record.id})
                                        .then(function () {
                                            _dataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l('ReIndexProject'),
                                visible: rocket.auth.isGranted('Docs.Admin.Documents'),
                                confirmMessage: function (data) { return l('ReIndexProjectConfirmationMessage', data.record.name); },
                                action: function (data) {
                                    aiwins.docs.admin.projectsAdmin
                                        .reindex({ projectId: data.record.id})
                                        .then(function () {
                                            rocket.message.success(l('SuccessfullyReIndexProject', data.record.name));
                                        });
                                }
                            }
                        ]
                }
            },
            {
                target: 1,
                data: "name"
            },
            {
                target: 2,
                data: "shortName"
            },
            {
                target: 3,
                data: "documentStoreType"
            },
            {
                target: 4,
                data: "format",
                render: function (data) {
                    if (data === 'md') {
                        return 'markdown';
                    }

                    return data;
                }
            }
        ]
    }));


    $("#CreateNewGithubProjectButtonId").click(function (event) {
        event.preventDefault();
        _createModal.open({source:"GitHub"});
    });

    $("#ReIndexAllProjects").click(function (event) {
        rocket.message.confirm(l('ReIndexAllProjectConfirmationMessage'))
            .done(function (accepted) {
                if (accepted) {
                    aiwins.docs.admin.projectsAdmin
                    .reindexAll()
                    .then(function () {
                        rocket.message.success(l('SuccessfullyReIndexAllProject'));
                    });
                }
            });
    });

    _createModal.onClose(function () {
        _dataTable.ajax.reload();
    });

    _editModal.onResult(function () {
        _dataTable.ajax.reload();
    });

});
