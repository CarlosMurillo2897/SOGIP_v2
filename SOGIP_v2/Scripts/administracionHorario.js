$(document).ready(function () {
    $('#btnClear').click(function () {
        if (confirm("Desea limpiar los campos?")) {
            /*Clear all input type="text" box*/
            $('#inicio_fecha').val('');
            $('#incio_horaT').val('');
            $('#final_fecha').val('');
            $('#final_hora').val('');

        }
    });
});