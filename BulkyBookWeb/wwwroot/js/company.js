var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "streetAdress", "width": "20%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Company/Upsert?id=${data}" class="btn btn-sm btn-primary mx-1">
                                <i class="bi bi-pencil-fill"></i> Edit
                            </a>
                            <a  onclick="Delete('/Admin/Company/Delete/${data}')" class="btn btn-sm btn-danger mx-1">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>
                    `;
                },
                "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        datatable.ajax.reload(); // burası küçük harfle olmalı
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
