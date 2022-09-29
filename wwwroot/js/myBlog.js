var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#myBlog").DataTable({
        "ajax": {
            "url": "/Admin/Blogs/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "body", "width": "15%" },
            {
                "data": "image",
                "render": function (data) {
                    var img = '/content/blog/' + data;
                    return '<img src="' + img + '" height="50px" width="50px" >';
                }
            },
            { "data": "description", "width": "15%" },
            { "data": "category", "width": "15%" },
            { "data": "tags", "width": "15%" },
            { "data": "created", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/Admin/Blogs/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/Blogs/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>`;
                }, "width": "40%"
            }
        ]
    });
}
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangeMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}