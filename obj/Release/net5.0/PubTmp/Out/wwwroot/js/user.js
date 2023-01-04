let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#userData").DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "email", "width": "25%" },
            { "data": "phoneNumber", "width": "25%" },
            { "data": "company.name", "width": "25%" },
            { "data": "role", "width": "25%" }
        ]
    });
}
