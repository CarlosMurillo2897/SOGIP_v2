﻿var cat1 = null
var maq1 = null;
var ejer1 = null;
var idEdit = null;
function tablaEjer1() {  //nuevo
    cat1 = 0;
    $('#textPrin').html("Categoria");
    $('#tipoEjer1').DataTable().destroy();
    $('#tipoEjer1').remove();
    $('#tabla1').show();
    var tabla = $('<table/>', { id: 'tipoEjer1', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Nombre</th><th>Acción</th></tr></thead>');
    $('#tabla1').append(tabla);
    table = $('#tipoEjer1').DataTable({
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
            "url": '/SOGIP/TipoME/getCategorias',
            "type": "GET",
            "dataSrc": ""
        },
        columns: [
            { data: "nombre" },
            {
                data: "Id",

                render: function (Id) {
                    return "<a class='btn btn-warning' style='padding: 2px 6px; margin: 2px;'  id='boton_" + Id + "' onclick='EditarCat(" + Id + ")'data-toggle='modal'  data-target='#editar'>" +
                        "<text class='hidden-xs'>Editar </text>" +
                        "<span class='glyphicon glyphicon-pencil'></span>" +
                        "</a>" +
                        "<a class='btn btn-info' style='padding: 2px 6px; margin: 2px;' id='boton_" + Id + "' onclick='EnviarMaquina(" + Id + ")'>" +
                        "<text class='hidden-xs'> Maquina </text>" +
                        "<span class='glyphicon glyphicon-list'></span>" +
                        "</a>" +
                        "<a class='btn btn-info' style='padding: 2px 6px; margin: 2px;' id='boton_" + Id + "' onclick='EnviarEjercicio(" + Id + ")'>" +
                        "<text class='hidden-xs'> Ejercicio</text>" +
                        "<span class='glyphicon glyphicon-list'></span>" +
                        "</a>";
                }
            }
        ],
    });
}

function tablaMaquina(id) {//nuevo
    $('#maquinas').DataTable().destroy();
    $('#maquinas').remove();
    $('#tabla2').show();
    var tabla = $('<table/>', { id: 'maquinas', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Nombre</th><th>Acción</th></tr></thead>');
    $('#tabla2').append(tabla);
    table1 = $('#maquinas').DataTable({
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
            "url": '/SOGIP/TipoME/Maquinas',
            "data": { 'id': id },
            "type": "GET",
            "dataSrc": ""
        },
        columns: [
            { data: "Nombre" },
            {
                data: "Id",
                render: function (Id) {
                    return "<a class='btn btn-warning' style='padding: 2px 6px; margin: 2px;'  id='boton_" + Id + "' onclick='EditarMaq(" + Id + ")' data-toggle='modal' data-target='#editar'>" +
                        "<text class='hidden-xs'>Editar </text>" +
                        "<span class='glyphicon glyphicon-pencil'></span>" +
                        "</a>" +
                        "<a class='btn btn-primary' style='padding: 2px 6px; margin: 2px;' id='boton_" + Id + "' onclick='EnviarRut(" + Id + ")''>" +
                        "<text class='hidden-xs'>Ejercicio </text>" +
                        "<span class='glyphicon glyphicon-list'></span>" +
                        "</a>";
                }
            }
        ],
    });

}
function tablaEjercicio(id) {//nuevo
    $('#ejercicios').DataTable().destroy();
    $('#ejercicios').remove();
    $('#tabla3').show();
    var tabla = $('<table/>', { id: 'ejercicios', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Nombre</th><th>Acción</th></tr></thead>');
    $('#tabla3').append(tabla);
    table1 = $('#ejercicios').DataTable({
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
            "url": '/SOGIP/TipoME/EjerciciosList',
            "data": { 'id': id },
            "type": "GET",
            "dataSrc": ""
        },
        columns: [
            { data: "Nombre" },
            {
                data: "Id",
                render: function (Id) {
                    return "<a class='btn btn-warning' style='padding: 2px 6px; margin: 2px;'  id='boton_" + Id + "' onclick='EditarEjer(" + Id + ")' data-toggle='modal' data-target='#editar'>" +
                        "<text class='hidden-xs'>Editar </text>" +
                        "<span class='glyphicon glyphicon-pencil'></span>" +
                        "</a>";
                }
            }
        ],
    });

}
function eliminar() {
    $('#tabla1').hide();

}
function EnviarRut(id) {

    window.location.href = '/SOGIP/TipoME/Ejercicios?id=' + id;
}

function EnviarMaquina(id) { // nuevo
    $('#txtnuevo').css("display", "none");
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/getNombre',
        data: { 'id': id },
        success: function (data) {
            $('#cat').css("display", "none");
            $('#ejer').css("display", "none");
            $('#textPrin').html(data.nombre);
            $('#btn2').show();
            $('#btn3').hide();
            $('#txtmaq').css("display", "block");
            $('#txtejer').css("display", "none");
            $('#maq').css("display", "block");
            $('#text2').html('Máquina' + ' ' + data.nombre);
            maq1 = id;
            cat1 = id;
        },
        error: function () {
            alert("Fallo");
        }
    })
    eliminar();
    tablaMaquina(id);
}
function EnviarEjercicio(id) {//nuevo
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/getNombre',
        data: { 'id': id },
        success: function (data) {
            $('#txtnuevo').css("display", "none");
            $('#cat').css("display", "none");
            $('#maq').css("display", "none");
            $('#textPrin2').html(data.nombre);
            $('#btn2').hide();
            $('#btn3').show();
            $('#txtmaq').css("display", "none");
            $('#txtejer').css("display", "block");
            $('#ejer').css("display", "block");
            $('#text3').html('Ejercicio' + ' ' + data.nombre);
            ejer1 = id;
            cat1 = id;
        },
        error: function () {
            alert("Fallo");
        }
    })
    eliminar();
    tablaEjercicio(id);
}

