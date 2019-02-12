$(document).ready(function () {
    clear();
    tablaUsuarios();
    $('#usuario').val('');
    fillDT();
    
});

$(document).on('click', '#close-preview', function () {
    $('.image-preview').popover('hide');

    $('.image-preview').hover(
        function () {
            $('.image-preview').popover('show');
        },
        function () {
            $('.image-preview').popover('hide');
        }
    );
});

$(function () {
    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;'
    });
    closebtn.attr("class", "close pull-right");

    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>Vista anticipada</strong>" + $(closebtn)[0].outerHTML,
        content: "No hay imágen disponible.",
        placement: 'bottom'
    });

    $('.image-preview-clear').click(function () { clear(); });

    $(".image-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();

        reader.onload = function (e) {
            $(".image-preview-input-title").text("Buscar");
            $("#upload").css("display", "none");
            $(".image-preview-clear").show();
            $(".image-preview-filename").val(file.name);
            img.attr('src', e.target.result);
            $(".image-preview").attr("data-content", $(img)[0].outerHTML).popover("show");
        };
        reader.readAsDataURL(file);
    });
});

function clear() {
    $('.image-preview').attr("data-content", "").popover('hide');
    $('.image-preview-filename').val("");
    $('.image-preview-clear').hide();
    $('.image-preview-input input:file').val("");
    $(".image-preview-input-title").text("Buscar");
    $("#upload").css("display", "");
}

function uploadImage() {
    var archivo = $('#excelfile')[0].files[0];

    if (archivo.size >= 20000000 && !confirm('\n¡Cuidado! Estás intentando subir un archivo de más de 20MB.\n\n ¿Estás seguro de querer subir este archivo?\n\n')) {
        clear();
        archivo = null;
        throw error;
    }

    var data = new FormData();
    data.append('excelfile', archivo);

    pop(true);
    $.ajax({
        type: "POST",
        url: "/UsersAdmin/Import",
        data: data,
        contentType: false,
        processData: false,
        success: function (list) {
            clear();
            var $table = $('<table/>').addClass('table table-responsive table-striped table-bordered');
            var $header = $('<thead/>').html('<tr>><th>Cédula</th><th>1° Nombre</th><th>2° Nombre</th><th>1° Apellido</th><th>2° Apellido</th>' +
                '<th>E-mail</th><th>Nacimiento</th><th>Sexo</th><th style="text-align: center;"><span class="glyphicon glyphicon-cog"></span></th></tr>'
            );

            $table.append($header);
            var $body = $('<tbody/>');

            $.each(list, function (i) {
                var date = new Date(parseInt(list[i].Fecha_Nacimiento.substr(6)));
                var sexo = list[i].Sexo === true ? 'Masculino' : 'Femenino';
                var ap2 = list[i].Apellido2 === null ? '' : list[i].Apellido2;
                var email = list[i].Email === null ? '' : list[i].Email;

                $body.append(
                    '<tr id="' + i + '">' +
                    '<td>' + list[i].Cedula + '</td>' +
                    '<td>' + list[i].Nombre1 + '</td>' +
                    '<td>' + list[i].Nombre2 + '</td>' +
                    '<td>' + list[i].Apellido1 + '</td>' +
                    '<td>' + ap2 + '</td>' +
                    '<td>' + email + '</td>' +
                    '<td>' + date.toLocaleDateString('en-GB') + '</td>' +
                    '<td>' + sexo + '</td>' +
                    '<td style="text-align: center;">' +
                    '<span class="glyphicon glyphicon-pencil invent" data-toggle="modal" onclick="cargar(this)" data-target="#myModal"></span>' +
                    '</td></tr>');
            });
            $table.append($body);
            $('#UpdatePanel').html($table);
            $('#registrar').html('<a class="btn btn-block btn-success" onclick="registrar()">Registrar</a>');
            contenidoPop(1);
        },
        error: function (data) {
            contenidoPop(0);
        }
    });
}

function pop(status) {
    document.getElementById('box').style.display = status ? "block" : "none";
    if (!status) {
        contenidoPop(2);
    }
}

function contenidoPop(content) {
    if (content === 0) {
        $('#box img').attr('src', '/Content/Imagenes/cancelar.png');
        $('#box img').attr('alt', 'Incorrecto');
        $('#box h1').text('¡Error encontrado!');
    }
    if (content === 1) {
        $('#box img').attr('src', '/Content/Imagenes/comprobado.png');
        $('#box img').attr('alt', 'Correcto');
        $('#box h1').text('Finalizado');
    }
    if (content === 2) {
        $('#box img').attr('src', '/Content/Imagenes/loader.gif');
        $('#box img').attr('alt', 'Cargando');
        $('#box h1').text('Cargando..');
    }
}

function fillDT() {
    var isE = $('#example').DataTable();
    isE.destroy(); //es mejor destruirla para poder incializarla
    dataTable();
    cargarTipos();
}

