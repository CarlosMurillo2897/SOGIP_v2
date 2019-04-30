var titulo = "Categoría";
var aux = 1;
var idSeleccionado = 0;

$(document).ready(function () {
    cargaDT(aux);

});

function cargaDT(id) {
    var col = [];
    var url, Identificador;
    var header = "<tr>";
    aux = id;
    
    switch (id) {
        case 1: {
            url = "/Opciones/GetCategorias";
            header = header + '<th style="width: 50%;">Nombre</th><th>Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Descripcion" }
            ];
            Identificador = "CategoriaId";
            titulo = "Categoría";
            break;
        }
        case 2: {
            url = "/Opciones/GetColores";
            header = header + '<th style="width: 25%;">Nombre</th><th style="width: 25%;">Identificador</th><th style="width: 25%;">Seleccionado</th><th style="width: 25%;">Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Nombre" },
                {
                    data: "Codigo",
                    "render": function (codigo) {
                        return '<text>' + codigo + '</text> <div class="foo" style="background-color: ' + codigo + '"></div>';
                    }
                },
                {
                    data: "Seleccionado",
                    "render": function (Selected) {
                        return Selected ? "Seleccionado" : "Sin seleccionar";
                    }
                }
            ];
            titulo = "Color <a href='https://flatuicolors.com/'>Más colores</a>";
            Identificador = "ColorId";
            break;
        }
        case 3: {
            url = "/Opciones/GetDeportes";
            header = header + '<th>Nombre</th><th>Tipo de Deporte</th><th>Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Nombre" },
                { data: "Descripcion" }
            ];
            titulo = "Deporte";
            Identificador = "DeporteId";
            break;
        }
        case 4: {
            url = "/Opciones/GetEntidades";
            header = header + '<th>Nombre</th><th>Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Descripcion" }
            ];
            titulo = "Entidad Pública";
            Identificador = "Tipo_EntidadId";
            break;
        }
        case 5: {
            url = "/Opciones/GetEstados";
            header = header + '<th style="width: 50%;">Nombre</th><th style="width: 50%;">Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Descripcion" }
            ];
            titulo = "Estado";
            Identificador = "EstadoId";
            break;
        }
        case 6: {
            url = "/Opciones/GetParametros";
            header = header + '<th>Nombre</th><th>Valor</th><th>Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Nombre" },
                { data: "Valor" }
            ];
            titulo = "Parámetro";
            Identificador = "ParametroId";
            break;
        }
        case 7: {
            url = "/Opciones/GetTiposArchivos";
            header = header + '<th>Nombre</th><th>Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Nombre" }
            ];
            titulo = "Tipo de Archivo";
            Identificador = "TipoId";
            break;
        }
        case 8: {
            url = "/Opciones/GetTipoDeportes";
            header = header + '<th>Nombre</th><th>Acción <span class="glyphicon glyphicon-cog"></span></th></tr>';
            col = [
                { data: "Descripcion" }
            ];
            titulo = "Tipo de Deporte";
            Identificador = "Tipo_DeporteId";
            break;
        }

    }
    $('#head').html(titulo);
    col[col.length] =
        {
            data: Identificador,
            "render": function (Identificador) {
                //return "<a class='btn btn-danger' id='boton_" + Identificador + "' onclick='EliminarArchivo(" + Identificador + ")' style='padding: 2px 6px; margin: 2px;' disabled='disabled'>" +
                //        "<text class='hidden-xs'>Eliminar </text>" +
                //        "<span class='glyphicon glyphicon-minus-sign'></span>" +
                //    "</a>" +
                    return "<a class='btn btn-warning' style='padding: 2px 6px; margin: 2px;' data-toggle='modal' onclick='CargarModal(this," + Identificador + ");'>" +
                        "<text class='hidden-xs'>Editar </text>" +
                        "<span class='glyphicon glyphicon-pencil'></span>" +
                    "</a>";
            }
        };

    $('#datos').DataTable().destroy();
    $('#datos').remove();

    $('<table />', {
        id: 'datos',
        class: 'table table-striped table-bordered table-hover dt-responsive'
    }).append("<thead>" + header + "</thead>").appendTo('#Content');

    $('#datos').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando _START_ de _END_, de un total de _TOTAL_ registros.",
            "infoEmpty": "No hay datos para mostrar.",
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
            "url": url,
            "type": "GET",
            "dataSrc": ""
        },
        columns: col
        /*dom: 'Bfrtip',
        buttons: [
            'print',// 'pdf',
            {
                    extend: "pdfHtml5",
                    customize: function (doc) {
                        var age = $('#datos').DataTable().column(1).data().toArray();
                        for (var i = 0; i < age.length; i++) {
                            if (age[i]==='yellow') {
                                doc.content[1].table.body[i + 1][1].fillColor = 'blue';
                            }
                        }
                    }
                }
        ]*/
    });
}
function cambiaColor() {
    $('#anticipada').css('background-color', $('#cod').val());
}