function EditarCat(id) {
    idEdit = id;
    $('#nuevo').modal('show');
    $('#txtnuevo').css("display", "none");
    $('#editarejer').css("display", "none");
    $('#editarmaq').css("display", "none");
    $('#editarcat').css("display", "block");
    $('#ejer').css("display", "none");
    $('#cat').css("display", "block");
    $('#maq').css("display", "none");
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/getNombre',
        data: { 'id': id },
        success: function (data) {
            $('#cat').val(data.nombre);
        }
    })

}
function EditarMaq(id) {
    idEdit = id;
    $('#nuevo').modal('show');
    $('#ejer').css("display", "none");
    $('#cat').css("display", "none");
    $('#maq').css("display", "block");
    $('#txtmaq').css("display", "none");
    $('#editarmaq').css("display", "block");
    $('#editarejer').css("display", "none");
    $('#editarcat').css("display", "none");
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/getNombreMaq',
        data: { 'id': id },
        success: function (data) {
            $('#maq').val(data.Nombre);
        }
    })

}
function EditarEjer(id) {
    idEdit = id;
    $('#nuevo').modal('show');
    $('#ejer').css("display", "block");
    $('#cat').css("display", "none");
    $('#maq').css("display", "none");
    $('#txtejer').css("display", "none");
    $('#editarejer').css("display", "block");
    $('#editarmaq').css("display", "none");
    $('#editarcat').css("display", "none");
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/getNombreEjer',
        data: { 'id': id },
        success: function (data) {
            $('#ejer').val(data.Nombre);
        }
    })

}
$('#btnCategorias').click(function () {
    $('#editarejer').css("display", "none");
    $('#editarmaq').css("display", "none");
    $('#editarcat').css("display", "none");
    $('#txtnuevo').css("display", "block");
    $('#txtejer').css("display", "none");
    $('#txtmaq').css("display", "none");
    $('#tabla2').hide();
    $('#tabla3').hide();
    $('#tipoEjer2').DataTable().destroy();
    $('#tipoEjer2').remove();
    $('#tipoEjer3').DataTable().destroy();
    $('#tipoEjer3').remove();
    $('#tabla1').show();
    $('#btn2').css("display", "none");
    $('#btn3').css("display", "none");
    tablaEjer1();

})
$('#btn2').click(function () {
    $('#txtnuevo').css("display", "none");
    $('#cat').css("display", "none");
    $('#ejer').css("display", "none");
    $('#textPrin').html(data.nombre);
    $('#btn2').show();
    $('#btn3').hide();
    $('#txtmaq').css("display", "block");
    $('#txtejer').css("display", "none");
    $('#maq').css("display", "block");
    eliminar();
    tablaMaquina(maq1);
})
$('#btn3').click(function () {
    $('#txtnuevo').css("display", "none");
    $('#cat').css("display", "none");
    $('#maq').css("display", "none");
    $('#textPrin2').html(data.nombre);
    $('#btn2').hide();
    $('#btn3').show();
    $('#txtmaq').css("display", "none");
    $('#txtejer').css("display", "block");
    $('#ejer').css("display", "block");
    eliminar();
    tablaEjercicio(id);
})
function SaveMaquina(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/SaveMaquina',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#maquinas').show();
            $('#maquinas').DataTable().ajax.reload(null, false);
            $('#maq').val('');
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function SaveEjercicio(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/SaveEjercicio',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#ejercicios').show();
            $('#ejercicios').DataTable().ajax.reload(null, false);
            $('#ejer').val('');

        },
        error: function () {
            alert("Fallo");
        }
    })
}
function SaveCategoria(data) { // nuevo
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/SaveCategoria',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#tipoEjer1').DataTable().ajax.reload(null, false);
            $('#cat').val('');

        },
        error: function () {
            alert("Fallo");
        }
    })
}

function EditarCategoria(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/EditCategoria',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#tipoEjer1').DataTable().ajax.reload(null, false);
            $('#cat').val('');
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function EditarMaquina(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/EditMaquina',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#maquinas').DataTable().ajax.reload(null, false);
            $('#maq').val('');
        },
        error: function () {
            alert("Fallo");
        }
    })
}

