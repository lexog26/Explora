// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function delete_file_function(fileId) {
    if (confirm('Desea eliminar el modelo?')) {
        $.ajax({
            type: "Delete",
            url: "/files/delete",
            data: { id: fileId },
            success: function () {
                alert("Modelo eliminado");
                window.location.replace("/files");
            }
        })
    } else {

    }
}
