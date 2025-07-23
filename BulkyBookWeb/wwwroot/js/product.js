var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "25%" },
            { "data": "isbn", "width": "20%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-sm btn-primary mx-1">
                                <i class="bi bi-pencil-fill"></i> Edit
                            </a>
                            <a  onclick="Delete('/Admin/Product/Delete/${data}')" class="btn btn-sm btn-danger mx-1">
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
