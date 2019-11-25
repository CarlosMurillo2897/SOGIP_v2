var table;
var dataSet = [];
var ced = $('#ced').val();
var url = "";
var date;
var cedu = null;
var idRut = null;

function tablaRutina() {
    if ($('#role').val() == "Administrador") {
        url = "/SOGIP/Rutinas/GetRutinasAdministrador";
    }
    else if ($('#role').val() == "Seleccion/Federacion") {
        url = "/SOGIP/Rutinas/GetRutinasSeleccion";

    }
    else if ($('#role').val() == "Asociacion/Comite") {
        url = "/SOGIP/Rutinas/GetRutinasAsociacion";
    }
    else if ($('#role').val() == "Entrenador") {

        url = "/SOGIP/Rutinas/GetRutinasEntrenador";

    }
    table = $('#rutina').DataTable({
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
        columns: [
            {
                title: "Cédula",
                data: "Cedula"
            },
            {
                title: "Usuario",
                data: "Usuario"
            },
            {
                title: "Fecha Inicio",
                data: "Fecha",
                'render': function (Fecha) {
                    var date = new Date(parseInt(Fecha.substr(6)));
                    return date.toLocaleDateString("en-GB");
                }
            },
            {
                title: "Fecha Fin",
                data: "Fecha2",
                'render': function (Fecha2) {
                    var date2 = new Date(parseInt(Fecha2.substr(6)));
                    return date2.toLocaleDateString("en-GB");
                }
            },
            {
                title: "Objetivo",
                data: "Objetivo"
            },

            {
                title: "Acción",
                data: "Id",
                "render": function (Id) {
                    return "<a class='btn btn-warning' style='padding: 2px 6px; margin: 2px;'  id='boton_" + Id + "' onclick='EditarRut(" + Id + ")'data-toggle='modal'  data-target='#editar'>" +
                        "<text class='hidden-xs'>Editar </text>" +
                        "<span class='glyphicon glyphicon-pencil'></span>" +
                        "</a>" +
                        "<a class='btn btn-primary' style='padding: 2px 6px; margin: 2px;' id='boton_" + Id + "' onclick='EnviarRut(" + Id + ")''>" +
                        "<text class='hidden-xs'>Rutina </text>" +
                        "<span class='glyphicon glyphicon-list'></span>" +
                        "</a>";
                }
            }
        ]
    });
}

function EditarRut(id) {
    idRut = id;
    $('#edit').css("display", "block");
    $('#agr').css("display", "none");
    $('#nuevo').modal('show');
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/Rutinas/ObtnerRutina',
        data: { 'id': id },
        success: function (data) {
            var date = new Date(parseInt(data.FechaInicio.substr(6)));
            var date2 = new Date(parseInt(data.FechaFin.substr(6)));
            //$('#txtStart').datepicker("update", date.toLocaleDateString("en-GB"));
            $('#dtp').datepicker("update", date.toLocaleDateString("en-GB"));
            //$('#txtStart2').datepicker("update", date2.toLocaleDateString("en-GB"));
            $('#dtp1').datepicker("update", date2.toLocaleDateString("en-GB"));
            $('#observ').val(data.RutinaObservaciones);
            $('#usuario').val(data.Usuario.Cedula + " " + data.Usuario.Nombre1 + " " + data.Usuario.Apellido1 + " " + data.Usuario.Apellido2);
            cedu = data.Usuario.Cedula;
        }
    })
}
function Edit(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/Rutinas/EditRutina',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#rutina').DataTable().ajax.reload(null, false);
        },
        error: function () {
            alert("Fallo");
        }
    })
}

