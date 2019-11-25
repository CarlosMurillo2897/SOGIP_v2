var ced = null;
function eliminar() {
    var des = $('#example').DataTable();
    des.destroy();
}
function eliminar2() {
    var des = $('#archivos').DataTable();
    des.destroy();
}
function llenarTablaUsuarios() {
    $('#archivos').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando _START_ de _END_, de un total de _TOTAL_ registros.",
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
            "url": "/SOGIP/CodigoQR/ObtenerUsuarios",
            "type": "GET",
            "dataSrc": "",
        },
        columns: [
            { data: "Nombre" },
            { data: "Tipo" },
            {
                data: "Id",
                "render": function (data, type, row) {
                    var Id = row.Id;
                    var action = '';
                    if (row.Nombre.split(' ')[0] === '000000000' && row.Tipo === 'INGRESO MASIVO') {
                        Id = 0;
                    }
                    return "<a class='btn btn-danger' id='boton_" + row.Id + "' onclick='EliminarArchivo(" + Id + ")' style='padding: 2px 6px; margin: 2px;'>" +
                        "<text class='hidden-xs'>Eliminar </text>" +
                        "<span class='glyphicon glyphicon-minus-sign'></span>" +
                        "</a>" +
                        "<a class='btn btn-info' href='/UsersAdmin/Download?archivoId=" + row.Id + "' style='padding: 2px 6px; margin: 2px;'>" +
                        "<text class='hidden-xs'>Descargar </text>" +
                        "<span class='glyphicon glyphicon-download'></span>" +
                        "</a>";
                }
            }
        ]
    });
}
function llenarTablaMaquinas() {
    $('#archivos').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando _START_ de _END_, de un total de _TOTAL_ registros.",
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
            "url": "/SOGIP/CodigoQR/ObtenerMaquinas",
            "type": "GET",
            "dataSrc": "",
        },
        columns: [
            { data: "Nombre" },
            { data: "Tipo" },
            {
                data: "Id",
                "render": function (data, type, row) {
                    var Id = row.Id;
                    var action = '';
                    if (row.Nombre.split(' ')[0] === '000000000' && row.Tipo === 'INGRESO MASIVO') {
                        Id = 0;
                    }
                    return "<a class='btn btn-danger' id='boton_" + row.Id + "' onclick='EliminarArchivo(" + Id + ")' style='padding: 2px 6px; margin: 2px;'>" +
                        "<text class='hidden-xs'>Eliminar </text>" +
                        "<span class='glyphicon glyphicon-minus-sign'></span>" +
                        "</a>" +
                        "<a class='btn btn-info' href='/UsersAdmin/Download?archivoId=" + row.Id + "' style='padding: 2px 6px; margin: 2px;'>" +
                        "<text class='hidden-xs'>Descargar </text>" +
                        "<span class='glyphicon glyphicon-download'></span>" +
                        "</a>";
                }
            }
        ]
    });
}
function llenarTablaEjercicio() {
    $('#archivos').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando _START_ de _END_, de un total de _TOTAL_ registros.",
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
            "url": "/SOGIP/CodigoQR/ObtenerEjercicio",
            "type": "GET",
            "dataSrc": "",
        },
        columns: [
            { data: "Nombre" },
            { data: "Tipo" },
            {
                data: "Id",
                "render": function (data, type, row) {
                    var Id = row.Id;
                    var action = '';
                    if (row.Nombre.split(' ')[0] === '000000000' && row.Tipo === 'INGRESO MASIVO') {
                        Id = 0;
                    }
                    return "<a class='btn btn-danger' id='boton_" + row.Id + "' onclick='EliminarDes(" + Id + ")' style='padding: 2px 6px; margin: 2px;'>" +
                        "<text class='hidden-xs'>Eliminar </text>" +
                        "<span class='glyphicon glyphicon-minus-sign'></span>" +
                        "</a>" +
                        "<a class='btn btn-info' onclick='EnviarDes(" + Id + ")' style='padding: 2px 6px; margin: 2px;'>" +
                        "<text class='hidden-xs'>Ver </text>" +
                        "<span class='glyphicon glyphicon-sd-video'></span>" +
                        "</a>";
                }
            }
        ]
    });
}
function EnviarDes(id) {
    var tr = $('#boton_' + id).closest('tr');
    if (id === 0) {
        alert('Archivo predeterminado, no se puede ver video.');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/SOGIP/CodigoQR/Descripcion",
        data: { id: id },
        success: function (data) {
            $('#one').attr('src', data);
            $('#modal').modal('show');

        },
        error: function (data) {
            alert("¡Error!");
        }
    });
}
function EliminarDes(id) {
    var tr = $('#boton_' + id).closest('tr');
    if (id === 0) {
        alert('Archivo predeterminado, no se puede borrar.');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/SOGIP/CodigoQR/EliminarEjer",
        data: { id: id },
        success: function (data) {
            var table = $('#archivos').DataTable().row(tr).remove().draw();
        },
        error: function (data) {
            alert("¡Error!");
        }
    });
}
function EliminarArchivo(id) {
    var tr = $('#boton_' + id).closest('tr');
    if (id === 0) {
        alert('Archivo predeterminado, no se puede borrar.');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/SOGIP/CodigoQR/EliminarArchivo",
        data: { id: id },
        success: function (data) {
            var table = $('#archivos').DataTable().row(tr).remove().draw();
        },
        error: function (data) {
            alert("¡Error!");
        }
    });
}


