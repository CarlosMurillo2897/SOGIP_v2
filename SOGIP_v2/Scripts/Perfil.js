$.validator.setDefaults({
    submitHandler: function () {
        alert("Enviando solicitud.");
        sendData();
    }
});

$.validator.addMethod("passw", function (value, element) {
    return value.length !== 0 ? /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)([A-Za-z0-9]){6,12}$/.test(value) :  true;
});

var p1, p2,p3,p4;
$(document).ready(function () {
    // Normal
    //unlockForm("None", "None", "Nn");
    // Desarrollando
    unlockForm("Supervisor", "None", "Nn");

    $('[data-toggle="popover"]').popover();

    var today = new Date();
    var min = new Date();
    var max = new Date();
    max.setFullYear(today.getFullYear() - 80);
    min.setFullYear(today.getFullYear() - 10);

        $("#dtp").datepicker({
            format: 'yyyy/mm/dd',
            defaultViewDate: max,
            startDate: max, 
            endDate: min,
            daysOfWeekDisabled: false
    });
        
    $("#signupForm1").validate({
        rules: {
            Cedula: {
                required: true,
                minlength: 9,
                remote: {
                    url: "/UsersAdmin/CedulaRepetida",
                    type: "GET",
                    data: {
                        ced: function () { return $('#Cedula').val(); },
                        Id: function () { return $('#Id').val(); }
                    }
                }
            },
            Nombre1: {
                required: true,
                minlength: 2
            },
            Apellido1: {
                required: true,
                minlength: 2
            },
            Sexo: {
                required: true
            },
            PasswordHash: {
                // required: true,
                minlength: 6,
                passw: true
            },
            Email: {
                required: true,
                email: true
            },
            Fecha_Nacimiento: {
                required: true
            },
            // Selecciones
            Nombre_Seleccion: {
                required: true,
                minlength: 6,
                remote: {
                    url: "/UsersAdmin/NombreSeleccionRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#Nombre_Seleccion').val(); },
                        Id: function () { return $('#Id').val(); }
                    }
                }
            },
            Nombre_Deporte: {
                required: true
            },
            // Asociaciones
            Nombre_Asociacion: {
                required: true,
                minlength: 4,
                remote: {
                    url: "/UsersAdmin/NombreAsociacionRepetido",
                    type: "GET",
                    data: {
                        nombre: function () { return $('#Nombre_Asociacion').val(); },
                        Id: function () { return $('#Id').val(); }
                    }
                }
            },
            // Entidades
            Nombre_Entidad: {
                required: true,
                minlength: 4
            },
            // Atletas
            Nombre_Entidad_Atleta: {
                required: true,
                minlength: 4
            },
            // Funcionarios
            Nombre_Entrenador_Funcionario: {
                required: true,
                minlength: 4
            }
        },
        messages: {
            Cedula: {
                required: "La cédula es un campo obligatorio.",
                minlength: "La longitud mínima de la cédula debe ser de 9 carácteres.",
                remote: "La cédula ingresada ya se encuentra en el sistema."
            },
            Nombre1: {
                required: "El primer nombre es un campo requerido.",
                minlength: "La longitud mínima del primer nombre debe ser de 2 carácteres."
            },
            Apellido1: {
                required: "El primer apellido es un campo requerido.",
                minlength: "La longitud mínima del primer apellido debe ser de 2 carácteres."
            },
            Sexo: {
                required: "El campo de género es requerido."
            },
            PasswordHash: {
                // required: "El campo de contraseña es obligatorio.",
                minlength: "La contraseña debe tener un mínimo de 6 letras.",
                passw : "Contraseña requiere de al menos una letra mayúscula, una minúscula, un número y una longitud de entre 6 y 12 letras."
            },
            Email: "Especifique su e-mail con el formato correcto.",
            Fecha_Nacimiento: "La fecha de nacimiento es obligatoria.",
            // Selecciones
            Nombre_Seleccion: {
                required: "El nombre de la selección es un campo obligatorio.",
                minlength: "La longitud mínima del nombre de la selección debe ser de 6 carácteres.",
                remote: "El nombre de la selección ingresada ya se encuentra en el sistema."
            },
            Nombre_Deporte: {
                required: "El Deporte de la Selección es requerido."
            },
            // Asociaciones
            Nombre_Asociacion: {
                required: "El nombre de la asociación es un campo obligatorio.",
                minlength: "La longitud mínima del nombre de la asociación es de 4 carácteres.",
                remote: "El nombre de la asociación ingresada ya se encuentra en el sistema."
            },
            // Entidades
            Nombre_Entidad: {
                required: "El nombre de la entidad es un campo obligatorio.",
                minlength: "La longitud mínima del nombre de la entidad pública es de 4 carácteres."
            },
            // Atletas
            Nombre_Entidad_Atleta: {
                required: "El nombre de la Entidad asociada es obligatorio.",
                minlength: "La longitud mínima del nombre de la Entidad es de 4 carácteres."
            },
            // Funcionarios
            Nombre_Entrenador_Funcionario: {
                required: "El nombre del Entrenador asociado es obligatorio.",
                minlength: "La longitud mínima del nombre del Entrenador es de 4 carácteres."
            }
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("help-block");
            element.parents(".afect").addClass("has-feedback");

            if (element.prop("id") === "Fecha_Nacimiento") {
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
var categ = "";
$('#tablaCategorias tbody tr').on('click', 'td:eq(3)', function () {
    var tr = $(this).closest('tr');//.find('td:eq(0)').text()
    
    cargarModal(2, tr.find('td:eq(0)').text(), tr.find('td:eq(1)').text());
});

function cargarArchivos(id) {

    $('#archivosUsuario').DataTable().destroy();
    $('#archivosUsuario').remove();
    var inactive = $('#invalid').val();

    var table = $('<table/>', {
        id: 'archivosUsuario',
        class: 'table table-striped table-bordered dt-responsive nowrap',
        width: '100%'
    }).append('<thead><tr><th>Nombre</th><th>Tipo</th><th>Descarga</th></tr></thead>');

     $('#files').append(table);

    table = $('#archivosUsuario').DataTable({
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
            "url": "/UsersAdmin/ArchivosUsuario",
            "type": "GET",
            "dataSrc": "",
            "data": { usuarioId: id }
        },
        columns: [
            { data: "Nombre" },
            { data: "Tipo" },
            {
                data: "ArchivoId",
                render: function (ArchivoId) {
                    var disable = "disabled";
                    var href = "#";
                    if (!inactive) {
                        disable = "";
                        href = "href='/UsersAdmin/Download?archivoId=" + ArchivoId + "'";
                    }

                    return "<a class='btn btn-info' " + href + " style='padding: 2px 6px; margin: 2px;' " + disable + " >"
                        + "<text class='hidden-xs'>Descargar </text><span class='glyphicon glyphicon-download'></span></a>";
                }
            }
        ]
    });
}

function cargarAtletas(ced) {

    $('#asignados').DataTable().destroy();
    $('#asignados').remove();

    var url = "";
    var col = [
        { data: "Cedula" },
        { data: "Nombre" },
        { data: "Apellido1" },
        { data: "Apellido2" }
    ];

    var head = '<thead><tr><td>Cédula</td><td>Nombre</td><td>1° Apellido</td><td>2° Apellido</td><td>Acción</td></tr></thead>';

    if ($('#role').val() === "Administrador") {
        url = "/AtletasAsignados/GetAtletasAdministrador";
    }
    else if ($('#role').val() === "Seleccion/Federacion") {
        url = "/AtletasAsignados/GetUsuariosSeleccion";
        head = '<thead><tr><td>Cédula</td><td>Nombre</td><td>1° Apellido</td><td>2° Apellido</td><td>Rol</td><td>Categoría</td><td>Acción</td></tr></thead>';
        col[col.length] = { data: "Rol" };
        col[col.length] = { data: "Categoria" };
    }
    else if ($('#role').val() === "Asociacion/Comite") {
        url = "/AtletasAsignados/GetAtletasAsociacion";
    }
    else if ($('#role').val() === "Entrenador") {
        url = "/AtletasAsignados/GetUsuariosEntrenador";
      //head = '<thead><tr><td>Cédula</td><td>Nombre</td><td>1° Apellido</td><td>2° Apellido</td><td>Categoría</td><td>Selección</td><td>Acción</td></tr></thead>';
        head = '<thead><tr><td>Cédula</td><td>Nombre</td><td>1° Apellido</td><td>2° Apellido</td><td>Categoría</td><td>Acción</td></tr></thead>';
        col[col.length] = { data: "Categoria" };
        //col[col.length] = { data: "Seleccion" };
    }

    col[col.length] = {
        data: "Id",
        "render": function (Id) {
            return "<a class='btn btn-warning' href='/Account/Perfil?id=" + Id + "' style='padding: 2px 6px; margin:2px;'>" +
                "<text class=''> Ver en detalle </text>" +
                "<span class='glyphicon glyphicon-user'></span>" +
                "</a>";
        }
    };

    var table = $('<table/>', {
        id: 'asignados',
        class: 'table table-striped table-bordered dt-responsive',
        width: '100%'
    }).append(head);

    $('#asignados').append(head);
    $('#athletes').append(table);

    $('#asignados').DataTable({
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
            "url": url,
            "type": "GET",
            "dataSrc": "",
            "data": { usuarioId: ced }
        },
        columns: col
    });

}
function cargarRutinas(IdUsuario) {

    $('#tablaRutinas').DataTable().destroy();
    $('#tablaRutinas').remove();

        var col = [];
        var head = '<thead><tr><th>Usuario</th><th>Fecha</th><th>Observaciones</th><th>Acción <span class="glyphicon glyphicon-cog"></span></th></tr></thead>';

        col[col.length] = { data: "Usuario" };
        col[col.length] = {
            data: "RutinaFecha",
            "render": function (RutinaFecha) {
                var date = new Date(parseInt(RutinaFecha.substr(6)));
                return date.toLocaleDateString('en-GB');
            }
        };
        col[col.length] = { data: "RutinaObservaciones" };
        col[col.length] = {
            data: "RutinaId",
            "render": function (Id) {
                return "<a class='btn btn-success' href='/Rutinas/ListaEjercicio?id=" + Id + "' target='_blank' style='padding: 2px 6px; margin:2px;'>" +
                    "<text class=''> Ver en detalle </text>" +
                    "<span class='glyphicon glyphicon-apple'></span>" +
                    "</a>";
            }
    };

    var table = $('<table/>', {
        id: 'tablaRutinas',
        class: 'table table-striped table-bordered dt-responsive',
        width: '100%'
    }).append(head);

    $('#routines').append(table);

    $('#tablaRutinas').DataTable({
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
            "url": "/Rutinas/obtenerRutinasUsuario",
            "type": "GET",
            "dataSrc": "",
            "data": { id: IdUsuario }
        },
        columns: col
    });
}

//"@ViewBag.rol_Usuario_Actual", "@ViewBag.usuario_Actual", "@Model.Cedula", true
function unlockForm(role, id_Actual, Cedula) {
    if (role === 'Administrador' || role === 'Supervisor' || id_Actual === Cedula) {
        $("#signupForm1 :input").attr("disabled", false);
    }
    else{
        $("#signupForm1 :input").attr("disabled", true);
    }
}

function GetCategorias(str) {
    var newArray = [];
    var TableData = [];
    $('#tablaCategorias tbody tr').each(function (row, tr) {
        TableData.push($(tr).find("td:eq(1)").text());
    });

    //console.log(`TDT = ${TableData}`);

    $.ajax({
        url: "/Opciones/GetCategorias",
        type: "GET",
        success: function (consulta) {
            $.each(consulta, function (i) {
                newArray.push(consulta[i].Descripcion);
            });
            //console.log(`NewA = ${newArray}`);
            newArray = newArray.filter((el) => !TableData.includes(el));
            
            if (newArray.length === 0) {
                console.log('mpt');
            }
            var selex = '<select class="form-control" id="cat"><option id="init" value="0"> -- Seleccionar -- </option>';
            $.each(newArray, function (i) {
                selex = selex + `<option>${newArray[i]}</option>`;
            });
            selex = selex + '</select>';
            //console.log(`[Filter] = ${newArray}`);
            //Div1
            $('#Div1').append('<div class="row">' +
                                '<div class="form-group col-md-12">' +
                                    '<h4>Seleccione el tipo de categoría deseada</h4>' + 
                                    '<div class="afect">' +
                                        selex +
                                    '</div>' +
                                '</div>');

            if (str !== '') { $("#cat").append(`<option selected>${str}</option>`); }
        }
    });
    
}

function sendData() {

    $("#signupForm1 :input[type=text]").val(function () {
        return this.value.toUpperCase();
    });

    var user = $('#signupForm1').serialize();

    user = user + '&role=' + $('#role').val();

    var a1 = [];
    var a2 = [];
    var a3 = [];

    $('#tablaCategorias tbody tr').each(function (row, tr) {
        user += '&l1=' + encodeURIComponent($(tr).find("td:eq(0)").text());
        user += '&l2=' + encodeURIComponent($(tr).find("td:eq(1)").text());
        user += '&l3=' + encodeURIComponent($(tr).find("td:eq(2)").text().split('-')[0]);
    });
    // user += '&Entidad_Atleta' + $('#Entidad_Atleta').val().split('-')[0];
    console.log(user);
    
   $.ajax({
        url: "/UsersAdmin/UpdateUser/",
        type: "POST",
        data: user,
        success: function () {
            alert('Cambios realizados.');
            location.reload();
        },
        error: function () {
            alert('Error desconocido. Contactar a soporte si continua.');
        }
    });
    

}

function OriginalPass() {
    if (!confirm('¿Está seguro de devolver la contraseña a sus valores predeterminados?')) {
        return;
    }
    $.ajax({
        url: "/UsersAdmin/RestaurarContraseñaOriginal",
        type: "POST",
        data: { Id: $('#Id').val() },
        success: function () {
            alert('Contraseña restaurada con éxito.');
        },
        error: function () {
            alert('Operación no completada. Intente de nuevo o contacte soporte en breve.');
        }

    });
}

var opcionSeleccionada = 0;
var fila = 0;
function cargarModal(tipo, filaSel, str = '') {
    opcionSeleccionada = tipo;
    fila = filaSel;
    p1 = "", p2 = "", p3 = "", p4 = "";
    //var saved = [];
    var url, header, rowSelected = "", dt = "";
    var col = [];

    $('#Div1').html('');
    $('#tablaDiv').html('');

    switch (tipo) {
        case 1: {
            $('#modalTitle').html('Buscar Deporte deseado');
            url = "/Opciones/GetDeportes";
            header = header + '<th>Nombre</th><th>Tipo de Deporte</th><th>Identificador</th></tr>';
            col = [
                { data: "Nombre" },
                { data: "Descripcion" },
                { data: "DeporteId" }
            ];
            break;
        }
        case 2: {

            filaSel === 0 ? $('#modalTitle').html('Agregar nueva Categoría') : $('#modalTitle').html('Editar la Categoría seleccionada');
            dt = {
                tipo: 4
            };
            url = "/UsersAdmin/getEntrenador";
            header = header + '<th>Cédula</th><th>Nombre</th><th>1er Apellido</th><th>2do Apellido</th></tr>';
            col = [
                { data: "Cedula" },
                { data: "Nombre1" },
                { data: "Apellido1" },
                { data: "Apellido2" }
            ];
            //if (!GetCategorias()) {
            //    alert('La selección ya cuenta con todas las categorías disponibles.');
            //}
            GetCategorias(str);
            $('#tablaDiv').append('<h4>Seleccione el entrenador que se asignará a la categoría</h4>');
            break;
        }
        case 3: {
            $('#modalTitle').html('Buscar Entidad deseada');
            url = "/Opciones/GetEntidades";
            header = header + '<th>Nombre</th><th>Identificador</th></tr>';
            col = [
                { data: "Descripcion" },
                { data: "Tipo_EntidadId" }
            ];
            break;
        }
        case 4: {
            $('#modalTitle').html('Buscar Entidad deseada');
            url = "/UsersAdmin/ObtenerUsuarios";
            header = '<th>Entidad</th><th>Cédula</th><th>Nombre</th><th>Rol</th><th>Acción</th>';
            col = [
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
            ];
            break;
        }
        case 5: {
            $('#modalTitle').html('Buscar Entrenador(a) deseado(a)');
            url = "/UsersAdmin/getEntrenador";
            dt = {
                tipo: 2
            };
            header = header + '<th>Cédula</th><th>Nombre</th><th>1er Apellido</th><th>2do Apellido</th></tr>';
            col = [
                { data: "Cedula" },
                { data: "Nombre1" },
                { data: "Apellido1" },
                { data: "Apellido2" }
            ];
            break;
        }
    }

    $('#datos').DataTable().destroy();
    $('#datos').remove();
    
    $('<table />', {
        id: 'datos',
        class: 'table table-striped table-bordered table-hover dt-responsive',
        width: "100%"
    }).append("<thead>" + header + "</thead>").appendTo('#tablaDiv');
    
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
            "url": url,
            "type": "GET",
            "dataSrc": "",
            "data": dt
        },
        columns: col,
        select: true
    });
    
    $('#datos tbody').on('click', 'tr', function () {
        var data = $('#datos').DataTable().row(this).data();
        switch (opcionSeleccionada) {
            case 1: { // Cambiar de deporte
                if (p1 === data.Nombre) {
                    p1 = '';
                    p2 = '';
                } else {
                    p1 = data.Nombre;
                    p2 = data.DeporteId;
                }
                break;
            }
            case 2: { // Agregar o editar categoría
                p1 = data.Cedula;
                p2 = data.Nombre1;
                p3 = data.Apellido1;
                p4 = data.Apellido2;
                break;
            }
            case 3: {
                if (p1 === data.Descripcion) {
                    p1 = '';
                    p2 = '';
                } else {
                    p1 = data.Descripcion;
                    p2 = data.Tipo_EntidadId;
                }
                break;
            }
            case 4: {
                
                break;
            }
            case 5: {
                p1 = data.Cedula;
                p2 = data.Nombre1;
                p3 = data.Apellido1;
                p4 = data.Apellido2;
                break;
            }
        }
    });
    
    $('#datos').on('change', '.selectDT', function () {
        // Tomamos el valor, el cual es el id de Categoría.
        p1 = $(this).val();

        if (p1 === '0') {
            p2 = '';
            //$('#Entidad_Atleta').val('');
        }
        else {
            // Nombre de Selección y Categoría.
            p2 = $(this).attr('id').split('_')[2] + "-" + $(this).find('option:selected').text();
        }
        // El Rol de la entidad.
        p3 = $(this).closest('tr').find('td:eq(3)').text();

        $('.selectDT').val(0);
        $(this).val(p1);

    });

    $('#modal').modal('show');

}

