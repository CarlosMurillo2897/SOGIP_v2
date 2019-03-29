$(document).ready(function () {
        $('#txtStart').datepicker();
    $('input.timepicker').timepicker({
        'step': 60,
        'minTime': '6:00am',
        'maxTime': '7:00pm',
        'disableTextInput': true

    });
    $('[data-toggle="popover"]').popover();
    $('#botón').click(function () { $('#myModalSave').modal('show'); });
    FetchEventAndRenderCalendar();




    //FUNCIÓN PARA GUARDAR ACTIVIDAD
    $('#btnSave').click(function () {
        var data = null;
        var data2 = null;

        //----------------validaciones-------------------//
        if ($('#titulo').val().length < 1) {//no ha ingresado horas
            bootbox2("El campo título está vacío");
            return;
        }
        if ($('#lugar').val().length < 1) {//no ha ingresado horas
            bootbox2("El campo lugar está vacío");
            return;
        }
        if ($('#txtStart').val().length < 1) {//no ha ingresado horas
            bootbox2("El campo fecha está vacío");
            return;
        }
        if ($('#txtHoraI').val().length < 1) {//no ha ingresado horas
            bootbox2("El campo Hora Inicio está vacío");
            return;
        }
        if ($('#txtHoraF').val().length < 1) {//no ha ingresado horas
            bootbox2("El campo Hora Final está vacío");
            return;
        }
        //----------------------------------------------//
        data = {
            Titulo: $('#titulo').val(),
            Descripcion: $('#descripcion').val(),
            Lugar: $('#lugar').val()
        }

        data2 = {
            FechaHoraInicio: $('#txtStart').val() + ' ' + $('#txtHoraI').val(),
            FechaHoraFinal: $('#txtStart').val() + ' ' + $('#txtHoraF').val()
        }

        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: '/Actividad/saveActividad',
            data: { 'actividad': data, 'horario':data2},
            success: function (data) {
                $('#myModalSave').modal('hide');
                bootbox1(" Guardando actividad...");
                FetchEventAndRenderCalendar();
                $('#titulo').val('');
                $('#lugar').val('');
                $('#descripcion').val('');
                $('#txtStart').val('');
                $('#txtHoraI').val('');
                $('#txtHoraF').val('');

                $('.image-preview').attr("data-content", "").popover('hide');
                $('.image-preview-filename').val("");
                $('.image-preview-clear').hide();
                $('.image-preview-input input:file').val("");
                $(".image-preview-input-title").text("Buscar");
            },
            error: function () {
                bootbox2("Hubo un ERROR");
            }
        })
    });
        
        //CALENDARIO
        function FetchEventAndRenderCalendar() {
            events = [];
            $.ajax({
                type: "POST",
                dataType: "JSON",
                url: "/Actividad/GetAct",
                success: function (data) {
                    $.each(data, function (i, v) {
                        events.push({
                            title: v.titulo,
                            lugar: v.lugar,
                            descripcion: v.descripcion,
                            start: $.fullCalendar.moment(v.Inicio),
                            end: $.fullCalendar.moment(v.Final)
                        });
                    })
                    GenerateCalendar(events);
                },
                error: function (error) {
                    alert("d");
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
                },
                weekends: false,
                eventLimit: true,
                eventColor: '#339966',
                events: events,
                themeSystem: 'jquery-ui',
                defaultView: 'list',
                selectable: true, //EVENTO SELECCIONABLE
                select: function (start, end) { //CLICK SOBRE EL DÍA EN EL CALENDARIO
                    var check = start.format("YYYY-MM-DD");
                    var today = moment().format("YYYY-MM-DD");
                    if (check < today) {
                        bootbox2("No se puede realizar la cita en fechas anteriores");
                    }
                   
                },
                editable: false, //para editar
                disableDragging: true,
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
                    $description.append($('<p/>').html('<b>Lugar: </b>' + calEvent.lugar));
                    $description.append($('<hr/>'));
                    if (calEvent.descripcion != null) {
                        $description.append($('<p/>').html('<b>Descripción: </b>' +calEvent.descripcion));
                    }
                    else {
                        $description.append($('<p/>').html('<b>Descripción: </b>'+'Sin descripción'));
                    }
                   
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

   
    //


    //BOOTBOXES
    function bootbox1(message) {
        var dialog = bootbox.dialog({//para cargas
            title: 'ACTIVIDAD',
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
            title: 'ACTIVIDAD',
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-exclamation-triangle"></i>' + message + '</p>'
        });

    }
    //

});

$(document).on('click', '#close-preview', function () {
    $('.image-preview').popover('hide');
    // Hover befor close the preview
    $('.image-preview').hover(
        function () {
            $('.image-preview').popover('show');
        },
        function () {
            $('.image-preview').popover('hide');
        }
    );
});

$(function () {
    // Create the close button
    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;',
    });
    closebtn.attr("class", "close pull-right");
    // Set the popover default content
    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>Preview</strong>" + $(closebtn)[0].outerHTML,
        content: "No hay ninguna imagen",
        placement: 'bottom'
    });
    // Clear event
    $('.image-preview-clear').click(function () {
        $('.image-preview').attr("data-content", "").popover('hide');
        $('.image-preview-filename').val("");
        $('.image-preview-clear').hide();
        $('.image-preview-input input:file').val("");
        $(".image-preview-input-title").text("Buscar");
    });
    // Create the preview image
    $(".image-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        // Set preview image into the popover data-content
        reader.onload = function (e) {
            $(".image-preview-input-title").text("Cambiar");
            $(".image-preview-clear").show();
            $(".image-preview-filename").val(file.name);
            img.attr('src', e.target.result);
            $(".image-preview").attr("data-content", $(img)[0].outerHTML).popover("show");
        }
        reader.readAsDataURL(file);
    });
});