$(document).ready(function () {
    //$('#selectedRoles').val('Atleta');
    formulario();
    $('[data-toggle="popover"]').popover();

    $('#selectedRoles').change(function check() {
        if ($('#selectedRoles').val() === 'Administrador' || $('#selectedRoles').val() === 'Supervisor') {     
            if (!confirm("Está a punto de crear un Administrador o Supervisor. ¿Continuar?")) {
                $('#selectedRoles').val('Atleta');
                alert('Cambiando a rol tipo Atleta.');
            }
        }
        formulario(); 
    });
});

function formulario() {
    var rol_selected = $('#selectedRoles option:selected').val();

    $('id_entrenador').val('');
    $('#entrenadorF').val('');
    $('#nombre_aso').val('');
    $('sele_n').val('');
    $('#CV').val('');
    
    switch (rol_selected) {

        case "Asociacion/Comite":
            $('#title_aso').show();
            $('#info_aso').show();
            $('#nombre_aso').prop('required', true);

            $('#title_atleta').hide();
            $('#info_atleta_tipo').hide(); //selección o asociación
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();
            $('#inpE').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            $('#btnE').hide();
            break;

        case "Atleta":
            $('#title_atleta').show();
            $('#info_atleta_tipo').show();
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').show();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();
            $('#inpE').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            $('#btnE').hide();
            fillDT(1);

            break;

        case "Atleta Becados":
            $('#title_atleta').show();
            $('#info_atleta_tipo').show();
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').show();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();
            $('#inpE').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            $('#btnE').hide();
            fillDT(1);

            break;

        case "Entidades Publicas":
            $('#title_entidad').show();
            $('#info_entidad').show();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_atleta').hide();
            $('#info_atleta_tipo').hide(); //selección o asociación
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();
            $('#inpE').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            $('#btnE').hide();
            break;

        case "Entrenador":
            $('#title_entrenador').show();
            $('#info_entrenador').show();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_atleta').hide();
            $('#info_atleta_tipo').hide(); //selección o asociación
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();
            $('#inpE').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();
            $('#btnE').hide();
            break;

        case "Funcionarios ICODER":
            $('#title_icoder').show();
            $('#btnE').show();
            $('#inpE').val('');
            $('#inpE').hide();
            

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_atleta').hide();
            $('#info_atleta_tipo').hide(); //selección o asociación
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            $('#info_selec_entre').hide();
            fillDT(2);
            break;

        case "Seleccion/Federacion":
            $('#title_seleccion').show();
            $('#info_seleccion').show();
            $('#info_seleccion_nombre').show();
            $('#btnE').show();
            $('#inpE').val('');
            $('#inpE').hide();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_atleta').hide();
            $('#info_atleta_tipo').hide(); //selección o asociación
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();
            $('#info_func').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            fillDT(3);
            break;

        default:
            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_atleta').hide();
            $('#info_atleta_tipo').hide(); //selección o asociación
            $('#info_atleta_selec').hide();
            $('#info_atleta_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            $('#btnE').hide();
            $('#inpE').hide();
            break;

    }

}


function fillDT(tipo) {
    $('#example').DataTable();
    $('#example').DataTable().destroy();
    $('#example').remove();
    /*Cedula = u.Cedula,
                                Nombre1 = u.Nombre1,
                                Nombre2 = u.Nombre2,
                                Apellido1 = u.Apellido1,
                                Apellido2 = u.Apellido2,
                                Entidad = s.Nombre_Seleccion,
                                Role = "Seleccion/Federacion"*/
    var header = tipo === 1 ? '<thead><tr><th>Entidad</th><th>Cédula</th><th>Nombre</th><th>Rol</th><th>Acción</th></tr></thead>' :'<thead><tr><th>Acción</th><th>Cédula</th><th>Nombre</th><th>1° Apellido</th><th>2° Apellido</th></tr></thead>';
    var table = $('<table/>', {
        id: 'example',
        class: 'table table-striped table-bordered dt-responsive nowrap',
        width: '100%'
    }).append(header);

    $('#Tabla').append(table);

    // { 1: Asox y Selex, 2: Admin, 3: Entrenador }
    if (tipo === 1) {
        // dataTableEntidad(tipo);
    } else {
        dataTable(tipo);
    }
}

function dataTableEntidad(tipo) {
        var glob = true;

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

//Elección de entrenadores
function dataTable(tipo) {
    var table;
    var dataSet = [];

    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: { tipo: tipo },
        url: "/UsersAdmin/getEntrenador",
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
                'columnDefs': [
                    {
                        orderable: false,
                        className: 'select-checkbox',
                        targets: [0]
                    }
                ],
                'select': {
                    'style': 'os',
                    'selector': 'td:first-child'
                }
            });
        },
        error: function (error) {
            alert("Error desconocido, contacte a soporte en breve.");
        }
    });

    $('#btnSave').click(function () {
        var tblData = table.rows('.selected').data();

        $.each(tblData, function (i, val) {
            var r = confirm("¿Desea realizar la transacción?");

            if (r === true) {
                $('#inpE').show();
                document.getElementById("einf").value = val[1] + " " + val[2] + " " + val[3] + " " + val[4];
                document.getElementById("hidef").value = val[1];
            }
            else {
                $("#me").modal('show');
            }
        });
    });
}

