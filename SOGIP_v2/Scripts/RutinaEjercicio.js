function EliminarEjer(id) {
    var tr = $('#boton_' + id).closest('tr');
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/Rutinas/DeleteEjercicio',
        data: {
            'n':  @viewDataRutina,
    'ejercicioId': id
},
success: function (data) {

    location.reload();

},
error: function () {
    alert("Fallo");
}
            })
        }
function EditarEjer(id) {
    var ejer = null;
    $.ajax({
        type: "POST",
        dataType: "JSON",
        url: '/Rutinas/ObtenerEjer',
        data: { 'ejercicioId': id },
        success: function (data) {
            $('#ejer').val(data.EjercicioId.Nombre);
            $('#s1').val(data.Serie1);
            $('#r1').val(data.Repeticion1);
            $('#p1').val(data.Peso1);
            $('#s2').val(data.Serie2);
            $('#r2').val(data.Repeticion2);
            $('#p2').val(data.Peso2);
            $('#s3').val(data.Serie3);
            $('#r3').val(data.Repeticion3);
            $('#p3').val(data.Peso3);
            $('#Dia').val(data.DiaEjercicio);
            $('#Color').val(data.ColorId.Nombre);
        },
        error: function () {
            alert("Fallo");
        }
    })
    $('#btnEdit').click(function () {
        ejer = {
            'n':  @viewDataRutina,
        'data': {
            Conjunto_EjercicioId: id,
                EjercicioId: { 'Nombre': $('#ejer').val() },
            Serie1: $('#s1').val(),
                Repeticion1: $('#r1').val(),
                    Peso1: $('#p1').val(),
                        Serie2: $('#s2').val(),
                            Repeticion2: $('#r2').val(),
                                Peso2: $('#p2').val(),
                                    Serie3: $('#s3').val(),
                                        Repeticion3: $('#r3').val(),
                                            Peso3: $('#p3').val(),
                                                DiaEjercicio: $('#Dia').val(),
                                                    ColorId: { 'Nombre': $('#Color').val() }
        }
    }

                $.ajax({
            type: "POST",
            dataType: "JSON",
            url: '/Rutinas/EditEjer',
            data: ejer,
            success: function (data) {

                location.reload();


            },
            error: function () {
                alert("Fallo");
            }
        })
            })
        }

function tablaEjercicios() {
    $('#ejercicios').DataTable().destroy();
    $('#ejercicios').remove();
    $('#tablePrin').show();
    var tabla = $('<table/>', { id: 'ejercicios', class: 'table table-striped table-bordered dt-responsive nowrap', width: '100%' }).append('<thead><tr><th>Acción</th><th>Nombre</th></tr></thead>');
    $('#tablePrin').append(tabla);
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
            "url": "/TipoME/GetEjerMaquina",
            "type": "GET",
            "dataSrc": ""
        },
        columns: [
            { data: "Accion" },
            { data: "Nombre" },
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
}

