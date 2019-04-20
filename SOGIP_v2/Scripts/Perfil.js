$.validator.setDefaults({
    submitHandler: function () {
        alert("Enviando solicitud.");
        sendData();
    }
});

$.validator.addMethod("passw", function (value, element) {
    return value.length !== 0 ? /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)([A-Za-z0-9]){6,12}$/.test(value) :  true;
});
var p1, p2 = "";
$(document).ready(function () {
    
    unlockForm("None", "None", "Nn");
    //unlockForm("Supervisor", "None", "Nn");

    $('[data-toggle="popover"]').popover();

    var today = new Date();
    var min = new Date();
    var max = new Date();
    max.setFullYear(today.getFullYear() - 80);
    min.setFullYear(today.getFullYear() - 10);

        $("#dtp").datepicker({
            format: 'yyyy/mm/dd',
            defaultViewDate: max,
            startDate: max, 
            endDate: min,
            daysOfWeekDisabled: false
    });
        
    $("#signupForm1").validate({
        rules: {
            Cedula: {
                required: true,
                minlength: 9,
                remote: {
                    url: "/UsersAdmin/CedulaRepetida",
                    type: "GET",
                    data: {
                        ced: function () { return $('#Cedula').val(); },
                        Id: function () { return $('#Id').val(); }
                    }
                }
            },
            Nombre1: {
                required: true,
                minlength: 2
            },
            Apellido1: {
                required: true,
                minlength: 2
            },
            Sexo: {
                required: true
            },
            PasswordHash: {
                // required: true,
                minlength: 6,
                passw: true
            },
            Email: {
                required: true,
                email: true
            },
            Fecha_Nacimiento: {
                required: true
            },
            // Selecciones
            Nombre_Seleccion: {
                required: true,
                minlength: 6,
                remote: {
                    url: "/UsersAdmin/NombreSeleccionRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#Nombre_Seleccion').val(); },
                        Id: function () { return $('#Id').val(); }
                    }
                }
            },
            Nombre_Deporte: {
                required: true
            }
        },
        messages: {
            Cedula: {
                required: "La cédula es un campo obligatorio.",
                minlength: "La longitud mínima de la cédula debe ser de 9 carácteres.",
                remote: "La cédula ingresada ya se encuentra en el sistema."
            },
            Nombre1: {
                required: "El primer nombre es un campo requerido.",
                minlength: "La longitud mínima del primer nombre debe ser de 2 carácteres."
            },
            Apellido1: {
                required: "El primer apellido es un campo requerido.",
                minlength: "La longitud mínima del primer apellido debe ser de 2 carácteres."
            },
            Sexo: {
                required: "El campo de género es requerido."
            },
            PasswordHash: {
                // required: "El campo de contraseña es obligatorio.",
                minlength: "La contraseña debe tener un mínimo de 6 letras.",
                passw : "Contraseña requiere de al menos una letra mayúscula, una minúscula, un número y una longitud de entre 6 y 12 letras."
            },
            Email: "Especifique su e-mail con el formato correcto.",
            Fecha_Nacimiento: "La fecha de nacimiento es obligatoria.",
            // Selecciones
            Nombre_Seleccion: {
                required: "El nombre de la selección es un campo obligatorio.",
                minlength: "La longitud mínima del nombre de la selección debe ser de 6 carácteres.",
                remote: "El nombre de la selección ingresada ya se encuentra en el sistema."
            },
            Nombre_Deporte: {
                required: "El Deporte de la Selección es requerido."
            }
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("help-block");
            element.parents(".afect").addClass("has-feedback");

            if (element.prop("id") === "Fecha_Nacimiento") {
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
//"@ViewBag.rol_Usuario_Actual", "@ViewBag.usuario_Actual", "@Model.Cedula", true
function unlockForm(role, id_Actual, Cedula) {
    if (role === 'Administrador' || role === 'Supervisor' || id_Actual === Cedula) {
        $("#signupForm1 :input").attr("disabled", false);
    }
    else{
        $("#signupForm1 :input").attr("disabled", true);
    }
}

function GetCategorias() {
    var newArray = [];
    var TableData = [];
    $('#tablaCategorias tbody tr').each(function (row, tr) {
        TableData.push($(tr).find("td:eq(0)").text());
    });
    console.log(TableData);

    $.ajax({
        url: "/Opciones/GetCategorias",
        type: "GET",
        success: function (consulta) {
            $.each(consulta, function (i) {
                newArray.push(consulta[i].Descripcion);
            });


            newArray = newArray.filter((el) => !TableData.includes(el));
            
            console.log(newArray);
        }
    });

}

function sendData() {

    $("#signupForm1 :input[type=text]").val(function () {
        return this.value.toUpperCase();
    });

    var usr = $('#signupForm1').serialize();
    
    usr = usr + "&Nombre_Seleccion=" + $('#Cedula').val();
    //Nombre_Seleccion, Deporte_Id
    //console.log(usr);

    $.ajax({
        url: "/UsersAdmin/UpdateUser/",
        type: "POST",
        data: usr,
        success: function () {
            alert('Cambios realizados.');
            location.reload();
        },
        error: function () {
            alert('Error desconocido. Contactar a soporte si continua.');
        }
    });

}

function OriginalPass() {
    if (!confirm('¿Está seguro de devolver la contraseña a sus valores predeterminados?')) {
        return;
    }
    $.ajax({
        url: "/UsersAdmin/RestaurarContraseñaOriginal",
        type: "POST",
        data: { Id: $('#Id').val() },
        success: function () {
            alert('Contraseña restaurada con éxito.');
        },
        error: function () {
            alert('Operación no completada. Intente de nuevo o contacte soporte en breve.');
        }

    });
}

function cargarModal(tipo) {
    p1 = "";
    var saved = [];
    var url, header, rowSelected = "";
    var col = [];
    switch (tipo) {
        case 1: {
            $('#modalTitle').html('Buscar Deporte deseado');
            url = "/Opciones/GetDeportes";
            header = header + '<th>Nombre</th><th>Tipo de Deporte</th><th>Identificador</th></tr>';
            col = [
                { data: "Nombre" },
                { data: "Descripcion" },
                { data: "DeporteId" }
            ];
            break;
        }
        case 2: {
            $('#modalTitle').html('Agregar nueva Categoría');
            url = "";
            break;
        }
    }

    $('#datos').DataTable().destroy();
    $('#datos').remove();

    $('<table />', {
        id: 'datos',
        class: 'table table-striped table-bordered table-hover dt-responsive',
        width: "100%"
    }).append("<thead>" + header + "</thead>").appendTo('#modalBody');

    $('#datos').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando _START_ de _END_, de un total de _TOTAL_ registros.",
            "infoEmpty": "No hay datos para mostrar.",
            "infoFiltered": "(filtrado de _MAX_ datos obtenidos).",
            "search": "Filtrar:",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "select": {
                "rows": {
                    _: "%d registros seleccionados.",
                    0: "Presione sobre la fila deseada.",
                    1: "1 registro seleccionado."
                }
            }
        },
        "ajax": {
            "url": url,
            "type": "GET",
            "dataSrc": ""
        },
        columns: col,
        select: true/*,
        "createdRow": function (row, data, index) { if (data.Nombre === $('#Nombre_Deporte').val() ) { $(row).addClass('selected'); rowSelected = $(row); } }*/
    });

    $('#datos tbody').on('click', 'tr', function () {
        /*if (rowSelected !== undefined) { rowSelected.removeClass('selected'); rowSelected = undefined; }*/
        var data = $('#datos').DataTable().row(this).data();
        p1 = data.Nombre;

    });

    $('#modal').modal('show');

}

$('#modalSave').on('click', function () {
    if (p1 !== "") { $('#Nombre_Deporte').val(p1); }
    $('#modal').modal('hide');
});