var Descripcion = "";

function cargaDatos(element) {
    Descripcion = $(element).closest('tr').find('td:eq(0)').text();
    var Parametro2 = $(element).closest('tr').find('td:eq(1)').text();

    var input = "<input type='text' class='form-control' id='cat' name='cat' placeholder='Digite la categoría a incorporar.' value='" + Descripcion + "'>";
    var extra1 = "";

    switch (aux) {
        case 2: {
            input = "<input type='text' class='form-control' id='col' name='col' placeholder='Digite el color a incorporar.' value='" + Descripcion + "'>";
            extra1 =
                '<div class="row">' +
                    '<div class="form-group col-md-6">' +
                        '<h4>Código de ' + titulo + '</h4>' +
                        '<div class="afect">' +
            '<input type="text" onkeyup="cambiaColor();" class="form-control" id="cod" name="cod" placeholder="Digite el código del color a incorporar." value="' + Parametro2 + '">' + 
                        '</div>' +
                    '</div>' +
                    '<div class="form-group col-md-6">' +
                    '<h4>Vista anticipada</h4>' +
                        '<div id="anticipada" class="foo" style="background-color: ' + Parametro2 + '; width: 100%;"></div>' +
                    '</div>'+
                '</div>';
            break;
        }
        case 3: {
            input = "<input type='text' class='form-control' id='dep' name='dep' placeholder='Digite el deporte a incorporar.' value='" + Descripcion + "'>";
            extra1 =
                "<div class='row'>" +
                    "<div class='form-group col-md-6'>" +
                        "<h4>Tipo de " + titulo + "</h4>" +
                        "<div class='afect'>" +
                            "<input type='text' class='form-control' id='tipoDepSelec' name='tipoDepSelec' placeholder='Seleccion de la lista el tipo de deporte a incorporar.' value='" + Parametro2 + "' readonly>" +
                        "</div>" +
                    "</div>" +
                    "<div class='form-group col-md-6'>" +
                        "<h4>Seleccione el Tipo de " + titulo + " de la tabla.</h4>" +
                            '<button id="botón" type="button" class="btn btn-warning btn-block">' +
                                '<text id="texto">Desplegar la tabla </text>' +
                                '<span id="icono" class="glyphicon glyphicon-chevron-up"></span>' +
                            '</button>' +
                    "</div>" +
                "</div>" +
                "<div class='row'>" +
                    "<div class='form-group col-md-12'>" +
                        "<div class='table-responsive' id='TipoDep'><div>" +
                    "</div>" +
                "</div>";

            break;
        }
        case 4: {
            input = "<input type='text' class='form-control' id='ent' name='ent' placeholder='Digite la Entidad Pública a incorporar.' value='" + Descripcion + "'>";
            break;
        }
        case 5: {
            input = "<input type='text' class='form-control' id='est' name='est' placeholder='Digite el estado a incorporar.' value='" + Descripcion + "'>";
            break;
        }
        case 6: {
            input = "<input type='text' class='form-control' id='par' name='par' placeholder='Digite el parámetro a incorporar.' value='" + Descripcion + "'>";
            extra1 =
                "<div class='row'>" +
                    "<div class='form-group col-md-12'>" +
                        "<h4>Valor de " + titulo + "</h4>" +
                        "<div class='afect'>" +
                            "<input type='text' class='form-control' id='valor' name='valor' placeholder='Digite el valor del parémtro a incorporar.' value='" + Parametro2 + "' >" +
                        "</div>" +
                    "</div>" +
                "</div>";
            break;
        }
        case 7: {
            input = "<input type='text' class='form-control' id='tpda' name='tpda' placeholder='Digite el tipo de archivo a incorporar.' value='" + Descripcion + "'>";
            break;
        }
        case 8: {
            input = "<input type='text' class='form-control' id='tpdd' name='tpdd' placeholder='Digite el tipo de deporte a incorporar.' value='" + Descripcion + "'>";
            break;
        }

        default: break;

    }

    var adjunta =
            "<div class='row'>" +
                "<div class='form-group col-md-12'>" +
                    "<h4>Nombre de " + titulo + "</h4>" +
                    "<div class='afect'>" +
                        input +
                    "</div>" +
                "</div>" +
            "</div>" +
            extra1;
        
    $('#modalBody').html(adjunta);

    $('#tipoDeporte').DataTable().destroy();
    $('#tipoDeporte').remove();

    var coloms = [
        { data: "Descripcion" },
        { data: "Tipo_DeporteId" }
    ];
    var rowSelected;

    $('<table />', {
        id: 'tipoDeporte',
        class: 'table table-striped table-bordered table-hover dt-responsive',
        width: '100%'
    }).append("<thead><th>Nombre</th><th>Identificador</th></tr></thead>").appendTo('#TipoDep');

    $('#tipoDeporte').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ resultados por página.",
            "zeroRecords": "No se han encontrado registros.",
            "info": "Mostrando _START_ de _END_, de un total de _TOTAL_ registros.",
            "infoEmpty": "No hay datos para mostrar.",
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
                    0: "Presione sobre la fila deseada.",
                    1: "1 registro seleccionado."
                }
            }
        },
        "ajax": {
            "url": "/Opciones/GetTipoDeportes",
            "type": "GET",
            "dataSrc": ""
        },
        columns: coloms,
        select: true,
        "createdRow": function (row, data, index) {
            if (data.Descripcion === Parametro2) {
                $(row).addClass('selected');
                rowSelected = $(row);
            }
        }
    });

    $('#tipoDeporte tbody').on('click', 'tr', function () {
        if (rowSelected !== undefined) {
            rowSelected.removeClass('selected');
            rowSelected = undefined;
        }
        var data = $('#tipoDeporte').DataTable().row(this).data();
        $('#tipoDepSelec').val() === data.Descripcion ? $('#tipoDepSelec').val('') : $('#tipoDepSelec').val(data.Descripcion);
    });
    
}

