$(document).ready(function () {
    $(".combo").select2({

        width: "resolve"
    });

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
            url: '/SOGIP/Rutinas/ObtenerColores',
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
            url: "/SOGIP/TipoME/GetEjerMaquina",
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
        a.className = "combo2";
        for (var i = 0; i < ejercicios.length; i++) {
            var option = document.createElement('option');
            option.value = ejercicios[i];
            option.text = ejercicios[i];
            a.add(option);

        }
        setTimeout(function () {
            $(".combo2").select2({
                width: "resolve"
            });
        }, 10)


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


    $('#tabla tbody').on('click', '.btn-danger', function () {
        var td1 = $(this);
        var tr1 = td1.closest('tr').remove();
    });


       });