$(document).ready(function () {
    var panels = $('.user-infos');
    var panelsButton = $('.dropdown-user');
    panels.hide();

    //Click dropdown
    panelsButton.click(function () {
        //get data-for attribute
        var dataFor = $(this).attr('data-for');
        var idFor = $(dataFor);

        //current button
        var currentButton = $(this);
        var id = dataFor.slice(1);

        if (idFor.is(':visible')) {

            let index, table = document.getElementById(id);

            while (table.rows.length > 8) {
                table.deleteRow(8);
            }

        }
        else {

            $.get("/UsersAdmin/ArchivosUsuario", { usuarioId: id }, function (data) {
                $.each(data, function (index, row) {
                    index = index + 1;

                    $("#tabla1-" + id).append('<dt>Nombre: </dt><dd>' + index + '. ' + row.Nombre + '</dd>' +
                        '<dt>Tipo: </dt><dd>' + row.Tipo + '</dd><dd>' +
                        '<form action="/UsersAdmin/Download" method="post" enctype="multipart/form-data">' +
                        '<input type="hidden" name="Documento" value="' + row.ArchivoId + '" />' +
                        '<button class="btn btn-sm btn-success" type="submit" data-toggle="tooltip" data-original-title="Descargar este documento.">' +
                        'Descargar <i class="glyphicon glyphicon-download"></i>' +
                        '</button></form>' +
                        '</dd>');

                    $("#tabla2-" + id).append('<tr><td>' + index + '. ' + row.Nombre + '</td><td>' + row.Tipo + '</td><td>' +
                        '<form action="/UsersAdmin/Download" method="post" enctype="multipart/form-data">' +
                        '<input type="hidden" name="Documento" value="'+row.ArchivoId+'" />' +
                        '<button class="btn btn-sm btn-success" type="submit" data-toggle="tooltip" data-original-title="Descargar este documento.">' +
                        'Descargar <i class="glyphicon glyphicon-download"></i>' +
                        '</button></form>' +
                        '</td></tr>');
                });

            });

        }
        
        idFor.slideToggle(400, function () {
            //Completed slidetoggle
            if (idFor.is(':visible')) {
                currentButton.html('<i class="glyphicon glyphicon-chevron-up text-muted"></i>');
            }
            else {
                currentButton.html('<i class="glyphicon glyphicon-chevron-down text-muted"></i>');
            }
        })
    });


    $('[data-toggle="tooltip"]').tooltip();

});

function dis(id, DI) {

    var clase = document.getElementById(DI).className;

    var status = (clase == 'btn btn-sm btn-danger')? true : false;

    var datos = {
        'usuarioId': id,
        'estado': status
    }

    $.ajax({
        url: '/UsersAdmin/InhabilitarUsuario',
        dataType: 'JSON',
        type: 'POST',
        data: JSON.stringify(datos),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            if (result == true) {
                $('#Comprobado-' + id).attr('src', "/Content/Imagenes/comprobado.png");
                $('#Cancelar-' + id).attr('src', "/Content/Imagenes/comprobado.png");

                $('#disable-' + id).attr('class', 'btn btn-sm btn-danger');
                $('#enable-' + id).attr('class', 'btn btn-sm btn-danger');

                $('#disable-' + id).attr('data-toggle', 'tooltip');
                $('#enable-' + id).attr('data-toggle', 'tooltip');

                $('#disable-' + id).attr('data-original-title', 'Deshabilitar este usuario.');
                $('#enable-' + id).attr('data-original-title', 'Deshabilitar este usuario.');

                let icono = document.createElement('icon');

                icono.setAttribute("class", "glyphicon glyphicon-remove");

                $('#disable-' + id).html("Deshabilitar ");
                $('#enable-' + id).html("Deshabilitar ");

                $('#disable-' + id).append(icono);
                $('#enable-' + id).append(icono);

                window.alert('¡Usuario habilitado!')

            } else {

                $('#Comprobado-' + id).attr('src', "/Content/Imagenes/cancelar.png");
                $('#Cancelar-' + id).attr('src', "/Content/Imagenes/cancelar.png");

                $('#disable-' + id).attr('class', 'btn btn-sm btn-success');
                $('#enable-' + id).attr('class', 'btn btn-sm btn-success');

                $('#disable-' + id).attr('data-toggle', 'tooltip');
                $('#enable-' + id).attr('data-toggle', 'tooltip');

                $('#disable-' + id).attr('data-original-title', 'Habilitar este usuario.');
                $('#enable-' + id).attr('data-original-title', 'Habilitar este usuario.');

                let icono = document.createElement('icon');

                icono.setAttribute("class", "glyphicon glyphicon-check");

                $('#disable-' + id).html("Habilitar ");
                $('#enable-' + id).html("Habilitar ");

                $('#disable-' + id).append(icono);
                $('#enable-' + id).append(icono);

                window.alert('Usuario deshabilitado.');

            }
                
        }
    });

}

// Se le adhiere un vector con todos los usuarios id y que elimine los tablas de todos.
//function cerrarPaneles() {
//    var panels = $('.user-infos');
//    panels.hide();
//}