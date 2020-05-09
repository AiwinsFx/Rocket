$(function () {

    var l = rocket.localization.getResource('Blogging');
    var _createModal = new rocket.ModalManager(rocket.appPath + 'Admin/Blogs/Create');
    var _editModal = new rocket.ModalManager(rocket.appPath + 'Admin/Blogs/Edit');

    var _dataTable = $('#BlogsTable').DataTable(rocket.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: false,
        info: false,
        scrollX: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[3, "desc"]],
        ajax: rocket.libs.datatables.createAjax(aiwins.blogging.blogs.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: rocket.auth.isGranted('Blogging.Blog.Update'),
                                action: function (data) {
                                    _editModal.open({
                                        blogId: data.record.id
                                    });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: rocket.auth.isGranted('Blogging.Blog.Delete'),
                                confirmMessage: function (data) { return l('BlogDeletionWarningMessage') },
                                action: function (data) {
                                    aiwins.blogging.blogs
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
                target: 1,
                data: "name"
            },
            {
                target: 2,
                data: "shortName"
            },
            {
                target: 3,
                data: "creationTime",
                render: function (date) {
                    return date;
                }
            },
            {
                target: 4,
                data: "description"
            }
        ]
    }));


    $("#CreateNewBlogButtonId").click(function () {
        _createModal.open();
    });

    _createModal.onClose(function () {
        _dataTable.ajax.reload();
    });

    _editModal.onResult(function () {
        _dataTable.ajax.reload();
    });

});