function EnviarRut(id) {
    var tr = $('#boton_' + id).closest('tr');
    var table = $('#rutina').DataTable().rows(tr).data();
    var idRutina = null;
    var idUsuario = null;
    $.each(table, function (i, val) {
        idUsuario = val[1];
        idRutina = id;
    });
    data = {
        idUsuario: idUsuario,
        idRutina: id
    }
    window.location.href = '/SOGIP/Rutinas/Ejercicio?idRutina=' + idRutina;

}
function Save(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/Rutinas/SaveRutina',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            window.location.href = '/SOGIP/Rutinas/Ejercicio?idRutina=' + data.RutinaId;
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function enviar() {
    var data = null;
    if ($('#agr').is(':visible')) {
        data = {
            fecha: $('#dtp').data('datepicker').getFormattedDate('dd/mm/yyyy'),
            fecha2: $('#dtp1').data('datepicker').getFormattedDate('dd/mm/yyyy'),
            obs: $('#observ').val().trim(),
            id: cedu
        }
        Save(data);
    } else {
        data = {
            fecha: $('#dtp').data('datepicker').getFormattedDate('dd/mm/yyyy'),
            fecha2: $('#dtp1').data('datepicker').getFormattedDate('dd/mm/yyyy'),
            obs: $('#observ').val().trim(),
            idUs: cedu,
            id: idRut
        }
        Edit(data);
    }
}
$.validator.setDefaults({
    submitHandler: function () {
        enviar();
    }
});

$(document).ready(function () {


    tablaRutina();
    $("#dtp").datepicker({
        format: 'dd/mm/yyyy',
        startDate: '-365d',
        endDate: '+365d',
        daysOfWeekDisabled: false
    });
    $("#dtp1").datepicker({
        format: 'dd/mm/yyyy',
        startDate: '-365d',
        endDate: '+365d',
        daysOfWeekDisabled: false
    });

    $('#btnNuevo').click(function () {
        $('#edit').css("display", "none");
        $('#agr').css("display", "block");
        $('#nuevo').modal('show');
        $('#usuario').val('');
        $('#observ').val('');
        $('#dtp').val('');
        $('#dtp1').val('');
        $('#txtStart').val('');
        $('#txtStart2').val('');

    })

    function usuarios() {
        if ($('#role').val() == "Administrador") {
            url = "/SOGIP/Rutinas/GetAtletasAdministrador";
        }
        else if ($('#role').val() == "Seleccion/Federacion") {
            url = "/SOGIP/Rutinas/GetUsuariosSeleccion";

        }
        else if ($('#role').val() == "Asociacion/Comite") {
            url = "/SOGIP/Rutinas/GetAtletasAsociacion";
        }
        else if ($('#role').val() == "Entrenador") {

            url = "/SOGIP/Rutinas/GetUsuariosEntrenador";

        }
        $('#usuarios1').DataTable().destroy();
        $('#usuarios1').remove();
        $('#example').show();
        var tabla = $('<table/>', { id: 'usuarios1', class: 'table table-striped table-bordered dt-responsive nowrap' }).append('<thead><tr><th>Acción</th><th>Cédula</th><th>Nombre</th><th>Apellido1</th><th>Apellido2</th></tr></thead>');
        $('#example').append(tabla);
        table1 = $('#usuarios1').DataTable({
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
            columns: [
                { data: "Accion" },
                { data: "Cedula" },
                { data: "Nombre" },
                { data: "Apellido1" },
                { data: "Apellido2" }
            ],
            'columnDefs': [
                {
                    orderable: false,
                    className: 'select-checkbox',
                    targets: [0]
                }
            ],
            'select': {
                'style': 'os',
                'selector': 'td:first-child'

            }
        });
    }
    $('#example').on('click', 'td.select-checkbox', function () {
        var td = $(this);
        var tr = td.closest('tr');
        $('#usuario').val(tr.find('td:eq(1)').text() + ' ' + tr.find('td:eq(2)').text() + ' ' + tr.find('td:eq(3)').text() + ' ' + tr.find('td:eq(4)').text());
        cedu = tr.find('td:eq(1)').text();
        $('#tablaUsuarios').hide();
    });


    $('#botón').click(function () {
        usuarios();

    });
    $("#forcat").validate({
        rules: {
            txtStart: {

                required: true,


            },
            txtStart2: {

                required: true,


            },
            obj: {
                required: false,
                minlength: 0

            },
            usuario: {
                required: true,

            }

        },
        messages: {
            txtStart: {
                required: "La fecha es obligatoria."

            },
            txtStart2: {
                required: "La fecha es obligatoria."

            },
            obj: {

            },
            usuario: {
                required: "El Usuario es obligatorio."
            }
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