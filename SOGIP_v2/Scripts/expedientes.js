$(document).ready(function () {
    var x;
    // Limpiar todos los campos una vez que se recarga la página.
    clear();
    $('#usuario').val('');

    llenarTablaArchivos();
    cargarTipos();

    $('#select').change(function () {
        if ($(this).val() === '0') {
            $('#Load_Big').attr('src', '/Content/Imagenes/comprobado.png');
        }
        else {
            $('#Load_Big').attr('src', '/Content/Imagenes/cancelar.png');
        }
    });
    //llenarDataTable();
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

// La función pop nos despliega el modal pequeño de carga, con el cual se indica si se está ejecutando una acción, se completó o no se pudo realizar.
function pop(status) {
    document.getElementById('box').style.display = status ? "block" : "none";
    if (!status) {
        contenidoPop(2);
    }
}

// Este método indica por medio del "pop up" (modal pequeño), si la tarea fue efectiva, incorrecta o si está en proceso.
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

function llenarDataTable() {
    var isE = $('#example').DataTable();
    isE.destroy();
    tablaUsuarios();
}

function tablaUsuarios() {
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
            alert("Error en la carga de usuarios.");
        }
    });

    // Acá indicamos que si tocan un check-box se cambie el valor del input de texto "usuario", tomando céd, nom1, nom2, ap1 y ap2 para indicar quien está seleccionado.
    // También, otorgamos la propiedad de desplegar la datatable o cerrar la misma al botón naranja.
    // A su vez, una vez seleccionada una casilla se procederá a cerrar la dataTable con el método "manipularDT();" y a la vez, se cambiará el texto dentro del botón naranja.
    $('#example').on('click', 'td.select-checkbox', function () {
        var td = $(this);
        var tr = td.closest('tr');
        x = tr;

        $('#usuario').val(tr.find('td:eq(1)').text() + ' ' + tr.find('td:eq(2)').text() + ' ' + tr.find('td:eq(3)').text() + ' ' + tr.find('td:eq(4)').text());
        manipularDT();
        $('#botón').attr('onclick', 'manipularDT();');
        if (tr.hasClass('selected')) {
            $('#usuario').val('');
        }
    });

}

// Al hacer uso de slideToggle indicamos que en 400 milisegundos (4segs) se esconda o muestre por el contrario cualquier elemento específicado.
// En el proceso de esconder o mostrar el elemento tomaremos el texto del botón y lo cambiaremos, así como el ícono que lo acompaña.
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

// Para determinar si el archivo fue subido correctamente haremos uso del popup de carga.
// Primeramente, antes de proceder al AJAX usaremos el método pop(true), con el cual mostraremos el .gif de carga.
// Dentro del ajax en caso de ser success mostraremos un check en vez del .gif que se mostró primeramente, en caso contrario mostraremos una X roja.
function subirArchivo() {
    var select = $("#select option:selected").val();
    var id = $('#usuario').val().split(' ')[0];
    var archivo = $('#archivo')[0].files[0];

    if (archivo !== undefined && archivo.size >= 20000000 && !confirm('\n¡Cuidado! Estás intentando subir un archivo de más de 20MB.\n\n ¿Estás seguro de querer subir este archivo?\n\n')) {
        clear();
        archivo = undefined;
    }

    if (select === '0' || id === '' || archivo === undefined) {
        alert('Error, faltan datos por completar.');
    }

    else {

        //*************** Mostramos el pop up.
        pop(true);

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
                // Cambiamos la imágen que despliega el pop up (.gif) por un check.
                contenidoPop(1);

                var table = $('#archivos').DataTable();
                table.row.add({
                    "Nombre": data.Nombre,
                    "Tipo": data.Tipo.Nombre,
                    "Usuario": data.Usuario.Cedula + " " + data.Usuario.Nombre1 + " " + data.Usuario.Nombre2 + " " + data.Usuario.Apellido1 + " " + data.Usuario.Apellido2,
                    "Id": data.ArchivoId
                }).draw();

                x.removeClass('selected');
                $('#usuario').val('');  
                $('#select').val(0);
                clear();
            },
            error: function (data) {
                contenidoPop(1);

                $('#archivos').DataTable().ajax.reload(null, false);
                x.removeClass('selected');
                $('#select').val(0);
                $('#usuario').val('');
                clear();
            }
        });
    }
}

// En este caso, haremos uso de la propiedad AJAX para inyectar directamente a través de una lista los archivos.
// Tomaremos las propiedades que provengan del controller {Nombre, Tipo, Usuario y Id}. Con el campo Id renderizaremos lo que son 3 botones.
function llenarTablaArchivos() {
    $('#archivos').DataTable({
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
                "url": "/ExpedientesFisicos/ObtenerArchivos",
                "type": "GET",
                "dataSrc": ""
            },
            columns: [
                { data: "Nombre" },
                { data: "Tipo" },
                { data: "Usuario" },
                {
                    data: "Id",
                    "render": function (Id) {
                        debugger
                        return "<a class='btn btn-danger' id='boton_" + Id + "' onclick='EliminarArchivo(" + Id + ")' style='padding: 2px 6px; margin: 2px;'>" +
                                    "<text class='hidden-xs'>Eliminar </text>" +
                                    "<span class='glyphicon glyphicon-minus-sign'></span>" +
                                "</a>" +
                                "<a class='btn btn-warning' style='padding: 2px 6px; margin: 2px;' data-toggle='modal' data-target='#modalEdición'>" +
                                    "<text class='hidden-xs'>Editar </text>" +
                                    "<span class='glyphicon glyphicon-pencil'></span>" +
                                "</a>" +
                                "<a class='btn btn-info' href='/UsersAdmin/Down?archivoId=" + Id + "' style='padding: 2px 6px; margin: 2px;'>" +
                                    "<text class='hidden-xs'>Descargar </text>" +
                                    "<span class='glyphicon glyphicon-download'></span>" +
                                "</a>";
                    }
                }
            ]
    });
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