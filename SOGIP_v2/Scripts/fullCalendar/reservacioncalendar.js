﻿function _alerta(inf) {
    var imp = inf;
    document.getElementById('lbl').innerHTML = imp;
    $('#volver').show();
    $('#aprobar').show();
    $('#rechazar').show();
    var table = $('#example').DataTable();
    table.destroy();
    $('#example').remove();

    var head = '<thead><tr><td>Día</td><td>Fecha</td><td>Hora de Inicio</td><td>Hora Final</td></tr></thead>';
    var tabla = $('<table/>', {
        id: 'example',
        class: 'table table-striped table-bordered dt-responsive'
    }).append(head);
    $('#tabla').append(tabla);

    
    var col = [
        { data: "Dia" },
        { data: "Fecha" },
        { data: "HoraI" },
        { data: "HoraF" }
    ];

    $('#example').DataTable({
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
            "url": "/Reservacion/getListaSolicitudes",
            "type": "POST",
            "dataSrc": "",
            "data": { userced: imp }
        },
        columns: col
    });
}
$(document).ready(function () {

    //variables
    lista();
    var cantidad;
    var events = [];
    var ar = [];
    $('#cant').show();
    FetchEventAndRenderCalendar();
    $('#txtStart').datepicker();
    $('#txtEnd').datepicker();
    $('input.timepicker').timepicker({
        'step': 60,
        'minTime': '6:00am',
        'maxTime': '7:00pm',
        'disableTextInput': true

    });
   

    //---habilitar/deshabilitar
    $("input[type=checkbox]").click(function () {
        hours();
        if ($("input[type=checkbox]").is(":checked")) {          
            $('#botón2').prop("disabled", false);
            console.log(ar);
        }
        else {
            $('#botón2').prop("disabled", true);
             console.log(ar);
        }
    });

   
 

//----FUNCIONES
    //guardar días para habilitar horas del modal
    function hours() {
        $("input:checkbox").each(function () {
            if ($(this).is(':checked')) {
                if (ar.indexOf((this).value) < 0) {
                    ar.push((this).value);
                }
            }
            else {
                var index = ar.indexOf((this).value);
                if (index > -1) {
                    ar.splice(index, 1);
                }
            }
        });
    }
    //hablitar horas del modal
    function ablehour(a) {
        if (a.length > 0) {
            $("div[name=d]").each(function () {
                if (String((this).id) == a[0]) {
                    this.hidden = false;
                }

            });
            a.shift();
            console.log(a);
            ablehour(a);
        }
    }
    
    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/Reservacion/GetReservaciones",
            success: function (data) {
                $.each(data, function (i, v) {
                    events.push({
                        reservacionId: v.ReservacionId,
                        title: 'RESERVACION',
                        estado: v.Estado.Descripcion,
                        cantidad: v.Cantidad,
                        cedula: v.UsuarioId.Cedula,
                        nombre: v.UsuarioId.Nombre1,
                        apellido1: v.UsuarioId.Apellido1,
                        apellido2: v.UsuarioId.Apellido2,
                        start: $.fullCalendar.moment(v.FechaHoraInicio),
                        end: $.fullCalendar.moment(v.FechaHoraFinal)
                    });
                })
                GenerateCalendar(events);
            },
            error: function (error) {
               // alert("Fallo");
            }
        })
    }


    function GenerateCalendar(events) {
        $('#calendar').fullCalendar('destroy');
        $('#calendar').fullCalendar({ //CREACIÓN DEL CALENDARIO
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev, next today',
                center: 'title',
                right: 'month, basicWeek, basicDay, agenda'
            },
            weekends: false,
            eventLimit: true,
            eventColor: '#339966',
            events: events,
            themeSystem: 'jquery-ui',

            selectable: true, //EVENTO SELECCIONABLE
            editable: false, //para editar
            eventDrop: function (event) { //MOVER EVENTOS
                var data = {
                    ReservacionId: event.reservacionId,
                    Estado: event.estado,
                    FechaHoraInicio: event.start.format('DD-MMM-YYYY HH:mm a'),
                    FechaHoraFinal: event.end.format('DD-MMM-YYYY HH:mm a')
                };
                SaveDate(data);
            },
            eventAfterRender: function (event, element, view) { //COLOR DE LOS EVENTOS



                if (event.estado=="EN PROCESO") {
                    //event.color = "##ff8533"; //Em andamento
                    element.css('background-color', '#ff8533');
                    element.css('border-color', '#ff8533');
                } else if (event.estado=="APROBADO") {
                    //event.color = "##339966"; //Aprobado
                    element.css('background-color', '#339966');
                    element.css('border-color', '#339966');
                }
            },
            eventClick: function (calEvent, jsEvent, view) { //INFORMACIÓN DEL EVENTO
               
                selectedEvent = calEvent;
                var check = calEvent.start.format("YYYY-MM-DD");
                var today = moment().format("YYYY-MM-DD");


                $('#myModal #eventTitle').text(calEvent.title);

                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Fecha: </b>' + calEvent.start.format("DD-MM-YYYY")));
                $description.append($('<p/>').html('<b>Hora: </b>' + calEvent.start.format("HH:mm a") + " - "+calEvent.end.format("HH:mm a")));
                
                $description.append($('<hr/>'));
                $description.append($('<p/>').html('<h4><b>Cantidad de asistentes</b></h4>'));
                $description.append($('<p/>').html( calEvent.cantidad));
                $description.append($('<hr/>'));
                $description.append($('<p/>').html('<h4><b>Estado</b></h4>'));
                $description.append($('<p/>').html(calEvent.estado));
                $description.append($('<hr/>'));
                $description.append($('<p/>').html('<h4><b>Solicitante</b></h4>'));
                $description.append($('<p/>').html('<b>Cédula: </b>' + calEvent.cedula));
                cedu = calEvent.cedula;
                $description.append($('<p/>').html('<b>Nombre Completo: </b>' + calEvent.nombre + ' ' + calEvent.apellido1 + ' ' + calEvent.apellido2));

                if (check < today) {
                    $('#btnEdit').prop('disabled', true);
                    $('#btnDelete').prop('disabled', true);

                }
                else {
                    $('#btnEdit').prop('disabled', false);
                    $('#btnDelete').prop('disabled', false);
                }

                $('#myModal #pDetails').empty().html($description);

                $('#myModal').modal();
            }
        })

    }


    //----BOTONES
    $('#botón2').click(function () {
        hours();
        $("div[name=d]").each(function () {          
                this.hidden = true;
        });
        ablehour(ar);
        $("div[name=d]").each(function () {
            if (this.hidden) {
                console.log(this.value);
            }
        });

        $('#myModalSave2').modal('show');
    });
    //array.shift() sirve para remover el primer elemento
    $('#botón').click(function () { $('#myModalSave').modal('show'); });
    $('#boton1').click(function () { $('#modaldt').modal('show'); tablalista(); });
    $('#btnSave').click(function () { 
        var array3 = [];
        function checkhours() {
            $('input.timepicker').each(function () {
                if ((this).value.length != 0) {
                    array3.push((this).value);
                }
            })

        }
        var array = [];
        var array2 = [];
        $("input:checkbox").each(function () {
            if ($(this).is(':checked')) {
                array.push(parseInt((this).value));
            }
        });
        console.log(array);

        var start = $("#txtStart").datepicker("getDate"),
            end = $("#txtEnd").datepicker("getDate"),
            currentDate = new Date(start),
            arr = [];


 //recursivo 1
     function dateof(currentDate, day) {
         var date = new Date(currentDate);
         if (currentDate > end) {
               return arr;             
            }

         if (date.getDay() == day) {
           

             arr.push(moment(new Date(date)).format("DD-MM-YYYY HH:mm a"));  

             $('input.timepicker').each(function () {
                 if ((this).value.length != 0 && this.name == day && !this.hidden) {
                     array2.push((this).value);
                 }
             })

         }
            return dateof(date.setDate(date.getDate() + 1),day);
         }

 //recursivo 2      
        function days(array) {
             
            if (array.length > 0) {
                dateof(currentDate, array[0]);
                array.shift();
                days(array);
            }
            return arr;
        }
        days(array);
        console.log(arr);
        console.log(array2);
        checkhours();
        if ($('#botón2').is(':disabled')) {//no ha seleccionado dias
            bootbox2("No ha seleccionado los días");
            return;
        }
        if (array2.length < 2 ) {//no ha ingresado horas
            bootbox2("Los horarios se encuentran vacíos");
            return;
        }
        if (arr.length == 0) { //no ha ingresado fechas
            bootbox2("El rango de fecha está vacío");
            return;
        }

        else {
            cantidad = $('#cantidad').val();
            $.ajax({
                type: "POST",
                dataType: "JSON",
                url: '/Reservacion/saveReser',
                data: { 'dias': arr, 'horas': array2, 'cantidad': cantidad},
                success: function (data) {

                    $('#myModalSave').modal('hide');
                    bootbox1(" Guardando reservación...");
                    FetchEventAndRenderCalendar();


                },
                error: function () {
                    bootbox2("Hubo un ERROR");
                }
            })
        }

    });
    $('#volver').click(function () {
        tablalista();
    });
    $('#aprobar').click(function () {
        var id = $('#lbl').text();

        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/Reservacion/Aprobar",
            data: {ced: id},
            success: function (data) {
                FetchEventAndRenderCalendar();
                $('#modaldt').modal('hide');                
            },
            error: function (error) {
                alert("Fallo");
            }
        })

    })
    $('#rechazar').click(function () {
    });

    function bootbox1(message) {
        var dialog = bootbox.dialog({//para cargas
            title: 'RESERVACIÓN',
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-spin fa-spinner"></i>' + message + '</p>'
        });
        dialog.init(function () {
            setTimeout(function () {
                dialog.modal('hide');
            }, 3000);

        });

    }

    function bootbox2(message) {//para errores
        bootbox.alert({
            title: 'RESERVACIÓN',
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-exclamation-triangle"></i>' + message + '</p>'
        });

    }

    function lista() {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/Reservacion/getSolicitud",
            success: function (data) {
                console.log(data);
            },
            error: function (error) {
                // alert("Fallo");
            }
        });
    }

    function tablaPadre() {
        var col = [
            { data: "Cedula" },
            { data: "Nombre" },
            { data: "Apellido1" },
            { data: "Apellido2" }
        ];

        var head = '<tr><td>Cédula</td><td>Nombre</td><td>1° Apellido</td><td>2° Apellido</td><td>Acción</td></tr>';
        col[col.length] = {
            data: "Id",
            "render": function (Id) {
                return "<button class='btn btn-warning btn-block' onclick='_alerta(" + Id + ")' style='padding: 2px 6px; margin:2px;'>" +
                    "<text class=''> Ver en detalle </text>" +
                    "<span class='glyphicon glyphicon-user'></span>" +
                    "</button>";

            }
        };
        $('#example thead').append(head);
        $.ajax({
            url: "/Reservacion/getSolicitud",
            type: 'POST',
            success: function (data) {
                var dat = data;
                $('#example').DataTable({
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
                    "data": dat,
                    columns: col
                });
            }
        })
    }

    function tablalista() {
        $('#volver').hide();
        $('#aprobar').hide();
        $('#rechazar').hide();
        if (!$.fn.DataTable.isDataTable('#example')) {
            tablaPadre();
           
        }
        else {
            var table = $('#example').DataTable();
            table.destroy();
            $('#example').remove(); 
            var thead = ("<thead></thead>");
            var tabla = $('<table/>', {
                id: 'example',
                class: 'table table-striped table-bordered dt-responsive'
            }).append(thead);
            $('#tabla').append(tabla);
            tablaPadre();
        }

    }

    
    })