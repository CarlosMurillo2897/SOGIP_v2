$(document).ready(function () {

    clear();

    cargaF();

    $('#funcionario').change(function () {
        cargaF();
    })

    $('#atleta').change(function () {
        cargaA();
    })

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
            daysOfWeekDisabled: false,
        });
    });

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
        style: 'font-size: initial;',
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
        }
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
    }

    var ext = archivo.name.split('.').pop();
    if (ext != 'xls' && ext != 'xlsx' && ext != null){
        if (!confirm('\nEste archivo no parece ser de Excel, recuerde que el sistema solo leerá archivos\n\ntipo Excel e intentar subir cualquier otro archivo puede ser peligroso. ¿Continuar?\n\n')) {
            clear();
        }
    }

    var data = new FormData();
    data.append('excelfile', archivo);

    $.ajax({
        type: "POST",
        url: "/UsersAdmin/Import",
        data: data,
        contentType: false,
        processData: false,
        success: function (list) {
            clear();
            var $table = $('<table/>').addClass('table table-responsive table-striped table-bordered');
            var $header = $('<thead/>').html('<tr>><th>Cedula</th><th>Nombre1</th><th>Nombre2</th><th>Apellido1</th><th>Apellido2</th>' +
                                             '<th>E-mail</th><th>Sexo</th><th>Nacimiento</th></tr>'
            );
            debugger
            $table.append($header);
            var $body = $('<tbody/>');

            // Table body.
            $.each(list, function (i) {
                var date = new Date(parseInt(list[i].Fecha_Nacimiento.substr(6)));
                var sexo = (list[i].Sexo == true) ? 'Masculino' : 'Femenino';
                $body.append(
                    "<tr>" +
                    "<td>" + list[i].Cedula + "</td>" +
                    "<td>" + list[i].Nombre1 + "</td>" +
                    "<td>" + list[i].Nombre2 + "</td>" +
                    "<td>" + list[i].Apellido1 + "</td>" +
                    "<td>" + list[i].Apellido2 + "</td>" +
                    "<td>" + list[i].Email + "</td>" +
                    "<td>" + sexo + "</td>" +
                    "<td>" + date.toLocaleDateString("en-GB") + "</td>" +
                    "<td><a href='#' class='btn btn-primary' data-toggle='modal' data-target='#myModal'>Open</a></td>" +
                    "</tr>");
            })


            $table.append($body);

            // Table footer(for paging content).

            $('#UpdatePanel').html($table);

        },
        error: function (data) {
            alert(data.Message);
        }
    });

}

function charge(x) {
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