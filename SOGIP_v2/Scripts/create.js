$(document).ready(function () {
    $('#selectedRoles').val('Atleta');
    formulario();

    $('#selectedRoles').change(
        function check() {

            if ($('#selectedRoles').val() == 'Administrador' || $('#selectedRoles').val() == 'Supervisor') {
                
                if (!confirm("Está a punto de crear un Administrador o Supervisor. ¿Continuar?")) {
                    $('#selectedRoles').val('Atleta');
                    alert('Cambiando a rol tipo Atleta.');
                }
            }
                formulario();
            
        });

});

var direccion;

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
            direccion = "/UsersAdmin/getEntrenador2";
            fillDT(direccion);
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
            direccion = "/UsersAdmin/getEntrenador";
            fillDT(direccion);
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

//Elección de entrenadores
function dataTable(direccion) {
    var table;
    var dataSet = [];

    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: direccion,
        success: function (data) {
                $.each(data, function (i, v) {
                dataSet.push(["", v.Cedula, v.Nombre1, v.Apellido1, v.Apellido2]);
            })
            table = $('#example').DataTable({

                // "aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],
                // "iDisplayLength": 5,
                "language": {
                    "lengthMenu": "Mostrando MENU resultados por página.",
                    "zeroRecords": "No se han encontrado resultados.",
                    "info": "Mostrando página PAGE de PAGES.",
                    "infoEmpty": "No hay datos para mostrar",
                    "infoFiltered": "(filtrado de MAX datos obtenidos).",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
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
            alert("Fallo");
        }
    });

    $('#example').on('click', 'td.select-checkbox', function () { //usar
        alert("hola");
    });  

    $('#btnSave').click(function () {

        var tblData = table.rows('.selected').data();
        $.each(tblData, function (i, val) {
            var r = confirm("Desea realizar el cambio?");

            if (r == true) {
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


function fillDT(direccion) {
    var isE = $('#example').DataTable();
    isE.destroy(); //es mejor destruirla para poder incializarla
    dataTable(direccion);
  

}

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

//******************************************************************************//

function isNumber(evt) {  // Aceptar solo números y otros comandos en la cédula.

    var allow = $("input[name='Nacionalidad']:checked").val();

    if (allow == "Nacional") {

        var key = evt.which || evt.keyCode;

        // Detectar si Ctrl fue ingresado.
        var ctrl = evt.ctrlKey ? evt.ctrlKey : ((key === 17) ? true : false); 

        // El código a continuación detecta si se va a Pegar(Ctrl + V = 118) algún texto y se valida que sea numérico.
        if (key == 118 && ctrl) { 
            pegar(evt);
        }

         /* 
         El código a continuación acepta:
         Retroceso, Tabulación, Fin, Inicio, Izquierda, Arriba, Derecha, Abajo, Suprimir, 
         Copiar (Ctrl + C = 99), Recargar (Ctrl + R = 114), Cortar (Ctrl + X = 120),
         Rehacer (Ctrl + Y = 121) Deshacer (Ctrl + Z = 122). 
         */

        else if (key == 8 || key == 9 || key == 35 || key == 36 || key == 37 || key == 38 || key == 39 || key == 40 || key == 46 ||
            (key == 99 && ctrl) || (key == 114 && ctrl) || (key == 120 && ctrl) || (key == 121 && ctrl) || (key == 122 && ctrl)) {
            return
        }

        let ch = String.fromCharCode(key);
        if (!(/[0-9]/.test(ch))) { // Detectar por medio de REGEX si es un número lo ingresado.
            evt.preventDefault();
        }

    }
}

function pegar(event) {

    var allow = $("input[name='Nacionalidad']:checked").val();

    if (allow == "Nacional") {

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
    var mm = (month < 10) ? "0" + month : month; // Month - 1
    var dd = (now.getDate() < 10) ? "0" + now.getDate() : now.getDate();

    var edadMaxima = yy - 80;
    var edadMinima = yy - 10;

    yy = yy - 10;
    var date = yy + "-" + mm + "-" + dd;

    if (num == 2) { // Cambio realizado con el teclado.

        // CASO 1 = FECHA MAYOR A LA ACTUAL NO ES PERMITIDA.
        if (birth >= date) {
            $('#birth').val(date);
        }

    }

    else if (num == 1) { // Campo ya no seleccionado.

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