var cat1 = null
var cat2 = null;
var cat3 = null;
function tablaPrin() {
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
            "url": '/SOGIP/TipoME/getMaquinas',
            "type": "GET",
            "dataSrc": ""
        },
        columns: [
            { data: "Nombre" },
            {
                data: "Id",
                render: function (Id) {
                    return "<a class='btn btn-primary' style='padding: 2px 6px; margin: 2px;' id='boton_" + Id + "' onclick='EnviarRut(" + Id + ")''>" +
                        "<text class='hidden-xs'>Ejercicio </text>" +
                        "<span class='glyphicon glyphicon-list'></span>" +
                        "</a>";
                }
            }
        ],
    });
}

function EnviarRut(id) {

    window.location.href = '/SOGIP/TipoME/MaquinaEjercicio?id=' + id;
}

$(document).ready(function () {
    tablaPrin();

    $('#btnEjer').click(function () {
        $('#maquinas').show();
        tablaPrin();
    })
});