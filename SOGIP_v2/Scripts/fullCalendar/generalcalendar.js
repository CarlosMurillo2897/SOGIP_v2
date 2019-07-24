$(document).ready(function () {
    
    var ced;
    $('#inbodyCheck').val(this.checked);
    $('#rutinaCheck').val(this.checked);
    var events = [];
    var selectedEvent = null;
    $('[data-toggle="popover"]').popover();
    FetchEventAndRenderCalendar();
    cedus();
    checks();
    checkTime();

    function cedus() {

        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/CitasAdmin/GetCed",
            success: function (data) {
                ced = data;
            },
            error: function (error) {
                alert("Fallo");
            }
        })
    }

    //FUNCIÓN PARA LLENAR Y ACTUALIZAR CALENDARIO
    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/CitasAdmin/GetEvents",
            success: function (data) {
                $.each(data, function (i, v) {
                    events.push({
                        citaId: v.CitaId,
                        title: 'CITA',
                        description1: v.InBody,
                        description2: v.Otro,
                        cedula: v.UsuarioId_Id.Cedula,
                        nombre: v.UsuarioId_Id.Nombre1,
                        apellido1: v.UsuarioId_Id.Apellido1,
                        apellido2: v.UsuarioId_Id.Apellido2,
                        start: $.fullCalendar.moment(v.FechaHoraInicio),
                        end: $.fullCalendar.moment(v.FechaHoraFinal)
                    });
                })
                GenerateCalendar(events);
            },
            error: function (error) {
                console.log("ERROR: generate calendar");
            }
        })
    }

    //FUNCIÓN PARA GENERAR EL CALENDARIO
    function GenerateCalendar(events) {
        $('#calendar').fullCalendar('destroy');
        $('#calendar').fullCalendar({
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
            eventColor: '#ff4d4d',
            events: events,
            themeSystem: 'jquery-ui',
            selectable: true,
            select: function (start, end) {
                var check = start.format("YYYY-MM-DD");
                var today = moment().format("YYYY-MM-DD");
                if (check < today) {
                    bootbox2("No se puede realizar la cita en fechas anteriores");
                }
                else {
                    selectedEvent = {
                        citaId: 0,
                        description1: '',
                        description2: '',
                        cedula: '',
                        nombre: '',
                        apellido1: '',
                        apellido2: '',
                        start: start,
                        end: end

                    };
                    openEditForm();
                    $('calendar').fullCalendar('unselect');
                }
            },
            eventAfterRender: function (event, element, view) { //COLOR DE LOS EVENTOS
                
                if (event.cedula == ced) {
                    element.css('background-color', '#668cff');
                    element.css('border-color', '#668cff');
                }
               
            },
            editable: false,
             dayClick:
                function (date, allDay, jsEvent, view) {//EVENTOS DEL DÍA

                    allOpT(date);

                },
            eventClick: function (calEvent, jsEvent, view) { //INFORMACIÓN DEL EVENTO
                allOpT(calEvent.start);
                if (calEvent.cedula == ced) {
                    selectedEvent = calEvent;
                    var check = calEvent.start.format("YYYY-MM-DD");
                    var today = moment().format("YYYY-MM-DD");

                    $('#myModal #eventTitle').text(calEvent.title);
                    var $description = $('<div/>');
                    $description.append($('<p/>').html('<b>Inicia: </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                    if (calEvent.end != null) {
                        $description.append($('<p/>').html('<b>Termina: </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                    }
                    $description.append($('<hr/>'));
                    $description.append($('<p/>').html('<b>Motivo de la Cita</b>'));
                    if (calEvent.description1 != false) {
                        $description.append($('<p/>').html('<b>InBody: </b>' + 'Si'));
                    }
                    else {
                        $description.append($('<p/>').html('<b>InBody: </b>' + 'No'));
                    }

                    if (calEvent.description2 != false) {
                        $description.append($('<p/>').html('<b>Rutina: </b>' + 'Si'));
                    }
                    else {
                        $description.append($('<p/>').html('<b>Rutina: </b>' + 'No'));
                    }
                    $description.append($('<hr/>'));
                    $description.append($('<p/>').html('<b>Información del Usuario</b>'));
                    $description.append($('<p/>').html('<b>Cedula: </b>' + calEvent.cedula));
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

    function allOpT(date) {
        timeA(date);
        timeP(date);
        timeCal(hours, hours2);
    }

    function timeA(date) {
        //$('input.timepicker').timepicker('remove');
        hours2 = [];
        var nw = new Date(); //para  fechas iguales y horas no inferiores
        var currT = nw;
        nw = moment(nw).format('YYYY-MM-DD');
        var date = date.format('YYYY-MM-DD');

        if (date == nw) { //para fechas iguales
            var h = currT.getHours();
            var mi = currT.getMinutes();

            var ap = h >= 12 ? 'pm' : 'am';

            h = h % 12;
            h = h ? h : 12;

            mi = mi < 10 ? '0' + mi : mi;
            var currentTime = h + ':' + mi + ap;
            hours2.push(['6:00am', currentTime]);


        }
    }

    function timeP(date) { //CONTROLAR TIMEPICKER
        hours = [];
        var date = date.format('YYYY-MM-DD');

        $('#calendar').fullCalendar('clientEvents', function (event) { //TODAS LAS HORAS DE LAS CITAS DE UN DÍA "loop"

            var start = moment(event.start).format("YYYY-MM-DD");
            var co = new Date(event.start); //hora inicio
            var fi = new Date(event.end); //hora finalización
            var hour1 = co.getHours();
            var min1 = co.getMinutes();
            var hour2 = fi.getHours();
            var min2 = fi.getMinutes();

            var ampm = hour1 >= 12 ? 'pm' : 'am';
            var ampm2 = hour2 >= 12 ? 'pm' : 'am';

            hour1 = hour1 % 12;
            hour1 = hour1 ? hour1 : 12;

            hour2 = hour2 % 12;
            hour2 = hour2 ? hour2 : 12;

            min1 = min1 < 10 ? '0' + min1 : min1;
            min2 = min2 < 10 ? '0' + min2 : min2;


            var firstT = hour1 + ':' + min1 + ampm;
            var secondT = hour2 + ':' + min2 + ampm2;
            if (date == start) {
                hours.push([firstT, secondT]);

            }

        });

    }

    function timeCal(hours, hours2) {
        $('input.timepicker').timepicker('remove');


        if (hours.length != 0 && hours2.length != 0) {
            var hours3 = hours.concat(hours2);
            $('input.timepicker').timepicker({
                'step': 60,
                'disableTimeRanges': hours3,
                'minTime': '6:00am',
                'maxTime': '7:00pm',
                'disableTextInput': true

            });
        }

        else if (hours.length != 0 && hours2) {

            $('input.timepicker').timepicker({

                'minTime': '6:00am',
                'step': 60,
                'disableTimeRanges': hours,
                'maxTime': '7:00pm',
                'disableTextInput': true

            });
        }

        else if (hours2.length != 0 && hours) {
            $('input.timepicker').timepicker({
                'step': 60,
                'disableTimeRanges': hours2,
                'minTime': '6:00am',
                'maxTime': '7:00pm',
                'disableTextInput': true
            });
        }

        else {
            $('input.timepicker').timepicker({
                'step': 60,
                'minTime': '6:00am',
                'maxTime': '7:00pm',
                'disableTextInput': true

            });
        }


    }


    //EDITAR CITA
    $('#btnEdit').click(function () {
        openEditForm();
    })

    //ELIMINAR CITA
    $('#btnDelete').click(function () {

        bootbox.confirm({
            title: 'CITA',
            size: 'small',
            message: '<p><i class="fa fa-exclamation-triangle"></i> ¿Está seguro de que deseea eliminar la cita?</p>',
            buttons: {
                confirm: {
                    label: 'Si',
                    className: 'btn-primary'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result && selectedEvent != null) {
                    $.ajax({
                        type: "POST",
                        dataType: "JSON",
                        url: '/SOGIP/CitasAdmin/DeleteEvent',
                        data: { 'citaId': selectedEvent.citaId },
                        success: function (data) {
                            if (data.status) {
                                bootbox1(" Eliminando Cita");
                                FetchEventAndRenderCalendar();
                                $('#myModal').modal('hide');
                            }
                        },
                        error: function () {
                            bootbox2(" Hubo un ERROR al intentar eliminar");
                        }
                    })
                }
            }
        });

    })

    //FUNCIÓN PARA ABRIR MODAL DE EDITAR CITA
    function openEditForm() {
        if (selectedEvent != null) {
            $('#hdEventID').val(selectedEvent.citaId);
            $('#txtStart').val(selectedEvent.start.format("DD-MM-YYYY"));
            $('#txtHora').val(selectedEvent.start.format("HH:mm a"));
            $('#txtHoraF').val(selectedEvent.end.format("HH:mm a"));
            $('#inbodyCheck').prop("checked", selectedEvent.description1 || false);
            $('#rutinaCheck').prop("checked", selectedEvent.description2 || false);
            $('#inbodyCheck').change();
            $('#rutinaCheck').change();
        }
        $('#myModal').modal('hide');
        $('#myModalSave').modal('show');
    }

    //GUARDA CITA
    $('#btnSave').click(function () {
        var data = null;
        //Validaciones
        if ($('#txtStart').val().trim() == "") {
            bootbox2(' Fecha de inicio no puede estar vacio');
            return;
        }
        if ($('#txtHora').val().trim() == "") {
            bootbox2(' Hora de Inicio no puede estar vacio');
            return;
        }

        if ($('#inbodyCheck').is(':checked') == false && $('#rutinaCheck').is(':checked') == false) {
            bootbox2(' No ha seleccionado InBody y/o Rutina');
            return;
        }

        else {
            var starDate = moment($('#txtStart').val(), "DD-MMM-YYYY HH:mm a").toDate();
            var endDate = moment($('#txtEnd').val(), "DD-MMM-YYYY HH:mm a").toDate();
            if (starDate > endDate) {
                bootbox2(' La fecha y hora de finalización es inválido');
                return;
            }

        }

        data = {
            CitaId: $('#hdEventID').val(),
            InBody: $('#inbodyCheck').is(':checked'),
            Otro: $('#rutinaCheck').is(':checked'),
            FechaHoraInicio: $('#txtStart').val()+ ' ' + $('#txtHora').val(),
            FechaHoraFinal: $('#txtStart').val() + ' ' + $('#txtHoraF').val()
        }

        //Llamando función para enviar cambios
        SaveDate(data);
    })


    function checkTime() {

        $('#txtHora').on('change', function () {

            $('#inbodyCheck').prop('disabled', false);
            $('#rutinaCheck').prop('disabled', false);

            var startTime = $('#txtHora').timepicker('getTime');
            //var nu = new Date(startTime.getTime() + 60 * 60000);

            var ampm = (startTime.getHours() > 12) ? 'pm' : 'am';
            var num = (startTime.getHours() + 1) % 12;
            var conv = num.toString() + ":00" + ampm;
            console.log(conv);

            for (var i = 0; i < hours.length; i++) {
                if (hours[i][0] == conv) {
                    $('#rutinaCheck').prop('disabled', true);
                    break;
                }
                else {
                    console.log("2");
                }
            }

            if ($('#inbodyCheck').is(':checked') == true && $('#rutinaCheck').is(':checked') == true) {
                var endTime = new Date(startTime.getTime() + 110 * 60000);   // ambas
                $('#txtHoraF').timepicker('setTime', endTime);
            }

            else if ($('#inbodyCheck').is(':checked') == false && $('#rutinaCheck').is(':checked') == true) {
                var endTime = new Date(startTime.getTime() + 90 * 60000);   // rutina
                $('#txtHoraF').timepicker('setTime', endTime);
            }

            else if ($('#inbodyCheck').is(':checked') == true && $('#rutinaCheck').is(':checked') == false) {
                var endTime = new Date(startTime.getTime() + 20 * 60000);   // inbody
                $('#txtHoraF').timepicker('setTime', endTime);
            }
        });



    }

    function checks() {

        $('#inbodyCheck, #rutinaCheck ').change(function () {

            var startTime = $('#txtHora').timepicker('getTime');

            if ($('#inbodyCheck').is(':checked') == true && $('#rutinaCheck').is(':checked') == true) {
                var endTime = new Date(startTime.getTime() + 110 * 60000);   // ambas
                $('#txtHoraF').timepicker('setTime', endTime);
            }

            else if ($('#inbodyCheck').is(':checked') == false && $('#rutinaCheck').is(':checked') == true) {
                var endTime = new Date(startTime.getTime() + 90 * 60000);   // rutina
                $('#txtHoraF').timepicker('setTime', endTime);
            }

            else if ($('#inbodyCheck').is(':checked') == true && $('#rutinaCheck').is(':checked') == false) {
                var endTime = new Date(startTime.getTime() + 20 * 60000);   // inbody
                $('#txtHoraF').timepicker('setTime', endTime);
            }
            else {
                $('#txtHoraF').timepicker('setTime', null);
                $('#txtHora').timepicker('setTime', null);
                $('#inbodyCheck').prop('disabled', true);
                $('#rutinaCheck').prop('disabled', true);
            }

            $('#inbodyCheck').val($(this).is(':checked'));
            $('#rutinaCheck').val($(this).is(':checked'));

        });

    }

    function SaveDate(data) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: '/SOGIP/CitasAdmin/SaveEvent',
            data: { 'e': data, 'cedu': ced },
            success: function (data) {
                if (data.status) {
                    //Actualiza el calendario
                    bootbox1(" Guardando cita...");
                    FetchEventAndRenderCalendar();
                    $('#myModalSave').modal('hide');
                }
                else {
                    alert("La hora no está disponible");
                }
            },
            error: function () {
                bootbox2(" Hubo un ERROR al intentar guardar");
            }
        })
    }

    //----------------------------------------BOOTBOX
    function bootbox1(message) {
        var dialog = bootbox.dialog({//para cargas
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
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-exclamation-triangle"></i>' + message + '</p>'
        });

    }



});