$('#modalSave').on('click', function () {

    switch (opcionSeleccionada) {
        case 1: {
            if (p1 !== "") { $('#Nombre_Deporte').val(p1); }
            if (p2 !== "") { $('#Deporte_Id').val(p2); }
            break;
        }
        case 2: {
            var sel = $('#cat option:selected').text();
            if ($('#cat option:selected').val() !== '0') {
                var _all = 'SIN ENTRENADOR';

                if (p1 !== '') {
                    _all = `${p1}-${p2} ${p3} ${p4}`;
                }

                if (fila === 0) {
                    $('#tablaCategorias tbody').append(`<tr><td>0</td><td>${sel}</td><td>${_all}</td><td><button type="button" class=" btn btn-primary" onclick="cargarModal(3);">Editar <span class="glyphicon glyphicon-edit"></span></button></td></tr>`);
                }
                else {
                    $('#tablaCategorias tbody tr').each(function (row, tr) {
                        if ($(tr).find("td:eq(0)").text() === fila) {
                            $(tr).find("td:eq(1)").text(`${sel}`);
                            $(tr).find("td:eq(2)").text(`${_all}`);
                        }
                    });
                }
            }
            break;
        }
        case 3: {
            if (p1 !== "") { $('#Nombre_Entidad').val(p1); }
            if (p2 !== "") { $('#Entidad_Id').val(p2); }
            break;
        }
        case 4: {
            // Id de Categoría
            if (p1 !== "") { $('#Categoria_Atleta_Id').val(p1); }
            // Nombre de Selección[0].
            if (p2 !== "") {
                $('#Nombre_Entidad_Atleta').val(p2);
                $('#Entidad_Atleta').val(p2.split('-')[0]);
            }
            // El Rol de la entidad, para buscar Atletas
            if (p3 !== "") { $('#Rol_Entidad_Atleta').val(p3); }
            break;
        }
        case 5: {
            if (p1 !== '') {
                $('#Nombre_Entrenador_Funcionario').val(`${p1}-${p2} ${p3} ${p4}`);
                $('#Funcionario_Cedula').val(p1);
            }
            break;
        }
    }

    $('#modal').modal('hide');

});