function EditarEjercicio(data) {
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/SOGIP/TipoME/EditEjercicio',
        data: data,
        success: function (data) {
            $('#nuevo').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#ejercicios').DataTable().ajax.reload(null, false);
            $('#ejer').val('');
        },
        error: function () {
            alert("Fallo");
        }
    })
}
function enviar() {
    if ($('#txtnuevo').is(':visible')) {
        if (cat1 == 0) {
            var data = null;
            data = {
                categoria: $('#cat').val()
            }
            SaveCategoria(data);
        }
    } else {
        if ($('#textPrin').is(':visible')) {
            var data = null;
            data = {
                id: cat1,
                nombre: $('#maq').val()
            }
            SaveMaquina(data);
        } else {
            if ($('#textPrin2').is(':visible')) {
                var data = null;
                data = {
                    id: cat1,
                    nombre: $('#ejer').val()
                }
                SaveEjercicio(data);
            } else {
                if ($('#editarcat').is(':visible')) {
                    data = {
                        id: idEdit,
                        categoria: $('#cat').val()
                    }
                    EditarCategoria(data);
                }
                else {
                    if ($('#editarejer').is(':visible')) {
                        data = {
                            id: idEdit,
                            categoria: $('#ejer').val()
                        }
                        EditarEjercicio(data);
                    } else {
                        data = {
                            id: idEdit,
                            categoria: $('#maq').val()
                        }
                        EditarMaquina(data);
                    }
                }
            }
        }
    }
}

$.validator.setDefaults({
    submitHandler: function () {
        enviar();
    }
});

$(document).ready(function () {
    $('#txtnuevo').css("display", "block");
    tablaEjer1();

    $("#forcat").validate({
        rules: {
            cat: {

                required: true,
                minlength: 4,
                remote: {
                    url: '/SOGIP/TipoME/TipoRepetido',
                    type: 'GET',
                    data: {
                        nombre: function () {
                            return $('#cat').val();
                        }
                    }
                }
            },
            ejer: {
                required: true,
                minlength: 4,
                remote: {
                    url: '/SOGIP/TipoME/EjercicioRepetido',
                    type: 'GET',
                    data: {
                        nombre: function () {
                            return $('#ejer').val();
                        }
                    }
                }
            },
            maq: {
                required: true,
                minlength: 4,
                remote: {
                    url: '/SOGIP/TipoME/MaquinaRepetido',
                    type: 'GET',
                    data: {
                        nombre: function () {
                            return $('#maq').val();
                        }
                    }
                }
            }

        },
        messages: {
            cat: {
                required: "El nombre de Categoría es un campo obligatorio.",
                minlength: "El nombre de Categoría debe de tener mas de 4 caracteres.",
                remote: "El nombre no debe ser Repetido."
            },
            ejer: {
                required: "El nombre  de Ejercicio  es un campo obligatorio.",
                minlength: "El nombre  de Ejercicio debe de tener mas de 4 caracteres.",
                remote: "El nombre de Ejercicio no debe ser Repetido."
            },
            maq: {
                required: "El nombre  de Máquina  es un campo obligatorio.",
                minlength: "El nombre  de Máquina debe de tener mas de 4 caracteres.",
                remote: "El nombre de Máquina no debe ser Repetido."
            }
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("help-block");
            element.parents(".afect").addClass("has-feedback");

            if (element.prop("id") === "nac") {
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

    $('#btnNuevo').click(function () {
        if ($('#btn2').is(':visible')) {
            $('#nuevo').modal('show');
            EnviarMaquina(maq1);
            $('#textPrin').css("display", "block");
            $('#textPrin2').css("display", "none");
            $('#txtnuevo').css("display", "none");
            $('#editarcat').css("display", "none");
            $('#editarejer').css("display", "none");
            $('#editarmaq').css("display", "none");
            $('#cat').val('');
            $('#ejer').val('');
            $('#maq').val('');
        } else {
            if ($('#btn3').is(':visible')) {
                $('#nuevo').modal('show');
                EnviarEjercicio(ejer1);
                $('#textPrin').css("display", "none");
                $('#textPrin2').css("display", "block");
                $('#txtnuevo').css("display", "none");
                $('#editarcat').css("display", "none");
                $('#editarejer').css("display", "none");
                $('#editarmaq').css("display", "none");
                $('#cat').val('');
                $('#ejer').val('');
                $('#maq').val('');
            } else {
                $('#nuevo').modal('show');
                $('#textPrin').css("display", "none");
                $('#textPrin2').css("display", "none");
                $('#txtnuevo').css("display", "block");
                $('#editarcat').css("display", "none");
                $('#editarejer').css("display", "none");
                $('#editarmaq').css("display", "none");
                $('#cat').val('');
                $('#ejer').val('');
                $('#maq').val('');
            }
        }
    })

});