//Insert new rows into table
$(document).ready(function () {

    var input10 = null;
    var colores = [];
    var colors = [];
    var ejercicios = [];
    var ejercicio = [];
    obtenerColores();
    obtnerEjercicios();

    function obtenerColores() {
        var select = document.getElementById("Color");
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: '/Rutinas/ObtenerColores',
            success: function (data) {
                $.each(data, function (i, v) {
                    select.options[i] = new Option(v.Nombre, v.Identificador);
                    colores.push([v.Nombre]);
                    colors.push([v.Identificador]);
                })

            },
            error: function () {
                alert("Fallo");
            }
        })

    }
    function obtnerEjercicios() {
        var select = document.getElementById("ejer");
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/TipoME/GetEjerMaquina",
            success: function (data) {
                $.each(data, function (i, v) {
                    select.options[i] = new Option(v.Nombre, v.Identificador);
                    ejercicios.push([v.Nombre]);
                    ejercicio.push([v.Id]);
                });

            },
            error: function () {
                alert("Fallo");
            }
        })
    }


    $("#adicional").on('click', function () {

        var tableRef = document.getElementById('tabla').getElementsByTagName('tbody')[0];

        // Insert a row in the table at the last row
        var newRow = tableRef.insertRow(tableRef.rows.length);

        // Insert a cell in the row at index 0
        var newCell = newRow.insertCell(0);
        var newCell1 = newRow.insertCell(1);
        var newCell2 = newRow.insertCell(2);
        var newCell3 = newRow.insertCell(3);
        var newCell4 = newRow.insertCell(4);
        var newCell5 = newRow.insertCell(5);
        var newCell6 = newRow.insertCell(6);
        var newCell7 = newRow.insertCell(7);
        var newCell8 = newRow.insertCell(8);
        var newCell9 = newRow.insertCell(9);
        var newCell10 = newRow.insertCell(10);
        var newCell11 = newRow.insertCell(11);
        var newCell12 = newRow.insertCell(12);



        var input1 = document.createElement("input");
        input1.type = "text"; input1.min = 0; input1.pattern = "[a-z]"; input1.title = "Palabras únicamente."

        var input2 = document.createElement("input");
        input2.type = "text"; input2.min = 0; input2.pattern = "[a-z]"; input2.title = "Palabras únicamente."

        var input3 = document.createElement("input");
        input3.type = "text"; input3.min = 0; input3.pattern = "[a-z]"; input3.title = "Palabras únicamente."

        var input4 = document.createElement("input");
        input4.type = "text"; input4.min = 0; input4.pattern = "[a-z]"; input4.title = "Palabras únicamente."

        var input5 = document.createElement("input");
        input5.type = "text"; input5.min = 0; input5.pattern = "[a-z]"; input5.title = "Palabras únicamente."

        var input6 = document.createElement("input");
        input6.type = "text"; input6.min = 0; input6.pattern = "[a-z]"; input6.title = "Palabras únicamente."

        var input7 = document.createElement("input");
        input7.type = "text"; input7.min = 0; input7.pattern = "[a-z]"; input7.title = "Palabras únicamente."

        var input8 = document.createElement("input");
        input8.type = "text"; input8.min = 0; input8.pattern = "[a-z]"; input8.title = "Palabras únicamente."

        var input9 = document.createElement("input");
        input9.type = "text"; input9.min = 0; input9.pattern = "[a-z]"; input9.title = "Palabras únicamente."

        input10 = document.createElement("input");
        input10.type = "text";
        input10.className = 'input10';

        var a = document.createElement('select');
        a.type = 'select';
        for (var i = 0; i < ejercicios.length; i++) {
            var option = document.createElement('option');
            option.value = ejercicios[i];
            option.text = ejercicios[i];
            a.add(option);

        }

        var c = document.createElement('select');
        c.type = 'select';
        for (var i = 0; i < colores.length; i++) {
            var option = document.createElement('option');
            option.value = colores[i];
            option.text = colores[i];
            c.add(option);
        }

        var dias = ["Dia1", "Dia2", "Dia3", "Dia4"];
        var d = document.createElement('select');
        d.type = 'select';
        for (var i = 0; i < dias.length; i++) {
            var option = document.createElement('option');
            option.value = dias[i];
            option.text = dias[i];
            d.add(option);
        }



        var checkBox = document.createElement('input');
        checkBox.type = 'button';
        checkBox.className = 'btn btn-danger';
        checkBox.value = '-';
        checkBox.name = 'checkbox';




        newCell.appendChild(a);
        newCell1.appendChild(input1);
        newCell2.appendChild(input2);
        newCell3.appendChild(input3);
        newCell4.appendChild(input4);
        newCell5.appendChild(input5);
        newCell6.appendChild(input6);
        newCell7.appendChild(input7);
        newCell8.appendChild(input8);
        newCell9.appendChild(input9);
        newCell10.appendChild(c);
        newCell11.appendChild(d);
        newCell12.appendChild(checkBox);

    });
    var td1 = null;
    var tr1 = null;
    var nomejer = null;
    //$('#tabla tbody').on('click', '.input10', function () {
    //    $('#edit').css("display", "none");
    //    $('#agr').css("display", "block");
    //    tablaEjercicios();
    //    td1 = null;
    //    tr1 = null;
    //    $('#nueva').modal('show');
    //    td1 = $(this);
    //    tr1 = td1.closest('tr');


    //});

    //$('#tablePrin').on('click', 'td.select-checkbox', function () {
    //    nomejer = null;
    //    var td = $(this);
    //    var tr = td.closest('tr');
    //    nomejer = tr.find('td:eq(1)').text();
    //    if ($('#agr').is(':visible')) {
    //        $('#nueva').modal('hide');
    //        tr1.find('.input10').val(nomejer);
    //    } else {
    //        $('#nueva').modal('hide');
    //        $('#ejer').val(nomejer);
    //    }
    //});

    $('#tabla tbody').on('click', '.btn-danger', function () {
        var td1 = $(this);
        var tr1 = td1.closest('tr').remove();
    });


    $('#submit').click(function () {

        var contador = null;
        var table1 = document.getElementById("tabla");
        var ejer = null;
        for (var i = 2; i < table1.rows.length; i++) {
            ejer = table1.rows[i].cells[0].children[0].value;
            if (ejer == '') {
                contador = 0;
            }
        }

        if (contador == null) {

            var table = document.getElementById("tabla");
            var tableArr = [];
            var n = null;
            for (var i = 2; i < table.rows.length; i++) {

                tableArr.push({
                    EjercicioId: { 'Nombre': table.rows[i].cells[0].children[0].value },
                    Serie1: table.rows[i].cells[1].children[0].value,
                    Repeticion1: table.rows[i].cells[2].children[0].value,
                    Peso1: table.rows[i].cells[3].children[0].value,
                    Serie2: table.rows[i].cells[4].children[0].value,
                    Repeticion2: table.rows[i].cells[5].children[0].value,
                    Peso2: table.rows[i].cells[6].children[0].value,
                    Serie3: table.rows[i].cells[7].children[0].value,
                    Repeticion3: table.rows[i].cells[8].children[0].value,
                    Peso3: table.rows[i].cells[9].children[0].value,
                    ColorId: { 'Nombre': table.rows[i].cells[10].children[0].value },
                    diaEjercicio: table.rows[i].cells[11].children[0].value
                });
            }

            var datos = {
                'data': @viewDataRutina,
            'ejercicios': tableArr
        }

        function delrows() {
            var row = $('#tabla tbody td').remove();
            var row = $('#tabla tbody td').empty();

        }
        $.ajax({
            url: '/Rutinas/Ejercicio',
            dataType: 'JSON',
            type: 'POST',
            data: JSON.stringify(datos),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert('Ejercicios Registrados');
                delrows();
                location.reload();


            },
            error: function (request) {
                alert('Hubo un error');
            }
        });
    } else {
            alert('Debe seleccionar todos los Ejercicios');
        }
                contador = null;

});

            //$('#ejer').click(function () {
            //    $('#agr').css("display", "none");
            //    $('#edit').css("display", "block");
            //    tablaEjercicios();
            //    $('#nueva').modal('show');
            //});


       });