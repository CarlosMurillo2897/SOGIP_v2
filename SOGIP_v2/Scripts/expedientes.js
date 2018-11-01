$(document).ready(function () {

    cargaF();

    $('#funcionario').change(function () {
        cargaF();
    })

    $('#atleta').change(function () {
        cargaA();
    })

});


function cargaF() {
    $.getJSON("/ExpedientesFisicos/getUsuariosF", null, function (data) {
        $("#usuariosDropdown").empty() // Remove all <option> child tags.
        $.each(data, function (index, item) { // Iterates through a collection
            $("#usuariosDropdown").append(
                $('<option></option>')
                    .text(item.cedNomCompleto)
                    .val(item.idAtleta)
            );
        });
    });
}

function cargaA() {
    $.getJSON("/ExpedientesFisicos/getUsuariosA", null, function (data) {
        $("#usuariosDropdown").empty() // Remove all <option> child tags.
        $.each(data, function (index, item) { // Iterates through a collection
            $("#usuariosDropdown").append(
                $('<option></option>')
                    .text(item.cedNomCompleto)
                    .val(item.idAtleta)
            );
        });
    });
}