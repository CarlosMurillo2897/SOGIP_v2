$(document).ready(function () {

    $('#SearchBtn').click(function () {
        search();
    });

    $('#Search').keyup(function () {
        
        search();
        
    });

    $('#SearchBy').change(function () {
        
        search();
        
    });

    function search() {

        var SearchBy = $('#SearchBy').val();
        var SearchValue = $('#Search').val();
        var SetData = $('#perfiles');
        SetData.html("");

        $.ajax({
            type: "post",
            url: "/Busqueda/Buscar?SearchBy=" + SearchBy + "&SearchValue=" + SearchValue,
            contentType: "html",
            success: function (result) {
                if (result.length == 0) {
                    // SetData.append('<tr style="color:red"><td colspan="3">No se encontró.</td></tr>');
                }
                else {
                    $.each(result, function (index, value) {

                        var Nombre2 = (value.Nombre2 === null) ? '' : value.Nombre2 + ' ';

                        let Data = '<div class="row user-row">' +
                                        '<div class="col-xs-3 col-sm-2 col-md-1 col-lg-1" >';
                        if (value.Estado == true) {
                            Data = Data + '<img class="img-circle" id = "Comprobado-' + value.Id + '" ' +
                                            'src = "../Content/Imagenes/comprobado.png" ' +
                                            'style = "width: 50px; height: 50px;" ' +
                                            'alt = "User Pic" >'
                        }else {
                            Data = Data + '<img class="img-circle" id="Cancelar-' + value.Id + '" ' +
                                            'src = "../Content/Imagenes/cancelar.png" ' +
                                            'style = "width: 50px; height: 50px;" ' +
                                            'alt = "User Pic" >';
                        }
                           Data = Data + '</div > ' +
                                         '<div class="col-xs-8 col-sm-9 col-md-10 col-lg-10" > ' +
                                            '<strong>' + value.Cedula + '</strong><br>' +
                                            '<strong>'+value.Nombre1+' '+Nombre2+value.Apellido1+' '+value.Apellido2+'</strong><br>' +
                                                '<span class="text-muted">Tipo de Usuario: REVISAR</span>' +
                                         '</div>' +
                                         '<div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 dropdown-user" data-for=".'+value.Id+'">' +
                                            '<i class="glyphicon glyphicon-chevron-down text-muted" ></i >' +
                                         '</div>' +
                                    '</div>'+
                                    '<div class="row user-infos '+value.Id+'"> ' +
                                        '<div class="col-xs-12 col-sm-12 col-md-10 col-lg-10 col-xs-offset-0 col-sm-offset-0 col-md-offset-1 col-lg-offset-1" >' +
                                            '<div class="panel panel-primary">' +
                                                '<div class="panel-heading">' +
                                                    '<h3 class="panel-title">Información de Usuario</h3>' +
                                                '</div>' +
                                                '<div class="panel-body">' +
                                                    '<div class="row">' +
                                                        '<div class="col-md-3 col-lg-3 hidden-xs hidden-sm">';
                        if (value.Sexo) {
                                            Data = Data + '<img class="img-circle" ' +
                                                            'src="../Content/Imagenes/M.png" ' +
                                                            'style="width: 100px; height: 100px;"' +
                                                            'alt="User Pic">';
                        }else{
                                            Data = Data + '<img class="img-circle" ' +
                                                            'src="../Content/Imagenes/W6.png" ' +
                                                            'style="width: 100px; height: 100px; background-color:black;" ' +
                                                            'alt="User Pic">';
                        }
                                            Data = Data +'</div>'+
                                                         '<div class=" col-md-9 col-lg-9 hidden-xs hidden-sm">' +
                                                                '<strong>'+value.Nombre1+'</strong><br>' +
                                                                    '<table class="table table-user-information" id="' + value.Id + '">' +
                                                                        '<tbody>' +
                                                                            '<tr>' +
                                                                                '<td>Nombre:</td>' +
                                                                                '<td>'+value.Nombre1+' '+Nombre2+'</td>' +
                                                                            '</tr>' +
                                                                    '</table >' +
                                                         '</div >' +
                                                    '</div >'+ 
                                                '</div >' +
                                                '<div class="panel-footer">';
                        if (value.Estado == true) {
                                           Data = Data + '<button class="btn btn-sm btn-danger" type="button" id="disable-'+value.Id+'" onclick="dis('+value.Id+', id);"' +
                                                            'data-toggle="tooltip"' +
                                                            'data-original-title="Desabilitar este usuario.">' +
                                                            'Deshabilitar <i class="glyphicon glyphicon-remove"></i>' +
                                                         '</button>';
                        }else{
                                           Data = Data + '<button class="btn btn-sm btn-success" type="button" id="enable-'+value.Id+'" onclick="dis('+value.Id+', id);"' +
                                                            'data-toggle="tooltip"' +
                                                            'data-original-title="Habilitar este usuario.">' +
                                                            'Habilitar <i class="glyphicon glyphicon-check"></i>' +
                                                         '</button>';
                        }
                                           Data = Data + '<span class="pull-right"> '+
                                                            '<a href="/UsersAdmin/Edit/'+value.Id+'" class="btn btn-sm btn-primary" type="button" data-toggle="tooltip" data-original-title="Editar este usuario.">' +
                                                                'Editar <i class="glyphicon glyphicon-edit"></i>' +
                                                            '</a>' +
                                                         '</span>' +
                                                     '</div>';
                                            '</div >' +
                                        '</div >' +
                                    '</div >';
                    
                        SetData.append(Data);

                                                })
                                            }
                                        }
                                    })
                            
                                }
                            

    var panels = $('.user-infos');
    var panelsButton = $('.dropdown-user');
    panels.hide();

    //Click dropdown
    panelsButton.click(function () {
        //get data-for attribute
        var dataFor = $(this).attr('data-for');
        var idFor = $(dataFor);

        //current button
        var currentButton = $(this);
        idFor.slideToggle(400, function () {
            //Completed slidetoggle
            if (idFor.is(':visible')) {
                currentButton.html('<i class="glyphicon glyphicon-chevron-up text-muted"></i>');
            }
            else {
                currentButton.html('<i class="glyphicon glyphicon-chevron-down text-muted"></i>');
            }
        })
    });

    $('[data-toggle="tooltip"]').tooltip();

});