function generarQR(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/CodigoQR/generarQr',
        data: data,
        success: function (data) {
            $('#archivos').DataTable().ajax.reload(null, false);
            $('#nuevo').modal('hide');
            $('#obj').val('');
            eliminar();
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function generarQR2(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/CodigoQR/generarQr2',
        data: data,
        success: function (data) {
            $('#archivos').DataTable().ajax.reload(null, false);
            $('#nuevo').modal('hide');
            $('#obj').val('');
            eliminar();
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function generarEjercicio(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/CodigoQR/generarEjercicio',
        data: data,
        success: function (data) {
            $('#archivos').DataTable().ajax.reload(null, false);
            $('#nuevo').modal('hide');
            $('#obj').val('');
            eliminar();
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function enviar() {
    var data = null;
    if ($('#label').is(':visible')) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: '/SOGIP/CodigoQR/obtenerIdMaq',
            data: { "n": ced },
            success: function (data) {
                ced = data.Id;
                var data = null;
                {
                    data = {
                        txtQRCode: $('#ref').prop('href'),
                        id: ced
                    }
                    generarQR(data);
                }
            },
            error: function () {
                alert("Fallo");
            }
        })
    } else {
        if ($('#label2').is(':visible')) {
            $.ajax({
                type: "POST",
                dataType: "JSON",
                url: '/SOGIP/CodigoQR/obtenerIdEjer',
                data: { "n": ced },
                success: function (data) {
                    ced = data.Id;
                    var data = null;
                    {
                        data = {
                            txtQRCode: $('#obj').val().trim(),
                            id: ced
                        }
                        generarEjercicio(data);
                    }
                },
                error: function () {
                    alert("Fallo");
                }
            })

        } else {
            data = {
                id: ced
            }
            generarQR2(data);
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
    llenarTablaMaquinas();
    $('#usu1').click(function () {
        eliminar2();
        llenarTablaUsuarios();
    })
    $('#maq1').click(function () {
        eliminar2();
        llenarTablaMaquinas();
    })
    $('#ejer1').click(function () {
        eliminar2();
        llenarTablaEjercicio();
    })

    $('#btnNuevo').click(function () {
        $("#radio_1").prop("checked", true);
        $('#nuevo').modal('show');
        $('#label').css("display", "block");
        $('#label2').css("display", "none");
        $('#obj').css("display", "none");
        $('#usuario').val('');
        $('#usuario1').val('');
        $('#usuario').css("display", "block");
        $('#usuario1').css("display", "none");
        maquinas();

        $('#maq').click(function () {
            $('#label').css("display", "block");
            $('#label2').css("display", "none");
            $('#obj').css("display", "none");
            $('#usuario').val('');
            $('#usuario1').val('');
            $('#usuario').css("display", "block");
            $('#usuario1').css("display", "none");
            eliminar();
            maquinas();
        })
        $('#ejer').click(function () {
            $('#label2').css("display", "block");
            $('#label').css("display", "none");
            $('#obj').css("display", "block");
            $('#usuario').val('');
            $('#usuario1').val('');
            $('#usuario').css("display", "none");
            $('#usuario1').css("display", "block");
            eliminar();
            ejercicios();

        })

    })

    function maquinas() {
        $('#example').DataTable().destroy();
        $('#example').remove();
        $('#tablePrin').show();
        var tabla = $('<table/>', { id: 'example', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Acción</th><th>Nombre</th></tr></thead>');
        $('#tablePrin').append(tabla);
        table = $('#example').DataTable({
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
                "url": '/SOGIP/CodigoQR/GetMaquinas2',
                "type": "GET",
                "dataSrc": ""
            },
            columns: [
                { data: "Accion" },
                { data: "Nombre" }
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

    function ejercicios() {
        $('#example').DataTable().destroy();
        $('#example').remove();
        $('#tablePrin').show();
        var tabla = $('<table/>', { id: 'example', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Acción</th><th>Nombre</th></tr></thead>');
        $('#tablePrin').append(tabla);
        table = $('#example').DataTable({
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
                "url": '/SOGIP/CodigoQR/GetMaquinas3',
                "type": "GET",
                "dataSrc": ""
            },
            columns: [
                { data: "Accion" },
                { data: "Nombre" }
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
    $('#cerrar').click(function () {
        eliminar();
        $('#nuevo').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    })
    $('#cerrar1').click(function () {
        eliminar();
        $('#nuevo').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    })
    $('#botón').click(function () {
        $('#Tabla_Prin').show();

    });
    $('#tablePrin').on('click', 'td.select-checkbox', function () {
        var td = $(this);
        var tr = td.closest('tr');

        $('#usuario').val(tr.find('td:eq(1)').text());
        $('#usuario1').val(tr.find('td:eq(1)').text());
        ced = tr.find('td:eq(1)').text();
        $('#Tabla_Prin').hide();
    });
    $("#forcat").validate({

        rules: {
            obj: {

                required: true

            },

            usuario: {
                required: true,
                remote: {
                    url: '/SOGIP/CodigoQR/RepetidoMaquina',
                    type: 'GET',
                    data: {
                        nombre: function () {
                            return $('#usuario').val();
                        }
                    }
                }
            },
            usuario1: {
                required: true,
                remote: {
                    url: '/SOGIP/CodigoQR/RepetidoEjercicio',
                    type: 'GET',
                    data: {
                        nombre: function () {
                            return $('#usuario1').val();
                        }
                    }
                }
            }



        },
        messages: {
            obj: {
                required: "El URL es un campo obligatorio."

            },
            usuario: {
                required: "Se debe seleccionar de la tabla.",
                remote: "Ya existe una Maquina con QR."

            },
            usuario1: {
                required: "Se debe seleccionar de la tabla.",
                remote: "Ya existe este Ejercicio con Video."

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