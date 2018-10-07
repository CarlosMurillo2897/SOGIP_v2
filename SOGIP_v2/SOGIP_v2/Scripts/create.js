$(document).ready(function () {

    $('#selectedRoles').change(function () { formulario(); });

});



function formulario() {
    var rol_selected = $('#selectedRoles option:selected').val();
    switch (rol_selected) {
        case "Asociacion/Comite":
            $('#title_aso').show();
            $('#info_aso').show();
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
        case "Atleta" || "Atleta Alto Rendimiento":
            $('#title_atleta').show();
            $('#info_atleta_tipo').show(); //selección o asociación
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
            break;
        case "Atleta Alto Rendimiento":
            $('#title_atleta').show();
            $('#info_atleta_tipo').show(); //selección o asociación
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
            break;
        case "Entrenador":
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
            break;
    }
}
