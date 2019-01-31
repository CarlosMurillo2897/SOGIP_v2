$(document).ready(function () {

    cargaF();

    $('#funcionario').change(function () {
        cargaF();
    });

    $('#atleta').change(function () {
        cargaA();
    });


   /* var today = new Date();
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
    });*/

});


function cargaF() {
    $.getJSON("/ExpedientesFisicos/getUsuariosF", null, function (data) {
        $("#usuariosDropdown").empty(); // Remove all <option> child tags.
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
        $("#usuariosDropdown").empty(); // Remove all <option> child tags.
        $.each(data, function (index, item) { // Iterates through a collection
            $("#usuariosDropdown").append(
                $('<option></option>')
                    .text(item.cedNomCompleto)
                    .val(item.idAtleta)
            );
        });
    });

}

$(document).on('click', '#close-preview', function () {
    $('.image-preview').popover('hide');

    // Hover before close the preview
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

    // Create the close button
    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;'
    });
    closebtn.attr("class", "close pull-right");

    // Set the popover default content
    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>Vista anticipada</strong>" + $(closebtn)[0].outerHTML,
        content: "No hay imágen disponible.",
        placement: 'bottom'
    });

    // Clear event
    $('.image-preview-clear').click(function () {
        clear();
    });

    // Create the preview image
    $(".image-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();

        // Set preview image into the popover data-content
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

function uploadImage() {
    var archivo = $('#excelfile')[0].files[0];

    if (archivo.size >= 20000000 && !confirm('\n¡Cuidado! Estás intentando subir un archivo de más de 20MB.\n\n ¿Estás seguro de querer subir este archivo?\n\n')) {
        clear();
        archivo = null;
        throw error;
    }

    var ext = archivo.name.split('.').pop();
    if (ext != 'xls' && ext != 'xlsx' && ext != null) {
        if (!confirm('\nEste archivo no parece ser de Excel, recuerde que el sistema solo leerá archivos\n\ntipo Excel e intentar subir cualquier otro archivo puede ser peligroso. ¿Continuar?\n\n')) {
            clear();
            throw error;
        }
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

            // Table body.
            $.each(list, function (i) {
                var date = new Date(parseInt(list[i].Fecha_Nacimiento.substr(6)));
                var sexo = list[i].Sexo == true ? 'Masculino' : 'Femenino';
                var nom2 = list[i].Nombre2 == null ? '' : list[i].Nombre2;
                var ap2 = list[i].Apellido2 == null ? '' : list[i].Apellido2;
                var email = list[i].Email == null ? '' : list[i].Email;

                $body.append(
                    '<tr>' +
                    '<td>' + list[i].Cedula + '</td>' +
                    '<td>' + list[i].Nombre1 + '</td>' +
                    '<td>' + nom2 + '</td>' +
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
    tr.find('td:eq(0)').text($('#ced').val());
    tr.find('td:eq(1)').text($('#nom1').val());
    tr.find('td:eq(2)').text($('#nom2').val());
    tr.find('td:eq(3)').text($('#apel1').val());
    tr.find('td:eq(4)').text($('#apel2').val());
    tr.find('td:eq(5)').text($('#email').val());
    tr.find('td:eq(6)').text($("#dtp").data('datepicker').getFormattedDate('dd/mm/yyyy'));
    tr.find('td:eq(7)').text($('#sexo').val());
}

function registrar() {
    pop(true);
    var array = [];

    $('tbody tr').each(function () {
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
            Sexo: tr.find('td:eq(7)').text() == 'Masculino' ? true : false,
            Estado: true,
            Fecha_Expiracion: new Date()
        });

    });

    var datos = {
        'users': array
    };

    $.ajax({
        url: '/UsersAdmin/CrearMasivo',
        dataType: 'JSON',
        type: 'POST',
        data: JSON.stringify(datos), //agregar el campo para el id de la rutina
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            $('#UpdatePanel').remove('.table');
            contenidoPop(1);
        },
        error: function (result) {
            contenidoPop(0);
        }
    });
}

function pop(status) {
    document.getElementById('box').style.display = (status) ? "block" : "none";
    if (!status) {
        contenidoPop(2);
    }
}

function contenidoPop(content) {
    if (content == 0) {
        $('#box img').attr('src', '/Content/Imagenes/cancelar.png');
        $('#box img').attr('alt', 'Incorrecto');
        $('#box h1').text('¡Error encontrado!');
    }
    if (content == 1) {
        $('#box img').attr('src', '/Content/Imagenes/comprobado.png');
        $('#box img').attr('alt', 'Correcto');
        $('#box h1').text('Finalizado');
    }
    if (content == 2) {
        $('#box img').attr('src', '/Content/Imagenes/loader.gif');
        $('#box img').attr('alt', 'Cargando');
        $('#box h1').text('Cargando..');
    }

}