function CargarModal(element, opcion) {
    $('#modal h2 text').html(opcion === 0 ? 'Inserción de ' + titulo : 'Edición de ' + titulo);
    idSeleccionado = opcion;

    cargaDatos(element);
    

    $.validator.setDefaults({
        submitHandler: function () {
            //alert("Enviando datos . . .");
            agregarEditar();
        }
    });

    $("#signupForm1").validate({
        rules: {
            cat: {
                required: true,
                minlength: 2,
                remote: {
                    url: "/Opciones/CategoriaRepetida",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#cat').val(); }
                    }
                }
            },
            col: {
                required: true,
                minlength: 2,
                remote: {
                    url: "/Opciones/ColorRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#col').val(); },
                        original: function () { return idSeleccionado; }
                    }
                }
            },
            cod: {
                required: true,
                minlength: 2,
                remote: {
                    url: "/Opciones/CodigoColorRepetido",
                    type: "GET",
                    data: {
                        codigo: function () { return $('#cod').val(); },
                        original: function () { return idSeleccionado; }
                    }
                }
            },
            dep: {
                required: true,
                minlength: 2,
                remote: {
                    url: "/Opciones/DeporteRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#dep').val(); },
                        original: function () { return idSeleccionado; }
                    }
                }
            },
            tipoDepSelec: {
                required: true
            },
            ent: {
                required: true,
                minlength: 3,
                remote: {
                    url: "/Opciones/TipoEntidadPublicaRepetida",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#ent').val(); }
                    }
                }
            },
            est: {
                required: true,
                minlength: 3,
                remote: {
                    url: "/Opciones/EstadoRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#est').val(); }
                    }
                }
            },
            par: {
                required: true,
                minlength: 3,
                remote: {
                    url: "/Opciones/ParametroRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#par').val(); },
                        original: function () { return idSeleccionado; }
                    }
                }
            },
            valor: {
                required: true
            },
            tpda: {
                required: true,
                minlength: 3,
                remote: {
                    url: "/Opciones/TipoArchivoRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#tpda').val(); }
                    }
                }
            },
            tpdd: {
                required: true,
                minlength: 3,
                remote: {
                    url: "/Opciones/TipoDeportRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#tpdd').val(); }
                    }
                }
            }
        },
        messages: {
            cat: {
                required: "El nombre de la categoría es un campo requerido.",
                minlength: "La longitud mínima del nombre de la categoría es de 2 carácteres.",
                remote: "La categoría ya se encuentra en el sistema."
            },
            col: {
                required: "El nombre del color es un campo requerido.",
                minlength: "La longitud mínima del nombre del color es de 2 carácteres.",
                remote: "El color ya se encuentra en el sistema."
            },
            cod: {
                required: "El código del color es un campo requerido.",
                minlength: "La longitud mínima del código del color es de 2 carácteres.",
                remote: "El código de color ya se encuentra en el sistema."
            },
            dep: {
                required: "El nombre del deporte es un campo requerido.",
                minlength: "La longitud mínima del nombre del deporte es de 2 carácteres.",
                remote: "El nombre de deporte ya se encuentra en el sistema."
            },
            tipoDepSelec: {
                required: "El tipo de deporte es un campo requerido."
            },
            ent: {
                required: "El nombre del tipo de entidad es un campo requerido.",
                minlength: "La longitud mínima del nombre del tipo de entidad es de 3 carácteres.",
                remote: "El nombre del tipo de entidad ya se encuentra en el sistema."
            },
            est: {
                required: "El nombre del estado es un campo requerido.",
                minlength: "La longitud mínima del nombre del estado es de 2 carácteres.",
                remote: "El nombre del estado ya se encuentra en el sistema."
            },
            par: {
                required: "El nombre del parámetro es un campo requerido.",
                minlength: "La longitud mínima del nombre del parámetro es de 2 carácteres.",
                remote: "El nombre del parámetro ya se encuentra en el sistema."
            },
            valor: {
                required: "El valor del parámetro es requerido."
            },
            tpda: {
                required: "El nombre del tipo de archivo es un campo requerido.",
                minlength: "La longitud mínima del nombre del tipo de archivo es de 2 carácteres.",
                remote: "El nombre del tipo de archivo ya se encuentra en el sistema."
            },
            tpdd: {
                required: "El nombre del tipo de deporte es un campo requerido.",
                minlength: "La longitud mínima del nombre del tipo de deporte es de 2 carácteres.",
                remote: "El nombre del tipo de deporte ya se encuentra en el sistema."
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
    
    $('#modal').modal('show');
}

function agregarEditar() {
    var table = $('#datos').DataTable();
    var nuevosDatos;
    var datos;
    var url;

    switch (aux) {
        case 1: {
            url = "/Opciones/AgregarCategoria";
            datos = { Nombre: $('#cat').val().toUpperCase(), id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Descripcion": data.Descripcion, "CategoriaId": data.CategoriaId };
            };
            break;
        }
        case 2: {
            url = "/Opciones/AgregarColor";
            datos = { Nombre: $('#col').val().toUpperCase(), Codigo: $('#cod').val().toUpperCase(), Selected: true, id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Nombre": data.Nombre, "Codigo": data.Codigo, "Seleccionado": data.Seleccionado, "ColorId": data.ColorId };
            };
            break;
        }
        case 3: {
            url = "/Opciones/AgregarDeportes";
            datos = { Nombre: $('#dep').val().toUpperCase(), Tipo: $('#tipoDepSelec').val(), id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Nombre": data.Nombre, "Descripcion": data.TipoDeporte.Descripcion, "DeporteId": data.DeporteId };
            };
            break;
        }
        case 4: {
            url = "/Opciones/AgregarTipoEntidad";
            datos = { Nombre: $('#ent').val().toUpperCase(), id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Descripcion": data.Descripcion, "Tipo_EntidadId": data.Tipo_EntidadId };
            };
            break;
        }
        case 5: {
            url = "/Opciones/AgregarEstado";
            datos = { Nombre: $('#est').val().toUpperCase(), id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Descripcion": data.Descripcion, "EstadoId": data.EstadoId };
            };
            break;
        }
        case 6: {
            console.log('oko');
            url = "/Opciones/AgregarParametro";
            datos = { Nombre: $('#par').val().toUpperCase(), Valor: $('#valor').val(), id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Nombre": data.Nombre, "Valor": data.Valor, "ParametroId": data.ParametroId };
            };
            break;
        }
            
        case 7: {
            url = "/Opciones/AgregarTipoArchivo";
            datos = { Nombre: $('#tpda').val().toUpperCase(), id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Nombre": data.Nombre, "TipoId": data.TipoId };
            };
            break;
        }
        case 8: {
            url = "/Opciones/AgregarTipoDeporte";
            datos = { Nombre: $('#tpdd').val().toUpperCase(), id: idSeleccionado };
            nuevosDatos = function (data) {
                return { "Descripcion": data.Descripcion, "Tipo_DeporteId": data.Tipo_DeporteId };
            };
            break;
        }

    }

    $.ajax({
        url: url,
        type: "POST",
        data: datos,
        success: function (data) {
            idSeleccionado === 0 ? table.row.add(nuevosDatos(data)).draw() : table.ajax.reload(null, false);
            $('#modal').modal('hide');
        },
        error: function() {
            alert('Error desconocido.');
        }
    });
}