$(document).ready(function () {

    $('#Tipo').val('0');
    $('#Filtro').val('0');

    $('#Tipo').on('change', function () {
        var tipo = $(this).val();
        var filter = $('#Filtro');
        var opciones = "<option value='0'>General</option>";
        filter.empty();

        $('#datos').DataTable().destroy();
        $('#datos').remove();

        if (tipo === '1') {
            $.ajax({
                url: "/SOGIP/UsersAdmin/ObtenerRoles",
                type: "GET",
                success: function (list) {
                    $.each(list, function (i) {
                        opciones = opciones + "<option value='" + list[i].Id + "'>" + list[i].Rol + "</option>";
                    });
                    filter.append(opciones);
                },
                error: function () {
                    alert("Error desconocido.");
                }
            });
        }
        else if (tipo === '2') {
            $.ajax({
                url: "/SOGIP/ExpedientesFisicos/ObtenerTipos",
                type: "GET",
                success: function (list) {
                    $.each(list, function (i) {
                        opciones = opciones + "<option value='" + list[i].TipoId + "'>" + list[i].Nombre + "</option>";
                    });
                    filter.append(opciones);
                },
                error: function () {
                    alert("Error desconocido.");
                }
            });
        }
        else if (tipo === '3' || tipo === '4') {
            $.ajax({
                url: "/SOGIP/TipoME/ObtenerTipos",
                type: "GET",
                data: { tipo: tipo },
                success: function (list) {
                    $.each(list, function (i) {
                        opciones = opciones + "<option value='" + list[i].Id + "'>" + list[i].nombre + "</option>";
                    });
                    filter.append(opciones);
                },
                error: function () {
                    alert("Error desconocido.");
                }
            });
        }
        else {
            filter.append(opciones);
        }
    });

    $('#Filtro').on('change', function () {
        $('#datos').DataTable().destroy();
        $('#datos').remove();
    });

    $('#Buscar').on('click', function () {
        var tipo = $('#Tipo').val();
        var filtro = $('#Filtro').val();
        var header = '<tr>';
        var col = [];

        switch (tipo) {
            case '0': {
                alert('No puedo ejecutar consulta sin un dato seleccionado.');
                return;
            }
            case '1': {
                url = "/SOGIP/UsersAdmin/GetUsuarios";
                header = header + '<th>Cédula</th><th>Nombre</th><th>Género</th><th>E-mail</th><th>Nacimiento</th><th>Estado</th><th>Rol</th><th>Entidad</th><th>Acción<span class="glyphicon glyphicon-cog"></span></th></tr>';
                col[col.length] = { data: "Cedula" };
                col[col.length] = { data: "Nombre" };
                col[col.length] = {
                    data: "Sexo",
                    "render": function (Sexo) {
                        var gender = Sexo ? "Masculino" : "Femenino";
                        return gender;
                    }
                };
                col[col.length] = { data: "Email" };
                col[col.length] = {
                    data: "Nacimiento",
                    "render": function (Nacimiento) {
                        var date = new Date(parseInt(Nacimiento.substr(6)));
                        return date.toLocaleDateString('en-GB');
                    }
                };
                col[col.length] = {
                    data: "Estado",
                    "render": function (Estado) {
                        var state = Estado ? "Activo" : "Inactivo";
                        return state;
                    }
                };
                col[col.length] = { data: "Rol" };
                col[col.length] = { data: "Entidad" };
                col[col.length] = {
                    data: "Id",
                    "render": function (Id) {
                        return "<a class='btn btn-large btn-warning' href='/SOGIP/Account/Perfil?id=" + Id + "'>Detalle <span class='glyphicon glyphicon-user'></span></a>";
                    }
                };
                break;
            }
            case '2': {
                url = "/SOGIP/ExpedientesFisicos/ObtenerArchivos";
                header = header + '<th>Nombre</th><th>Tipo</th><th>Usuario</th><th>Acción <span class="glyphicon glyphicon-cog"></th></tr>';
                col[col.length] = { data: "Nombre" };
                col[col.length] = { data: "Tipo" };
                col[col.length] = { data: "Usuario" };
                col[col.length] = {
                    data: "Id",
                    "render": function (Id) {
                        return "<a class='btn btn-info' href='/SOGIP/UsersAdmin/Download?archivoId=" + Id + "' style='padding: 2px 6px; margin: 2px;'>" +
                            "<text class='hidden-xs'>Descargar </text>" +
                            "<span class='glyphicon glyphicon-download'></span>" +
                            "</a>";
                    }
                };
                break;
            }
            case '3': case '4': {
                url = tipo === '3' ? "/SOGIP/TipoME/ObtenerEjercicios" : url = "/SOGIP/TipoME/ObtenerMaquinas";
                header = header + '<th>Nombre</th><th>Tipo</th></tr>';
                col[col.length] = { data: "Nombre" };
                col[col.length] = { data: "Tipo" };
                break;
            }
            case '5': {
                url = "/SOGIP/Busqueda/getActividades";
                header = header + '<th>Título</th><th>Lugar</th><th>Descripción</th><th>Inicia</th><th>Termina</th></tr>';
                col[col.length] = { data: "Titulo" };
                col[col.length] = { data: "Lugar" };
                col[col.length] = { data: "Descripcion" };
                col[col.length] = {
                    data: "FechaHoraInicio",
                    "render": function (FechaHoraInicio) {
                        var date = new Date(parseInt(FechaHoraInicio.substr(6)));
                        return date.toLocaleDateString('en-GB');
                    }
                };
                col[col.length] = {
                    data: "FechaHoraFinal",
                    "render": function (FechaHoraFinal) {
                        var date = new Date(parseInt(FechaHoraFinal.substr(6)));
                        return date.toLocaleDateString('en-GB');
                    }
                };
                //col[col.length] = {
                //    data: "ArchivoId",
                //    "render": function (ArchivoId) {
                //        return "<a class='btn btn-info' href='/UsersAdmin/Download?archivoId=" + ArchivoId + "' style='padding: 2px 6px; margin: 2px;'>" +
                //            "<text class='hidden-xs'>Descargar </text>" +
                //            "<span class='glyphicon glyphicon-download'></span>" +
                //            "</a>";
                //    }
                //};
                break;
            }
        }

        $('#datos').DataTable().destroy();
        $('#datos').remove();

        $('<table />', {
            id: 'datos',
            class: 'table table-striped table-bordered'
        }).append("<thead>" + header + "</thead>").append("<tfoot>" + header + "</tfoot>").appendTo('#Resultados');
        
        $('#datos').DataTable({
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
                "url": url,
                "type": "GET",
                "data": { filtro: filtro },
                "dataSrc": ""
            },
            columns: col
            // select: true
        });

        $('#datos tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" class="form-control" placeholder="' + title + '" />');
        });

        var table = $('#datos').DataTable();
        table.columns().every(function () {
            var that = this;

            $('input', this.footer()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that.search(this.value).draw();
                }
            });
        });

    });

});