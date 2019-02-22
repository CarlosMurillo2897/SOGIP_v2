$(document).ready(function () {
    var cedu = "";
    $('#inbodyCheck').val(this.checked);
    $('#rutinaCheck').val(this.checked);
    var hours2 = [];
    var hours = []; //cualquier fecha
    var events = [];
    var selectedEvent = null;
    FetchEventAndRenderCalendar();
    llenarTabla();
    checks();


    //-----------------------------------------FUNCIÓN PARA LLENAR Y ACTUALIZAR CALENDARIO
    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/CitasAdmin/GetEvents",
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
                alert("Fallo");
            }
        })
    }

    //-----------------------------------------FUNCIÓN PARA GENERAR EL CALENDARIO
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
                    alert("No se puede realizar la cita en fechas posteriores");
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
                    $('#cedUD').show();
                    openEditForm();
                    $('calendar').fullCalendar('unselect');
                }
            },
            editable: true, //para editar
            eventDrop: function (event) { //MOVER EVENTOS
                var data = {
                    CitaId: event.citaId,
                    InBody: event.description1,
                    Otro: event.description2,
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
            dayClick:
                function (date, allDay, jsEvent, view) {//EVENTOS DEL DÍA
                    allOpT(date);
                    $('#chgUs').hide();
                    $('#usuario').val('Seleccione un usuario de la tabla');
                    $('#infouser').show();
                    $('#Tabla_Usuarios').hide();
                },
            eventClick: function (calEvent, jsEvent, view) { //INFORMACIÓN DEL EVENTO
                allOpT(calEvent.start);
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
                $description.append($('<p/>').html('<h4><b>Motivo de la Cita</b></h4>'));
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
                $description.append($('<p/>').html('<h4><b>Información del Usuario</b></h4>'));
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
//---------------------------------------------NO TOCAR
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

    //-----------------------------------------EDITAR CITA
    $('#btnEdit').click(function () {
        //Abrir modal dialog para editar el evento seleccionado
        $('#chgUs').show();
        $('#infouser').hide();
        $('#Tabla_Usuarios').hide();
        openEditForm();
    })
    $('#chgUs').click(function () {

        bootbox.confirm({
            title: 'CITA',
            size: 'small',
            message: '<p><i class="fa fa-exclamation-triangle"></i> ¿Está seguro de que desea asignar la cita a otro usuario?</p>',
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
                if (result) {
                    $('#infouser').show();
                    $('#chgUs').hide();

                }
            }
        });
    });

    //-----------------------------------------ELIMINAR CITA
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
                        url: '/CitasAdmin/DeleteEvent',
                        data: { 'citaId': selectedEvent.citaId },
                        success: function (data) {
                            if (data.status) {
                                bootbox1(" Eliminando Cita");
                                FetchEventAndRenderCalendar();
                                $('#myModal').modal('hide');
                            }
                        },
                        error: function () {
                            alert("Fallo");
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
            $('#txtStart').val(selectedEvent.start.format("DD-MMM-YYYY"));
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

    //-----------------------------------------GUARDA CITA
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

        if (cedu == "") {
            bootbox2(' No ha seleccionado usuario');
            return;
        }

        else {
            var starDate = moment($('#txtStart').val(), "DD-MMM-YYYY HH:mm a").toDate();
            var endDate = moment($('#txtEnd').val(), "DD-MMM-YYYY HH:mm a").toDate();
            if (starDate > endDate) {
                bootbox2(' La fecha y hora de finalización es inválido');
                return;
            }

            else if (!starDate.getMonth()) {
                alert("No existe");
            }

        }
        //Esta variable almacena la cita sobre la cual se está trabajando

            data = {
                CitaId: $('#hdEventID').val(),
                InBody: $('#inbodyCheck').is(':checked'),
                Otro: $('#rutinaCheck').is(':checked'),
                FechaHoraInicio: $('#txtStart').val().trim() + ' ' + $('#txtHora').val().trim(),
                FechaHoraFinal: $('#txtStart').val().trim() + ' ' + $('#txtHoraF').val().trim()
            }
        
        //Llamando función para enviar cambios
        SaveDate(data);
    })
    //----------------------------------------CHECKBOXES Y TIMEPICKER

    
    function checks() {
        $('#inbodyCheck, #rutinaCheck ').change(function () {
            var startTime = $('#txtHora').timepicker('getTime');

            if ($('#inbodyCheck').is(':checked') == true && $('#rutinaCheck').is(':checked') == true) {
                var endTime = new Date(startTime.getTime() + 110 * 60000);   // add 30 minutes
                $('#txtHoraF').timepicker('setTime', endTime);
            }

            else if ($('#inbodyCheck').is(':checked') == false && $('#rutinaCheck').is(':checked') == true) {
                var endTime = new Date(startTime.getTime() + 90 * 60000);   // add 30 minutes
                $('#txtHoraF').timepicker('setTime', endTime);
            }

            else if ($('#inbodyCheck').is(':checked') == true && $('#rutinaCheck').is(':checked') == false) {
                var endTime = new Date(startTime.getTime() + 20 * 60000);   // add 30 minutes
                $('#txtHoraF').timepicker('setTime', endTime);
            }
            else {
                $('#txtHoraF').timepicker('setTime', null);
                $('#txtHora').timepicker('setTime', null);
            }
            $('#inbodyCheck').val($(this).is(':checked'));
            $('#rutinaCheck').val($(this).is(':checked'));
          
        });

    }

    function SaveDate(data) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: '/CitasAdmin/SaveEvent',
            data: { 'e': data, 'cedu':cedu},
            success: function (data) {
                if (data.status) {
                    //Actualiza el calendario
                    bootbox1(" Guardando cita...");
                    FetchEventAndRenderCalendar();
                    $('#myModalSave').modal('hide');
                    cedu = "";
                }
                else {
                    alert("La hora no está disponible");
                }
            },
            error: function () {
                bootbox2("Hubo un ERROR");
            }
        })
    }
    //--------------------------------------------------
    var today = new Date();
    $(function () {
        $("#dtp1").datepicker({
        });
    });


    //-----------------------------------------DATATABLE
    function llenarTabla() {
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
                "url": "/CitasAdmin/getUsuariosA",
                "type": "GET",
                "dataSrc": ""
            },
            columns: [
                { data: "Accion" },
                { data: "Cedula" },
                { data: "Nombre" },
                { data: "Apellido1"},
                { data: "Apellido2" },
                { data: "Rol" }
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

    //Usar botón para esconder o mostrar datatable
    $('#botón').click(function () {
        $('#Tabla_Usuarios').show();
    });

    //Esconder la tabla al seleccionar por check-box
    $('#example').on('click', 'td.select-checkbox', function () {
        var td = $(this);
        var tr = td.closest('tr');
      
        $('#usuario').val(tr.find('td:eq(1)').text() + ' ' + tr.find('td:eq(2)').text() + ' ' + tr.find('td:eq(3)').text() + ' ' + tr.find('td:eq(4)').text());
        cedu=tr.find('td:eq(1)').text();
        $('#Tabla_Usuarios').hide();
    });

    //----------------------------------------BOOTBOX
    function bootbox1(message) {
        var dialog = bootbox.dialog({//para cargas
            title: 'CITA',
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-spin fa-spinner"></i>'+message+'</p>'
        });
        dialog.init(function () {
            setTimeout(function () {
                dialog.modal('hide');
            }, 3000);
            
        });
        
    }

    function bootbox2(message) {//para errores
        bootbox.alert({
            title: 'CITA',
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-exclamation-triangle"></i>' + message + '</p>'
        });

    }


});