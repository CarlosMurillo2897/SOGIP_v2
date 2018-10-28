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

            //let dl = document.getElementById('new');

            //let dt = document.createElement('dt');
            //let dd = document.createElement('dd');

            //let text1 = document.createTextNode('NUEVO'); 
            //let text2 = document.createTextNode('123');

            //dt.appendChild(text1);
            //dd.appendChild(text2);
            //dl.appendChild(dt);
            //dl.appendChild(dd);

            while (table.rows.length > 8) {
                table.deleteRow(8);
            }

        }
        else {

            $.get("/UsersAdmin/ArchivosUsuario", { usuarioId: id }, function (data) {
                $.each(data, function (index, row) {
                    index = index + 1;
                    $("#" + id).append('<tr><td>' + index + '. ' + row.Nombre + '</td><td>' + row.Tipo + '</td><td>' +
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

    //$('#inhabilitar').click(function (e) {
    //    e.preventDefault();
    //    confirm("¿Inhabilitar este usuario?");
    //});

});

//function cerrarPaneles() {
//    var panels = $('.user-infos');
//    panels.hide();
//}