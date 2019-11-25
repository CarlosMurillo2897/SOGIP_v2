function tablaPrin() {
    var d = @viewDataMaquina;
    data = {
        id: d
    }
    $('#maquinas').DataTable().destroy();
    $('#maquinas').remove();
    $('#tablaPrincipal').show();
    var tabla = $('<table/>', { id: 'maquinas', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Nombre</th><th>Acción</th></tr></thead>');
    $('#tablaPrincipal').append(tabla);
    table5 = $('#maquinas').DataTable({
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
            "url": '/SOGIP/TipoME/getEjercicios',
            "type": "GET",
            "data": data,
            "dataSrc": ""
        },
        columns: [
            { data: "Nombre" },
            {
                data: "Id",

                render: function (Id) {
                    return "<a class='btn btn-info' onclick='EnviarDes(" + Id + ")' style='padding: 2px 6px; margin: 2px;'>" +
                        "<text class='hidden-xs'>Ver </text>" +
                        "<span class='glyphicon glyphicon-sd-video'></span>" +
                        "</a>";
                }
            }
        ],
    });
}
function EnviarDes(id) {
    var tr = $('#boton_' + id).closest('tr');
    if (id === 0) {
        alert('Archivo predeterminado, no se puede ver video.');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/SOGIP/TipoME/Descripcion",
        data: { id: id },
        success: function (data) {
            $('#one').attr('src', data);
            $('#modal').modal('show');

        },
        error: function (data) {
            alert("No hay Video");
        }
    });
}

$(document).ready(function () {
    tablaPrin();

});