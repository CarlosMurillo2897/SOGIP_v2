function eliminar() {
    var des = $('#tablePrin').DataTable();
    des.remove();
    des.destroy();
}
var cat1 = null
var cat2 = null;
var cat3 = null;
var idEjer = null;
function tablaPrin() {
    var d = @viewDataMaquina;
    data = {
        id: d
    }
    $('#maquinas').DataTable().destroy();
    $('#maquinas').remove();
    $('#tablaPrincipal').show();
    var tabla = $('<table/>', { id: 'maquinas', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Nombre</th><th>Acción</th></tr></thead>');
    $('#tablaPrincipal').append(tabla);
    table5 = $('#maquinas').DataTable({
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
            "url": '/SOGIP/TipoME/getEjercicios',
            "type": "GET",
            "data": data,
            "dataSrc": ""
        },
        columns: [
            { data: "Nombre" },
            {
                data: "Id",
                render: function (Id) {
                    return "<a class='btn btn-danger' style='padding: 2px 6px; margin: 2px;'  id='boton_" + Id + "' onclick='Eliminar(" + Id + ")' data-toggle='modal' data-target='#editar'>" +
                        "<text class='hidden-xs'>Eliminar Maquina </text>" +
                        "<span class='glyphicon glyphicon-pencil'></span>" +
                        "</a>";
                }
            }
        ],
    });
}
function tablaEjercicios() {
    $('#tipoEjer1').DataTable().destroy();
    $('#tipoEjer1').remove();
    $('#tablePrin').show();
    var tabla = $('<table/>', { id: 'tipoEjer1', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Acción</th><th>Nombre</th></tr></thead>');
    $('#tablePrin').append(tabla);
    table = $('#tipoEjer1').DataTable({
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
            "url": "/SOGIP/TipoME/GetEjerMaquina",
            "type": "GET",
            "dataSrc": ""
        },
        columns: [
            { data: "Accion" },
            { data: "Nombre" },
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
function Eliminar(id) {
    var n = @viewDataMaquina;
    var data = null;
    data = {
        nom: n,
        ejer: id
    }
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/DeleteMaquinaEjercicio',
        data: data,
        success: function (data) {
            $('#nueva').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#maquinas').DataTable().ajax.reload(null, false);
            //eliminar();
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function SaveMaquinaEjercicio(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/SaveMaquinaEjercicio',
        data: data,
        success: function (data) {
            $('#nueva').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#maquinas').DataTable().ajax.reload(null, false);
            $('#usuario').val('');
            //eliminar();
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function SaveMaquinaEjercicio2(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/SaveEjerciciosMaq',
        data: data,
        success: function (data) {
            $('#nueva').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#maquinas').DataTable().ajax.reload(null, false);
            $('#cat').val('');
            //eliminar();
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function enviar() {
    var n = @viewDataMaquina;
    var n1 = null;
    if ($('#labelEjer').is(':visible')) {
        var data = null;
        {
            data = {
                nom: n,
                nombre: $('#cat').val()
            }
            SaveMaquinaEjercicio2(data);
        }
    } else {
        if (idEjer != null) {
            $.ajax({
                type: "POST",
                dataType: "JSON",
                url: '/SOGIP/TipoME/obtenerIdEjer',
                data: { "n": idEjer },
                success: function (data) {
                    n1 = data.Id;
                    var data = null;
                    {
                        data = {
                            nom: n,
                            ejer: n1
                        }
                        SaveMaquinaEjercicio(data);
                    }
                },
                error: function () {
                    alert("Fallo");
                }
            })
        }
    }
}
$.validator.setDefaults({
    submitHandler: function () {
        enviar();
        alert("Enviando solicitud.");
    }
});
$(document).ready(function () {
    tablaPrin();
    $('#btnNuevo').click(function () {
        $("#radio_1").prop("checked", true);
        $('#nueva').modal('show');
        $('#repejer').prop('checked', false);
        $('#usuario').css("display", "none");
        $('#labelEjer').css("display", "block");
        $('#cat').css("display", "block");
        $('#botón').css("display", "none");

        $('#newejer').click(function () {
            $('#cat').val('');
            $('#usuario').css("display", "none");
            $('#labelEjer').css("display", "block");
            $('#cat').css("display", "block");
            $('#botón').css("display", "none");
            $('#tipoEjer1').DataTable().destroy();
            $('#tipoEjer1').remove();
            $('#tablePrin').hide();
            $('#Tabla_Prin').hide();
        })
        $('#repejer').click(function () {
            $('#usuario').val('');
            $('#usuario').css("display", "block");
            $('#labelEjer').css("display", "none");
            $('#cat').css("display", "none");
            $('#botón').css("display", "block");
            $('#tablePrin').show();
            tablaEjercicios();
        })
    })



    $('#tablePrin').on('click', 'td.select-checkbox', function () {
        var td = $(this);
        var tr = td.closest('tr');
        $('#usuario').val(tr.find('td:eq(1)').text());
        idEjer = tr.find('td:eq(1)').text();
        $('#Tabla_Prin').hide();
    });

    $('#botón').click(function () {
        $('#Tabla_Prin').show();

    });
    var n = @viewDataMaquina;
    $("#forcat").validate({

        rules: {
            cat: {

                required: true,
                minlength: 4,
                remote: {
                    url: '/TipoME/EjercicioRepetido',
                    type: 'GET',
                    data: {
                        nombre: function () {
                            return $('#cat').val();
                        }
                    }
                }
            },

            usuario: {
                required: true,

            }

        },
        messages: {
            cat: {
                required: "El nombre de Categoría es un campo obligatorio.",
                minlength: "El nombre de Categoría debe de tener mas de 4 caracteres.",
                remote: "El nombre no debe ser Repetido."
            },
            usuario: {
                required: "Se debe seleccionar un ejercicio.",
                // remote: "El nombre no debe ser Repetido."
            },
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