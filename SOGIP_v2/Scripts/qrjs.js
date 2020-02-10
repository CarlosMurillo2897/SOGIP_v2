function Save(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/ControlIngreso/SaveIngreso',
        data: data,
        success: function (data) {
            $('#principal').hide();
            $('#ingreso').show();
            var date = new Date(parseInt(data.Fecha.substr(6)));
            $('#usuario').val(data.Usuario.Nombre1 + " " + data.Usuario.Apellido1 + " " + data.Usuario.Apellido2);
            $('#fecha').val(date.toLocaleDateString("en-GB"));
        },
        error: function () {
            bootbox2(" No hay ningún usuario asociado a ese QR");
        }
    })
}
$(document).ready(function () {
    $('#ingreso').hide();

    $('#btnSave').click(function () {

        var data = null;
        data = {
            id: $('#obj').val()
        }
        Save(data);
    })
    $('#btnNuevo').click(function () {
        $('#obj').val('');
        $('#ingreso').hide();
        $('#principal').show();
    })
});

function bootbox2(message) {//para errores
    bootbox.alert({
        size: 'small',
        closeButton: false,
        message: '<p><i class="fa fa-exclamation-triangle"></i>' + message + '</p>'
    });

}