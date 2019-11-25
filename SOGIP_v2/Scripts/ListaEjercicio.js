function EnviarDes(id) {
    var idEjer = null;
    var tr = $('#boton_' + id).closest('tr');
    if (id === 0) {
        alert('Archivo predeterminado, no se puede ver video.');
        return;
    }
    $.ajax({

        type: "POST",
        url: "/SOGIP/Rutinas/getNombreEjer",
        data: { id: id },
        success: function (data) {

            idEjer = data.EjercicioId.Descripcion;
            $('#one').attr('src', idEjer);
            $('#modal').modal('show');

        },
        error: function (data) {
            alert("¡Error!");
        }
    });
   
}
$(document).ready(function () {
});