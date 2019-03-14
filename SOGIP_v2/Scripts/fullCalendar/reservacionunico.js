$(document).ready(function () {

    //variables
    var ced;
    var cantidad;
    var events = [];
    var ar = [];
    cedus();

    $('#cant').hide();

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

    //obtener la cédula del usuario actual
    function cedus() {

        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/Reservacion/GetCed",
            success: function (data) {
                ced = data;
            },
            error: function (error) {
                alert("Fallo");
            }
        })
    }

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


                if (event.cedula == ced) {
                    element.css('background-color', '#b30000');
                    element.css('border-color', '#b30000');
                }
            },
            eventClick: function (calEvent, jsEvent, view) { //INFORMACIÓN DEL EVENTO

                if (calEvent.cedula == ced) {
                    selectedEvent = calEvent;
                    var check = calEvent.start.format("YYYY-MM-DD");
                    var today = moment().format("YYYY-MM-DD");


                    $('#myModal #eventTitle').text(calEvent.title);

                    var $description = $('<div/>');
                    $description.append($('<p/>').html('<b>Fecha: </b>' + calEvent.start.format("DD-MM-YYYY")));
                    $description.append($('<p/>').html('<b>Hora: </b>' + calEvent.start.format("HH:mm a") + " - " + calEvent.end.format("HH:mm a")));

                 
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
            return dateof(date.setDate(date.getDate() + 1), day);
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
        if (array2.length < 2) {//no ha ingresado horas
            bootbox2("Los horarios se encuentran vacíos");
            return;
        }
        if (arr.length == 0) { //no ha ingresado fechas
            bootbox2("El rango de fecha está vacío");
            return;
        }

        else {
            cantidad = 1;
            $.ajax({
                type: "POST",
                dataType: "JSON",
                url: '/Reservacion/saveReser',
                data: { 'dias': arr, 'horas': array2, 'cantidad': cantidad },
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

})