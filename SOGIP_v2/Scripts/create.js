$(document).ready(function () {

    $('#selectedRoles').change(function () { formulario(); });

});

function formulario() {
    var rol_selected = $('#selectedRoles option:selected').val();

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

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            break;

        case "Atleta":
            $('#title_atleta').show();
            $('#info_atleta_tipo').show();
            $('#info_atleta_selec').show();
            $('#info_atleta_aso').show();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
            break;

        case "Atleta Becados":
            $('#title_atleta').show();
            $('#info_atleta_tipo').show();
            $('#info_atleta_selec').show();
            $('#info_atleta_aso').show();

            $('#title_aso').hide();
            $('#info_aso').hide();

            $('#title_entidad').hide();
            $('#info_entidad').hide();

            $('#title_icoder').hide();
            $('#info_icoder').hide();

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
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

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
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

            $('#title_seleccion').hide();
            $('#info_seleccion').hide();
            $('#info_seleccion_nombre').hide();
            break;

        case "Funcionarios ICODER":
            $('#title_icoder').show();
            $('#info_icoder').show();

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
            break;

        case "Seleccion/Federacion":
            $('#title_seleccion').show();
            $('#info_seleccion').show();
            $('#info_seleccion_nombre').show();

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

            $('#title_entrenador').hide();
            $('#info_entrenador').hide();
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
            break;

    }

}

function isNumber(e) {  // Aceptar solo números en la cédula

    var allow = $("input[name='Nacionalidad']:checked").val();
    var ced = $("#Cedula").val().length;

    if (allow == "Nacional") {
            e = e || window.event;
            var charCode = e.which ? e.which : e.keyCode;
            return /\d/.test(String.fromCharCode(charCode));
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

jQuery.extend(jQuery.validator.messages, { // Cambio de mensaje.
    required: "Este campo es requerido",
    minlength: jQuery.validator.format("Este campo requiere de al menos 2 caracteres.")
});
