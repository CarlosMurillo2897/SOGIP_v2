$.validator.setDefaults({
    submitHandler: function () {
        alert("Enviando solicitud.");
    }
});

$.validator.addMethod("passw",
    function (value, element) {
        return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)([A-Za-z0-9]){6,12}$/.test(value);
});

$(document).ready(function () {

    $('[data-toggle="popover"]').popover();

    var today = new Date();
    var min = new Date();
    var max = new Date();
    max.setFullYear(today.getFullYear() - 80);
    min.setFullYear(today.getFullYear() - 10);

        $("#dtp").datepicker({
            format: "dd/mm/yyyy",
            defaultViewDate: max,
            startDate: max,
            endDate: min,
            daysOfWeekDisabled: false
    });
        
    $("#signupForm1").validate({
        rules: {
            ced: {
                required: true,
                minlength: 9
            },
            nom1: {
                required: true,
                minlength: 2
            },
            ape1: {
                required: true,
                minlength: 2
            },
            gender: {
                required: true
            },
            password1: {
                required: true,
                minlength: 6,
                passw: true
            },
            email1: {
                required: true,
                email: true
            },
            nac: {
                required: true
            }
        },
        messages: {
            ced: {
                required: "La cédula es un campo obligatorio.",
                minlength: "La longitud mínima de la cédula debe ser de 9 carácteres."
            },
            nom1: {
                required: "El primer nombre es un campo requerido.",
                minlength: "La longitud mínima del primer nombre debe ser de 2 carácteres."
            },
            ape1: {
                required: "El primer apellido es un campo requerido.",
                minlength: "La longitud mínima del primer apellido debe ser de 2 carácteres."
            },
            gender: {
                required: "El campo de género es requerido."
            },
            password1: {
                required: "El campo de contraseña es obligatorio.",
                minlength: "La contraseña debe tener un mínimo de 6 letras.",
                passw : "Contraseña requiere de al menos una letra mayúscula, una minúscula, un número y una longitud de entre 6 y 12 letras."
            },
            email1: "Especifique su e-mail con el formato correcto.",
            nac: "La fecha de nacimiento es obligatoria."
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("help-block");
            element.parents(".afect").addClass("has-feedback");

            if (element.prop("id") === "nac") {
                error.insertAfter(element.parent("div"));
            } else {
                error.insertAfter(element);
            }
            if (!element.next("span")[0]) {
                $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
            }
        },
        success: function (label, element) {
            if (!$(element).next("span")[0]) {
                $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".afect").addClass("has-error").removeClass("has-success");
            $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".afect").addClass("has-success").removeClass("has-error");
            $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
        }
    });

});

function cargarArchivos(id) {

    $('#archivosUsuario').DataTable().destroy();
    $('#archivosUsuario').remove();
    var inactive = $('#invalid').val();

    var table = $('<table/>', {
        id: 'archivosUsuario',
        class: 'table table-striped table-bordered dt-responsive nowrap',
        width: '100%'
    }).append('<thead><tr><th>Nombre</th><th>Tipo</th><th>Descarga</th></tr></thead>');

     $('#files').append(table);

    table = $('#archivosUsuario').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando página _PAGE_ de _PAGES_.",
            "infoEmpty": "No hay datos para mostrar",
            "infoFiltered": "(filtrado de _MAX_ datos obtenidos).",
            "search": "Filtrar:",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/UsersAdmin/ArchivosUsuario",
            "type": "GET",
            "dataSrc": "",
            "data": { usuarioId: id }
        },
        columns: [
            { data: "Nombre" },
            { data: "Tipo" },
            {
                data: "ArchivoId",
                render: function (ArchivoId) {
                    var disable = "disabled";
                    var href = "#";
                    if (!inactive) {
                        disable = "";
                        href = "href='/UsersAdmin/Download?archivoId=" + ArchivoId + "'";
                    }

                    return "<a class='btn btn-info' " + href + " style='padding: 2px 6px; margin: 2px;' " + disable + " >"
                        + "<text class='hidden-xs'>Descargar </text><span class='glyphicon glyphicon-download'></span></a>";
                }
            }
        ]
    });
}

function cargarAtletas(ced) {

    $('#asignados').DataTable().destroy();
    $('#asignados').remove();

    var url = "";
    var col = [
        { data: "Cedula" },
        { data: "Nombre" },
        { data: "Apellido1" },
        { data: "Apellido2" }
    ];

    var head = '<thead><tr><td>Cédula</td><td>Nombre</td><td>1° Apellido</td><td>2° Apellido</td><td>Acción</td></tr></thead>';

    if ($('#role').val() === "Administrador") {
        url = "/AtletasAsignados/GetAtletasAdministrador";
    }
    else if ($('#role').val() === "Seleccion/Federacion") {
        url = "/AtletasAsignados/GetUsuariosSeleccion";
        head = '<thead><tr><td>Cédula</td><td>Nombre</td><td>1° Apellido</td><td>2° Apellido</td><td>Rol</td><td>Categoría</td><td>Acción</td></tr></thead>';
        col[col.length] = { data: "Rol" };
        col[col.length] = { data: "Categoria" };
    }
    else if ($('#role').val() === "Asociacion/Comite") {
        url = "/AtletasAsignados/GetAtletasAsociacion";
    }
    else if ($('#role').val() === "Entrenador") {
        url = "/AtletasAsignados/GetUsuariosEntrenador";
    }

    col[col.length] = {
        data: "Id",
        "render": function (Id) {
            return "<a class='btn btn-warning' href='/Account/Perfil?id=" + Id + "' style='padding: 2px 6px; margin:2px;'>" +
                "<text class=''> Ver en detalle </text>" +
                "<span class='glyphicon glyphicon-user'></span>" +
                "</a>";
        }
    };

    var table = $('<table/>', {
        id: 'asignados',
        class: 'table table-striped table-bordered dt-responsive',
        width: '100%'
    }).append(head);

    $('#asignados').append(head);
    $('#athletes').append(table);

    $('#asignados').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando página _PAGE_ de _PAGES_.",
            "infoEmpty": "No hay datos para mostrar",
            "infoFiltered": "(filtrado de _MAX_ datos obtenidos).",
            "search": "Filtrar:",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": url,
            "type": "GET",
            "dataSrc": "",
            "data": { usuarioId: ced }
        },
        columns: col
    });


}
