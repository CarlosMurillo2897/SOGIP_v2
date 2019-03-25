$(document).ready(function () {
    var selected, role, value;
    $('#usuario').val('');
    clear();

    cargarUsuarios();

    var today = new Date();
    var min = new Date();
    var max = new Date();
    max.setFullYear(today.getFullYear() - 80);
    min.setFullYear(today.getFullYear() - 10);

    $(function () {
        $("#dtp").datepicker({
            format: 'dd/mm/yyyy',
            defaultViewDate: max,
            startDate: max,
            endDate: min,
            daysOfWeekDisabled: false
        });
    });

    $('#tosh').on('click', function ()
    {
        $('tr.supr').remove();
    });
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
            $("#submit").css("display", "");
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
    $("#submit").css("display", "none");
}

function cargarUsuarios() {

    var table = $('<table/>', {
        id: 'Entidades',
        class: 'table table-striped table-bordered dt-responsive nowrap',
        width: '100%'
    }).append('<thead><tr><th>Entidad</th><th>Cédula</th><th>Nombre</th><th>Rol</th><th>Acción</th></tr></thead>');

    $('#Tabla_Usuarios').append(table);

    table = $('#Entidades').DataTable({
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
        },
        "ajax": {
            "url": "/UsersAdmin/ObtenerUsuarios",
            "type": "GET",
            "dataSrc": ""
        },
        columns: [
            { data: "Entidad" },
            { data: "Cédula" },
            { data: "Nombre" },
            { data: "Rol" },
            {
                data: function (data, type, dataToSet) {
                    var opciones = "";
                    if (data.Categoria.length !== 0) {
                        $.each(data.Categoria, function (i) {
                            opciones = opciones + "<option value='" + data.Categoria[i].CategoriaId + "'>" + data.Categoria[i].Descripcion + "</option>";
                        });
                    } else {
                        opciones = "<option value='1'>SELECCIONADA</option>";
                    }
                    return '<select style="display: inline-block; width: 200px;" id="selectDT_' + data.Cédula + '_' + data.Entidad + '" class="selectDT form-control" ><option value="0">-- Cambiar --</option>' + opciones + '</select>';
                }
            }
        ]
    });

    $('#Entidades').on('change', '.selectDT', function () {
        value = $(this).val();

        value === '0' ? $('#usuario').val('') : $('#usuario').val($(this).attr('id').split('_')[2] + " - " + $(this).attr('id').split('_')[1]);
        role = $(this).closest('tr').find('td:eq(3)').text();
        
        $('.selectDT').val(0);
        $(this).val(value);

        $('#botón').attr('onclick', 'manipularDT();');
        manipularDT();

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


function uploadImage() {
    var archivo = $('#excelfile')[0].files[0];

    if (archivo.size >= 20000000 && !confirm('\n¡Cuidado! Estás intentando subir un archivo de más de 20MB.\n\n ¿Estás seguro de querer subir este archivo?\n\n')) {
        clear();
        archivo = null;
        throw error;
    }

    var ext = archivo.name.split('.').pop();
    if (ext !== 'xls' && ext !== 'xlsx' && ext !== null) {
        alert('Estas intentando subir un archivo no aceptado por el sistema, solo aceptamos archivos tipo Excel. ');
        clear();
        throw error;
    }

    var data = new FormData();
    data.append('excelfile', archivo);


    pop(true);

    $('#UpdatePanel').empty();
    $('#registrar').empty();

    $.ajax({
        type: "POST",
        url: "/UsersAdmin/Import",
        data: data,
        contentType: false,
        processData: false,
        success: function (list) {
            clear();
            var $table = $('<table/>', {
                class: 'table table-striped table-bordered',
                id: 'UsuariosExcel'
            }).append('<thead><tr><th>Cédula</th><th>1° Nombre</th><th>2° Nombre</th><th>1° Apellido</th><th>2° Apellido</th>' +
                '<th>E-mail</th><th>Nacimiento</th><th>Sexo</th><th style="text-align: center;"><span class="glyphicon glyphicon-cog"></span></th></tr></thead>'
            );

            var $body = $('<tbody/>');

            $.each(list, function (i) {
                var date = new Date(parseInt(list[i].Fecha_Nacimiento.substr(6)));
                var sexo = list[i].Sexo === true ? 'Masculino' : 'Femenino';
                var ap2 = list[i].Apellido2 === null ? '' : list[i].Apellido2;
                var email = list[i].Email === null ? '' : list[i].Email;

                var p, clase;
                if (list[i].Message.length !== 0) {
                    p = "<p style='color: red;'><br />";
                    $.each(list[i].Message, function (x) {
                        p = p + "\u23FA Error n°" + (x + 1) + ": " + list[i].Message[x] + "<br /><br />";
                        clase = "btn btn-danger";
                    });
                }
                
                else {
                    p = "<p style='color: green;'> \u23FA Todo en orden.";
                    clase = "btn btn-success";
                }
                p = p + "</p>";
                
                $body.append(
                    '<tr id="' + i + '">' +
                    '<td>' +
                    '<a tabindex="0" class="' + clase + '" title="<strong>Estado</strong> (para cerrar oprima fuera del cuadro)." data-trigger="focus" data-html="true" data-container="body" data-toggle="popover" data-placement="bottom" data-content="' + p + '">' + list[i].Cedula + '</a>' +
                    '</td>' +
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
            $('#UpdatePanel').append($table);
            $('#registrar').html('<a id="reg" class="btn btn-block btn-success" onclick="registrar()">Registrar</a>');
            contenidoPop(1);

            var title = '<strong>Estado</strong>';
            $('#btn_1').attr('data-content', title);

            $('[data-toggle="popover"]').popover();

        },
        error: function (data) {
            contenidoPop(0);
        }
    });
}

function cargar(x) {
    var tr = $(x).closest('tr');
    $('#ced').val(tr.find('td:eq(0)').text());
    $('#nom1').val(tr.find('td:eq(1)').text());
    $('#nom2').val(tr.find('td:eq(2)').text());
    $('#apel1').val(tr.find('td:eq(3)').text());
    $('#apel2').val(tr.find('td:eq(4)').text());
    $('#email').val(tr.find('td:eq(5)').text());
    $("#dtp").datepicker("update", tr.find('td:eq(6)').text());
    $('#sexo').val(tr.find('td:eq(7)').text());
    $('#hidden').val(tr.attr('id'));
}

function actualizar() {
    var tr = $('#' + $('#hidden').val());
    tr.find('td:eq(0)').text($('#ced').val().toUpperCase());
    tr.find('td:eq(1)').text($('#nom1').val().toUpperCase());
    tr.find('td:eq(2)').text($('#nom2').val().toUpperCase());
    tr.find('td:eq(3)').text($('#apel1').val().toUpperCase());
    tr.find('td:eq(4)').text($('#apel2').val().toUpperCase());
    tr.find('td:eq(5)').text($('#email').val());
    tr.find('td:eq(6)').text($("#dtp").data('datepicker').getFormattedDate('dd/mm/yyyy'));
    tr.find('td:eq(7)').text($('#sexo').val());
}

function registrar() {
    if ($('#usuario').val() === '') {
        alert('Favor seleccione un Usuario de la tabla.');
    }
    else {
        pop(true);
        var array = [];

        $('#UsuariosExcel tbody tr .btn-success').each(function () {
            var tr = $(this).closest('tr');
            array.push({
                Cedula: tr.find('td:eq(0)').text(),
                UserName: tr.find('td:eq(0)').text(),
                Nombre1: tr.find('td:eq(1)').text(),
                Nombre2: tr.find('td:eq(2)').text(),
                Apellido1: tr.find('td:eq(3)').text(),
                Apellido2: tr.find('td:eq(4)').text(),
                Email: tr.find('td:eq(5)').text(),
                Fecha_Nacimiento: tr.find('td:eq(6)').text(),
                Sexo: tr.find('td:eq(7)').text() === 'Masculino' ? true : false,
                Estado: true,
                Fecha_Expiracion: new Date()
            });
        });
        console.log(array);

        var datos = {
            'users': array,
            'usuario': $('#usuario').val().split("- ")[1],
            'value': value,
            'rol': role
        };

        $.ajax({
            url: '/UsersAdmin/CrearMasivo',
            dataType: 'JSON',
            type: 'POST',
            data: JSON.stringify(datos), //agregar el campo para el id de la rutina
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#UsuariosExcel tbody td .btn-success').closest('tr').remove();
                $('#Usuario').val('');
                contenidoPop(1);
            },
            error: function (data) {
                contenidoPop(0);
            }
        });
    }
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