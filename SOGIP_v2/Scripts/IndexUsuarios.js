$(document).ready(function () {
    var panels = $('.user-infos');
    var panelsButton = $('.dropdown-user');
    panels.hide();

    $.noConflict();
    $('#tabla').DataTable({
        // "aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],
        // "iDisplayLength": 5,
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado resultados.",
            "info": "Mostrando página _PAGE_ de _PAGES_.",
            "infoEmpty": "No hay datos para mostrar",
            "infoFiltered": "(filtrado de _MAX_ datos obtenidos).",
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }

    });

    //Click dropdown
    panelsButton.click(function () {
        //get data-for attribute
        var dataFor = $(this).attr('data-for');
        var idFor = $(dataFor);
        //current button
        var id = dataFor.slice(1);
        var currentButton = $('#boton-' + id);

        if (idFor.is(':visible')) {
            let index, table = document.getElementById("tabla2-"+id);

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
        });
    });
});

function dis(id, DI) {

    var clase = document.getElementById(DI).className;

    var status = clase === 'btn btn-sm btn-danger'? true : false;

    var datos = {
        'usuarioId': id,
        'estado': status
    };

    $.ajax({
        url: '/UsersAdmin/InhabilitarUsuario',
        dataType: 'JSON',
        type: 'POST',
        data: JSON.stringify(datos),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result === true) {
                $('#Comprobado-' + id).attr('src', "/Content/Imagenes/comprobado.png");
                $('#Cancelar-' + id).attr('src', "/Content/Imagenes/comprobado.png");

                $('#disable-' + id).attr('class', 'btn btn-sm btn-danger');
                $('#enable-' + id).attr('class', 'btn btn-sm btn-danger');

                let icono = $("<i/>", { class: "glyphicon glyphicon-remove" });
                let msg = $("<text/>", { text: "Deshabilitar ", class: "hidden-xs" });

                $('#enable-' + id).children('text').remove();
                $('#enable-' + id).children('i').remove();
                $('#enable-' + id).append(msg);
                $('#enable-' + id).append(icono);

                $('#disable-' + id).children('i').remove();
                $('#disable-' + id).children('text').remove();
                $('#disable-' + id).append(msg);
                $('#disable-' + id).append(icono);

                window.alert('¡Usuario habilitado!');

            } else {

                $('#Comprobado-' + id).attr('src', "/Content/Imagenes/cancelar.png");
                $('#Cancelar-' + id).attr('src', "/Content/Imagenes/cancelar.png");

                $('#disable-' + id).attr('class', 'btn btn-sm btn-success');
                $('#enable-' + id).attr('class', 'btn btn-sm btn-success');

                let icono = $("<i/>", { class: "glyphicon glyphicon-check" });
                let msg = $("<text/>", { text: "Habilitar ", class: "hidden-xs" });

                $('#enable-' + id).children('text').remove();
                $('#enable-' + id).children('i').remove();
                $('#enable-' + id).append(msg);
                $('#enable-' + id).append(icono);

                $('#disable-' + id).children('i').remove();
                $('#disable-' + id).children('text').remove();
                $('#disable-' + id).append(msg);
                $('#disable-' + id).append(icono);

                window.alert('Usuario deshabilitado.');
            }
                
        }
    });

}