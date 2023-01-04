let mypost;
$(document).ready(function () {
    mypost = $('#Blog').DataTable({
        "ajax": {
            "url": "/admin/blogs/getall"
        },
        "columns": [
            { "data": "title" },
            { "data": "body" },
            {
                "data": "image",
                "render": function (data) {
                    var img = '/content/blog/' + data;
                    return '<img src="' + img + '" height="50px" width="50px" >';
                }
            },
            { "data": "description" },
            { "data": "tags" },
            { "data": "category" },
            { "data": "created"},
            {
                "data": "id",
                "render": function (data) {
                    return "<a class='btn btn-success' href='/Admin/Blogs/Edit/" + data + "'>Edit</a>";
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return "<button class='btn btn-danger js-delete' data-post-id=" + data + ">Delete</button>";
                }
            }
        ]
    });
    $("#mypost").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this message?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/posts/" + button.attr("data-message-id"),
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                    }
                })
            }
        })
    });
});