//******************************************************************************//

function isNumber(evt) {  // Aceptar solo números y otros comandos en la cédula.

    var allow = $("input[name='Nacionalidad']:checked").val();

    if (allow === "Nacional") {

        var key = evt.which || evt.keyCode;

        // Detectar si Ctrl fue ingresado.
        var ctrl = evt.ctrlKey ? evt.ctrlKey : key === 17 ? true : false; 

        // El código a continuación detecta si se va a Pegar(Ctrl + V = 118) algún texto y se valida que sea numérico.
        if (key === 118 && ctrl) { 
            pegar(evt);
        }

         /* 
         El código a continuación acepta:
         Retroceso, Tabulación, Fin, Inicio, Izquierda, Arriba, Derecha, Abajo, Suprimir, 
         Copiar (Ctrl + C = 99), Recargar (Ctrl + R = 114), Cortar (Ctrl + X = 120),
         Rehacer (Ctrl + Y = 121) Deshacer (Ctrl + Z = 122). 
         */

        else if (key === 8 || key === 9 || key === 35 || key === 36 || key === 37 || key === 38 || key === 39 || key === 40 || key === 46 ||
            key === 99 && ctrl || key === 114 && ctrl || key === 120 && ctrl || key === 121 && ctrl || key === 122 && ctrl) {
            return;
        }

        let ch = String.fromCharCode(key);
        if (!/[0-9]/.test(ch)) { // Detectar por medio de REGEX si es un número lo ingresado.
            evt.preventDefault();
        }

    }
}

function pegar(event) {

    var allow = $("input[name='Nacionalidad']:checked").val();

    if (allow === "Nacional") {

         // El código a continuación intercpeta lo que se procede a pegar.
        let data = (event.clipboardData || window.clipboardData).getData('text');
        let ch = String.fromCharCode(data);

        // El código a continuación valida si lo que se va a pegar es un número.
        if (isNaN(data)) { 
            window.alert("Cédula nacional no acepta datos que no sean numéricos.\n Texto a pegar: '" + data + "'.");
            event.preventDefault();
        }

    }

}

function cls(max) { // Limpiar campo de cédula
    $('#Cedula').val('');
    document.getElementById("Cedula").maxLength = max; 
}

function brth(num) {             // 1 = BLUR      |       2 = KEYPRESSED

    var birth = $('#birth').val();

    var now = new Date();

    var yy = now.getFullYear();
    var month = now.getMonth() + 1;
    var mm = month < 10 ? "0" + month : month; // Month - 1
    var dd = now.getDate() < 10 ? "0" + now.getDate() : now.getDate();

    var edadMaxima = yy - 80;
    var edadMinima = yy - 10;

    yy = yy - 10;
    var date = yy + "-" + mm + "-" + dd;

    if (num === 2) { // Cambio realizado con el teclado.

        // CASO 1 = FECHA MAYOR A LA ACTUAL NO ES PERMITIDA.
        if (birth >= date) {
            $('#birth').val(date);
        }

    }

    else if (num === 1) { // Campo ya no seleccionado.

        // CASO 2 = FECHA MÁXIMA SON 80 AÑOS.
        if (birth <= edadMaxima + '-01-01') {
            window.alert(" La edad máxima dentro del sistema es de 80 años. ");
            $('#birth').val(date);
        }

        // CASO 3 = FECHA MÍNIMA SON 10 AÑOS.
        else if (birth >= edadMinima + '-01-01') {
            window.alert(" La edad mínima dentro del sistema es de 10 años. ");
            $('#birth').val(date);
        }

        // CASO 4 = Error en el lenguaje.
         // || birth > '19380-01-01'

    }

}

function EscondeSelecciones() {
    $('#info_atleta_selec').hide();
    $('#info_atleta_aso').show();
}

function EscondeAsociaciones() {
    $('#info_atleta_selec').show();
    $('#info_atleta_aso').hide();
}