function dataTable() {
    var table;
    var dataSet = [];

    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: "/ExpedientesFisicos/ObtenerUsuarios",
        success: function (data) {
            $.each(data, function (i, v) {
                dataSet.push(["", v.Cedula, v.Nombre1, v.Apellido1, v.Apellido2]);
            });
            table = $('#example').DataTable({
                // "aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],
                // "iDisplayLength": 5,
                "language": {
                    "lengthMenu": "Mostrando _MENU_ resultados por página.",
                    "zeroRecords": "No se han encontrado resultados.",
                    "info": "Mostrando página _PAGE_ de _PAGES_.",
                    "infoEmpty": "No hay datos para mostrar",
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
                            0: "Seleccione un cuadrado en la columna 'Acción'.",
                            1: "1 registro seleccionado."
                        }
                    }
                },
                data: dataSet,
                columns: [
                    { title: "Acción" },
                    { title: "Cédula" },
                    { title: "Nombre" },
                    { title: "1° Apellido" },
                    { title: "2° Apellido" }
                ],
                'columnDefs': [{
                    orderable: false,
                    className: 'select-checkbox',
                    targets: [0]
                }],
                'select': {
                    'style': 'os',
                    'selector': 'td:first-child'
                }
            });
        },
        error: function (error) {
            alert("Fallo");
        }
    });

    $('#example').on('click', 'td.select-checkbox', function () {
        var td = $(this);
        var tr = td.closest('tr');
        $('#usuario').val(tr.find('td:eq(1)').text() + ' ' + tr.find('td:eq(2)').text() + ' ' + tr.find('td:eq(3)').text() + ' ' + tr.find('td:eq(4)').text());
        manipularDT();
        $('#botón').attr('onclick', 'manipularDT();');
        if (tr.hasClass('selected')) {
            $('#usuario').val('');
        }
    });

}

function manipularDT() {
    $('#Tabla_Usuarios').slideToggle(400, function () {
        if ($('#Tabla_Usuarios').is(':visible')) {
            $('#icono').removeClass('glyphicon-search').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
            $('#texto').html('Cerrar lista ');
        }
        else {
            $('#icono').removeClass('glyphicon-search').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
            $('#texto').html('Desplegar lista ');
        }
    });
}

function cargarTipos() {

    $.ajax({
        type: "POST",
        data: { role: $('#Role').val() },
        url: "/ExpedientesFisicos/ObtenerTipos",
        success: function (data) {
            $('#select').append('<option value="0">-- Seleccionar --</option>');
            $.each(data, function (i, v) {
                $('#select').append('<option value="' + v.TipoId + '">' + v.Nombre + '</option>');
            });
        },
        error: function (error) {
            alert("Fallo");
        }
    });
}

function subirArchivo() {
    var select = $("#select option:selected").val();
    var id = $('#usuario').val().split(' ')[0];
    var archivo = $('#archivo')[0].files[0];

    if (archivo !== undefined && archivo.size >= 20000000 && !confirm('\n¡Cuidado! Estás intentando subir un archivo de más de 20MB.\n\n ¿Estás seguro de querer subir este archivo?\n\n')) {
        clear();
        archivo = undefined;
    }

    var ext = archivo.name.split('.').pop();

    if (archivo !== undefined && ext === 'dsa' || ext === 'mp3') {
        alert('Archivos de este tipo son peligrosos para nuestro sistema.');
        clear();
        archivo = undefined;
    }

    if (select === '0' || id === '' || archivo === undefined) {
        alert('Error, faltan datos por completar.');
    }

    else {
        var data = new FormData();
        data.append('archivo', archivo);
        data.append('id', id);
        data.append('select', select);
        data.append('ArchivoId', 0);

        $.ajax({
            type: "POST",
            url: "/ExpedientesFisicos/SubirArchivo",
            data: data,
            contentType: false,
            processData: false,
            success: function (data) {
                var table = $('#archivos').DataTable();
                table.row.add(["", data.Nombre, data.Tipo.Nombre, data.Usuario.Nombre1 + " " + data.Usuario.Nombre2 + " " + data.Usuario.Apellido1 + " " + data.Usuario.Apellido2, data.ArchivoId]).draw();
                clear();
                $('#usuario').val('');
                $('#select').val(0);
            },
            error: function (data) {
                alert(data.Nombre + 'Error en los datos.');
                var table = $('#archivos').DataTable();
                table.ajax.reload();
            }
        });
    }
}

function tablaUsuarios() {
    $('#archivos').DataTable({
            // "aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],
            // "iDisplayLength": 5,
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
                },
                "select": {
                    "rows": {
                        _: "%d registros seleccionados.",
                        0: "Seleccione un cuadrado en la columna 'Acción'.",
                        1: "1 registro seleccionado."
                    }
                }
        }/*,
        columns: [
            { title: "Nombre" },
            { title: "Tipo" },
            { title: "Usuario" },
            { title: "Acción" }
        ]*/
    });

    //$('#archivos').on('click', 'td.select-checkbox', function () {
    //    var td = $(this);
    //    var tr = td.closest('tr');
    //    var table = $('#archivos').DataTable();
    //    var data = table.row(tr).data();
    //    alert(data[4]);
        
        //$('#archivo ').val(tr.find('td:eq(1)').text() + ' ' + tr.find('td:eq(2)').text() + ' ' + tr.find('td:eq(3)').text() + ' ' + tr.find('td:eq(4)').text());
        //manipularDT();

        //if (tr.hasClass('selected')) {
        //    $('#usuario').val('');
        //}

    //});
}

function EliminarArchivo(id) {
    var tr = $('#boton_' + id).closest('tr');

    $.ajax({
        type: "POST",
        url: "/ExpedientesFisicos/EliminarArchivo",
        data: { id: id },
        success: function (data) {
            var table = $('#archivos').DataTable().row(tr).remove().draw();
        },
        error: function(data){
            alert("¡Error!");
        }
    });
}