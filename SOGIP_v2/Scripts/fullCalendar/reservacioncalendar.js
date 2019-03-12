$(document).ready(function () {

    //variables
    var events = [];
    var   ar = [];
    
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
                        estado: v.Estado,
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
                alert("Fallo");
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
            select: function (start, end) { //CLICK SOBRE EL DÍA EN EL CALENDARIO
                var check = start.format("YYYY-MM-DD");
                var today = moment().format("YYYY-MM-DD");
                if (check < today) {
                    alert("No se puede realizar reservaciones en fechas posteriores");
                }
                else {
                    selectedEvent = {
                        reservacionId: 0,
                        estado: '',
                        cedula: '',
                        nombre: '',
                        apellido1: '',
                        apellido2: '',
                        start: start,
                        end: end

                    };
                    //$('#cedUD').show();
                    //openEditForm();
                    $('calendar').fullCalendar('unselect');
                }
            },
            editable: true, //para editar
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

                var date = event.start.format("YYYY-MM-DD");
                var today = moment().format("YYYY-MM-DD");

                if (date > today) {
                    //event.color = "#FFB347"; //Em andamento
                    element.css('background-color', '#668cff');
                    element.css('border-color', '#668cff');
                } else if (date < today) {
                    //event.color = "#77DD77"; //Concluído OK
                    element.css('background-color', '#ff4d4d');
                    element.css('border-color', '#ff4d4d');
                }
            },
            //dayClick:
            //    function (date, allDay, jsEvent, view) {//EVENTOS DEL DÍA
            //        allOpT(date);
            //        $('#chgUs').hide();
            //        $('#usuario').val('Seleccione un usuario de la tabla');
            //        $('#infouser').show();
            //        $('#Tabla_Usuarios').hide();
            //    },
            //eventClick: function (calEvent, jsEvent, view) { //INFORMACIÓN DEL EVENTO
            //    allOpT(calEvent.start);
            //    selectedEvent = calEvent;
            //    var check = calEvent.start.format("YYYY-MM-DD");
            //    var today = moment().format("YYYY-MM-DD");

            //    $('#myModal #eventTitle').text(calEvent.title);

            //    var $description = $('<div/>');
            //    $description.append($('<p/>').html('<b>Inicia: </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
            //    if (calEvent.end != null) {
            //        $description.append($('<p/>').html('<b>Termina: </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
            //    }
            //    $description.append($('<hr/>'));
            //    $description.append($('<p/>').html('<h4><b>Motivo de la Cita</b></h4>'));
            //    if (calEvent.description1 != false) {
            //        $description.append($('<p/>').html('<b>InBody: </b>' + 'Si'));
            //    }
            //    else {
            //        $description.append($('<p/>').html('<b>InBody: </b>' + 'No'));
            //    }

            //    if (calEvent.description2 != false) {
            //        $description.append($('<p/>').html('<b>Rutina: </b>' + 'Si'));
            //    }
            //    else {
            //        $description.append($('<p/>').html('<b>Rutina: </b>' + 'No'));
            //    }
            //    $description.append($('<hr/>'));
            //    $description.append($('<p/>').html('<h4><b>Información del Usuario</b></h4>'));
            //    $description.append($('<p/>').html('<b>Cédula: </b>' + calEvent.cedula));
            //    cedu = calEvent.cedula;
            //    $description.append($('<p/>').html('<b>Nombre Completo: </b>' + calEvent.nombre + ' ' + calEvent.apellido1 + ' ' + calEvent.apellido2));

            //    if (check < today) {
            //        $('#btnEdit').prop('disabled', true);
            //        $('#btnDelete').prop('disabled', true);

            //    }
            //    else {
            //        $('#btnEdit').prop('disabled', false);
            //        $('#btnDelete').prop('disabled', false);
            //    }

            //    $('#myModal #pDetails').empty().html($description);

            //    $('#myModal').modal();
            //}


        })

    }


    //----BOTONES
    $('#botón2').click(function () {
        //$("div[name=d]").each(function () { PENSAR EN UNA SOLUCIÓN <- - - - -
        //    this.hidden = true;
        //});
        ablehour(ar);
        $('#myModalSave2').modal('show');
    });
    //array.shift() sirve para remover el primer elemento
    $('#botón').click(function () { $('#myModalSave').modal('show');});
    $('#btnSave').click(function () {
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
                 if ((this).value.length != 0 && this.name == day) {
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


        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: '/Reservacion/saveReser',
            data: { 'dias': arr, 'horas': array2 },
            success: function (data) {
                if (data.status) {
                    alert("hola");
                }
                
            },
            error: function () {
                bootbox2("Hubo un ERROR");
            }
        })


    });



    })