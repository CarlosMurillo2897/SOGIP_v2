$(document).ready(function () {
    //_____________________________________________________________
    // La nomenclatura es la siguiente:
    // todo aquello que lleve un F se refiere a Femenno
    // todo aquello que lleve una M se refiere a Masculino
    // algunas funciones como por ejemplo  porMesF() extrae solo las mujeres de una selección, entidad...
    // si la función es porMesMFunc() se refiere a Masculino-Funcionario
    // aquellas funciones que terminen en Usuario se refiere a la extracción de un usuario específico en la consulta
    var mes = [], //id
        all = [],    //datasets
        meses = [], //nom
        allid = [], //id selecciones,entidades, asociaciones
        allnom = [], //nombre selecciones, entidades, asociaciones
        grupo = [], //sele, aso, entidad...
        extra = [],
        graficos = [],
        lineal = [],
        an = [],
        pc = [];
    yrs();
    //_____________________________________________________________
    var cedu = "";
    llenarTabla();
    selecciones();
    asociaciones();
    entidades();

    ///  ________________________SELECT'S___________________________
    $('#Grupo').multiselect({
        nonSelectedText: '--GRUPOS--',
        onChange: function (option, checked, select) {
            var brands = $('#Grupo option:selected');
            $(brands).each(function (index, brand) {
                grupo = [];
                grupo.push([$(this).val()]);
            });

            if ($(option).val() == 1) {//selección
                allid = [];
                allnom = [];
                $('#a2').hide();
                $('#e3').hide();
                $('#s1').show();
                $('#uss').hide();
                $('#month').show();
                $('#years').show();
                $('#bt').show();
            }
            else if ($(option).val() == 2) {//asociación
                allid = [];
                allnom = [];
                $('#s1').hide();
                $('#e3').hide();
                $('#a2').show();
                $('#uss').hide();
                $('#month').show();
                $('#years').show();
                $('#bt').show();
            }
            else if ($(option).val() == 3) {//entidades
                allid = [];
                allnom = [];
                $('#s1').hide();
                $('#a2').hide();
                $('#e3').show();
                $('#uss').hide();
                $('#month').show();
                $('#years').show();
                $('#bt').show();
            }
            else if ($(option).val() == 4) {//otros
                allid = [];
                allnom = [];
                $('#s1').hide();
                $('#a2').hide();
                $('#e3').hide();
                $('#uss').hide();
                $('#month').show();
                $('#years').show();
                $('#bt').show();
            }
            else if ($(option).val() == 5) {//otros
                allid = [];
                allnom = [];
                $('#s1').hide();
                $('#a2').hide();
                $('#e3').hide();
                $('#uss').show();
                $('#month').show();
                $('#years').show();
                $('#bt').show();
            }

            else {
                allid = [];
                allnom = [];
                $('#s1').hide();
                $('#a2').hide();
                $('#e3').hide();
                $('#uss').hide();
                $('#month').hide();
                $('#years').hide();
                $('#bt').hide();
            }


        }
    });
    $('#Meses').multiselect({//selecciona el id del mes y su nombre
        disableIfEmpty: true,
        enableFiltering: true,
        filterPlaceholder: 'Buscar...',
        buttonText: function (options, select) {
            return '-MESES-';
        },
        buttonTitle: function (options, select) {
            var labels = [];
            options.each(function () {
                labels.push($(this).text());
            });
            return labels.join(' - ');
        },
        onChange: function (element, checked) {
            mes = [];
            meses = [];
            var brands = $('#Meses option:selected');
            $(brands).each(function (index, brand) {
                mes.push([$(this).val()]);
                meses.push([$(this).text()]);
            });

        },
        includeSelectAllOption: true,
        selectAllText: 'seleccionar todos',
        onSelectAll: function (element, checked) {
            mes = [];
            meses = [];
            var brands = $('#Meses option:selected');
            $(brands).each(function (index, brand) {
                mes.push([$(this).val()]);
                meses.push([$(this).text()]);
            });
        }
    });
    $('#pie').multiselect({
        enableFiltering: true,
        buttonText: function (options, select) {
            return '-MESES-';
        },
        buttonTitle: function (options, select) {
            var labels = [];
            options.each(function () {
                labels.push($(this).text());
            });
            return labels.join(' - ');
        },
        onChange: function (element, checked) {
            mes = [];
            meses = [];
            var brands = $('#pie option:selected');
            $(brands).each(function (index, brand) {
                mes.push([$(this).val()]);
                meses.push([$(this).text()]);
            });
        }
    });
    $('#Graficos').multiselect({
        nonSelectedText: '--GRÁFICOS--',
        onChange: function (option, checked, select) {
            mes = [];
            meses = [];
            var brands = $('#Graficos option:selected');
            $(brands).each(function (index, brand) {
                graficos = [];
                graficos.push([$(this).val()]);
            });

            if ($(option).val() == 1) {//Selecciones
                $('#multigrupo').show();
                $('#pied').hide();
                $('#tipo').hide();
            }
            else if ($(option).val() == 2) {//Lineal se divide en genero ó todos los grupos
                $('#s1').hide();
                $('#a2').hide();
                $('#e3').hide();
                $('#uss').hide();
                $('#multigrupo').hide();
                $('#pied').hide();
                $('#tipo').show();
                $('#years').show();
                $('#month').show();
                $('#bt').show();
            }
            else if ($(option).val() == 3) {//Pastel es por un mes y se divide por grupos
                $('#s1').hide();
                $('#a2').hide();
                $('#e3').hide();
                $('#uss').hide();
                $('#tipo').hide();
                $('#multigrupo').hide();
                $('#month').hide();
                $('#years').show();
                $('#pied').show();
                $('#bt').show();
            }

            else {
                $('#s1').hide();
                $('#a2').hide();
                $('#e3').hide();
                $('#uss').hide();
                $('#multigrupo').hide();
                $('#month').hide();
                $('#bt').hide();
                $('#tipo').hide();
                $('#pied').hide();
                $('#years').hide();
            }

        }
    });
    $('#LinealTipo').multiselect({
        nonSelectedText: '--TIPO--',
        onChange: function (option, checked, select) {
            var brands = $('#LinealTipo option:selected');
            $(brands).each(function (index, brand) {
                lineal = [];
                lineal.push([$(this).val()]);
            });

        }
    });


    function yrs() {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/years",
            data: { 'mes': mes },
            success: function (data) {
                an.push("2019");

                $.each(data, function (i, v) {
                    $('#ann').append('<option value=' + i + '>' + v + '</option>');
                });

                $('#ann').multiselect({
                    disableIfEmpty: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Buscar...',
                    buttonText: function (options, select) {
                        return '-AÑO-';
                    },
                    onChange: function (element, checked) {
                        an = [];
                        var brands = $('#ann option:selected');
                        $(brands).each(function (index, brand) {
                            an.push([$(this).text()]);
                        });
                    }
                })

            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }
    //______________________________________________________________

    function selecciones() { //POPULATE SELECT OPTIONS WITH SELECCIONES
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/Selecciones",
            success: function (data) {
                $.each(data, function (i, v) {
                    $('#Selecciones').append('<option value=' + v.Id + '>' + v.Nombre + '</option>');
                });
                $('#Selecciones').multiselect({
                    disableIfEmpty: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Buscar...',
                    buttonText: function (options, select) {
                        return '-SELECCIONES-';
                    },
                    buttonTitle: function (options, select) {
                        var labels = [];
                        options.each(function () {
                            labels.push($(this).text());
                        });
                        return labels.join(' - ');
                    },
                    onChange: function (element, checked) {
                        allid = [];
                        allnom = [];
                        var brands = $('#Selecciones option:selected');
                        $(brands).each(function (index, brand) {
                            allid.push([$(this).val()]);
                            allnom.push([$(this).text()]);
                        });

                    },
                    includeSelectAllOption: true,
                    selectAllText: 'seleccionar todos',
                    onSelectAll: function (element, checked) {
                        allid = [];
                        allnom = [];
                        var brands = $('#Selecciones option:selected');
                        $(brands).each(function (index, brand) {
                            allid.push([$(this).val()]);
                            allnom.push([$(this).text()]);
                        });
                    }
                })
            },
            error: function (error) {
                // alert("Fallo");
            }
        })
    }

    function asociaciones() { //POPULATE SELECT OPTIONS WITH ASOCIACIONES
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/Asociaciones",
            success: function (data) {
                $.each(data, function (i, v) {
                    $('#Asociaciones').append('<option value=' + v.Id + '>' + v.Nombre + '</option>');
                });

                $('#Asociaciones').multiselect({
                    disableIfEmpty: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Buscar...',
                    buttonText: function (options, select) {
                        return '-ASOCIACIONES-';
                    },
                    buttonTitle: function (options, select) {
                        var labels = [];
                        options.each(function () {
                            labels.push($(this).text());
                        });
                        return labels.join(' - ');
                    },
                    onChange: function (element, checked) {
                        allid = [];
                        allnom = [];
                        var brands = $('#Asociaciones option:selected');
                        $(brands).each(function (index, brand) {
                            allid.push([$(this).val()]);
                            allnom.push([$(this).text()]);
                        });
                    },
                    includeSelectAllOption: true,
                    selectAllText: 'seleccionar todos',
                    onSelectAll: function (element, checked) {
                        allid = [];
                        allnom = [];
                        var brands = $('#Asociaciones option:selected');
                        $(brands).each(function (index, brand) {
                            allid.push([$(this).val()]);
                            allnom.push([$(this).text()]);
                        });
                    }
                })
            },
            error: function (error) {
                // alert("Fallo");
            }
        })
    }

    function entidades() { //POPULATE SELECT OPTIONS WITH ENTIDADES
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/Entidades",
            success: function (data) {
                $.each(data, function (i, v) {
                    $('#Entidades').append('<option value=' + v.Id + '>' + v.Nombre + '</option>');
                });

                $('#Entidades').multiselect({
                    disableIfEmpty: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Buscar...',
                    buttonText: function (options, select) {
                        return '-ENTIDADES-';
                    },
                    buttonTitle: function (options, select) {
                        var labels = [];
                        options.each(function () {
                            labels.push($(this).text());
                        });
                        return labels.join(' - ');
                    },
                    onChange: function (element, checked) {
                        allid = [];
                        allnom = [];
                        var brands = $('#Entidades option:selected');
                        $(brands).each(function (index, brand) {
                            allid.push([$(this).val()]);
                            allnom.push([$(this).text()]);
                        });
                    },
                    includeSelectAllOption: true,
                    selectAllText: 'seleccionar todos',
                    onSelectAll: function (element, checked) {
                        allid = [];
                        allnom = [];
                        var brands = $('#Entidades option:selected');
                        $(brands).each(function (index, brand) {
                            allid.push([$(this).val()]);
                            allnom.push([$(this).text()]);
                        });
                    }
                })
            },
            error: function (error) {
                // alert("Fallo");
            }
        })
    }


    $("#b").click(function () {
        all = [];

        if (graficos[0] == 1) {

            if (grupo.length < 1 || grupo[0] == 0) {
                bootbox1(" No ha seleccionado ninguna agrupación");
                return;
            }

            else if (grupo[0] == 4) {
                if (mes.length < 1) {
                    bootbox1(" No ha seleccionado ningún mes");
                    return;
                }
                if (an.length < 1) {
                    bootbox1(" No ha seleccionado ningún año");
                    return;
                }


                mesesFuncionarios(); //FUNCIONARIOS CARLOS
            }

            else if (grupo[0] == 5) {

                if (mes.length < 1) {
                    bootbox1(" No ha seleccionado ningún mes");
                    return;
                }
                if (cedu == "") {
                    bootbox1(" No ha seleccionado usuario");
                    return;
                }
                if (an.length < 1) {
                    bootbox1(" No ha seleccionado ningún año");
                    return;
                }
                mesesUsuario(); //USUARIOS PARTICULARES CARLOS
            }

            else {
                if (allid.length < 1) {
                    bootbox1(" No ha seleccionado ningún Selección, Asociación o Entidad");
                    return;
                }
                if (mes.length < 1) {
                    bootbox1(" No ha seleccionado ningún mes");
                    return;
                }
                if (an.length < 1) {
                    bootbox1(" No ha seleccionado ningún año");
                    return;
                }
                extra = allid.slice();
                console.log(extra);
                mesesAtletasA(allid); //ATLETAS CARLOS-> AQUI VARIA EN SI ES SELECCIÓN, ASOCIACIÓN O ENTIDAD
            }

        }
        else if (graficos[0] == 2) {
            if (lineal.length < 1) {
                bootbox1(" No ha seleccionado ningún tipo");
                return;
            }

            if (lineal[0] == 1) {
                if (mes.length < 1) {
                    bootbox1(" No ha seleccionado ningún mes");
                    return;
                }
                if (an.length < 1) {
                    bootbox1(" No ha seleccionado ningún año");
                    return;
                }
                allusersd();
            }
            else if (lineal[0] == 2) {
                if (mes.length < 1) {
                    bootbox1(" No ha seleccionado ningún mes");
                    return;
                }
                if (an.length < 1) {
                    bootbox1(" No ha seleccionado ningún año");
                    return;
                }
                allultimate();

            }


        }
        else if (graficos[0] == 3) {
            if (mes.length < 1) {
                bootbox1(" No ha seleccionado ningún mes");
                return;
            }
            if (an.length < 1) {
                bootbox1(" No ha seleccionado ningún año");
                return;
            }
            allpie();
        }

    });
    //____________________________________________ ESTOS SON LAS FUNCIONES PASTEL _______________________________
    function allpie() { // >>>>>>>>>>>>>>>>>>>>>> P
        var a = an[0];
        pc = [];
        allsele(mes, a);
        allaso(mes, a);
        allguber(mes, a);
        allfunc(mes, a);
        console.log(pc);
        bootbox4(" Cargando datos");
    }

    //____________________________________________ ESTOS SON LAS FUNCIONES LINEAL_______________________________
    //---------------------------------------------> MIXTO
    function allusers(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/allusers",
            data: { 'mes': mes, 'an': an },
            success: function (data) {
                all.push({
                    label: "Total de visitas",
                    fill: false,
                    data: data,
                    borderColor: 'rgba(54, 162, 235, 3)',
                    borderWidth: 1
                });

            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }
    function allwomen(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/allwomen",
            data: { 'mes': mes, 'an': an },
            success: function (data) {
                all.push({
                    label: "Mujeres",
                    fill: false,
                    data: data,
                    borderColor: 'rgb(255, 133, 51, 3)',
                    borderWidth: 1
                });
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }
    function allmen(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/allmen",
            data: { 'mes': mes, 'an': an },
            success: function (data) {
                all.push({
                    label: "Hombres",
                    fill: false,
                    data: data,
                    borderColor: 'rgb(0, 128, 43, 3)',
                    borderWidth: 1
                });
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }

    function allusersd() { // >>>>>>>>>>>>>>>>>>>>>> P

        allusers(mes, an[0]);
        allwomen(mes, an[0]);
        allmen(mes, an[0]);
        bootbox3(" Cargando datos");
    }

    //---------------------------------------------> AGRUPACIONES
    function allsele(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/allsele",
            data: { 'mes': mes, 'an': an },
            success: function (data) {
                all.push({
                    label: "Selecciones",
                    fill: false,
                    data: data,
                    borderColor: 'rgb(255, 51, 51, 3)',
                    borderWidth: 1
                });
                pc.push(data[0]);
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }
    function allaso(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/allaso",
            data: { 'mes': mes, 'an': an },
            success: function (data) {
                all.push({
                    label: "Asociaciones/Comites",
                    fill: false,
                    data: data,
                    borderColor: 'rgb(51, 204, 255,3)',
                    borderWidth: 1
                });
                pc.push(data[0]);
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }
    function allguber(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/allguber",
            data: { 'mes': mes, 'an': an },
            success: function (data) {
                all.push({
                    label: "Instituciones Gubernamentales",
                    fill: false,
                    data: data,
                    borderColor: 'rgb(0, 204, 153,3)',
                    borderWidth: 1
                });
                pc.push(data[0]);
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }
    function allfunc(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/allfunc",
            data: { 'mes': mes, 'an': an },
            success: function (data) {
                all.push({
                    label: "Funcionarios ICODER",
                    fill: false,
                    data: data,
                    borderColor: 'rgb(179, 102, 255,3)',
                    borderWidth: 1
                });
                pc.push(data[0]);
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }

    function allultimate() { // >>>>>>>>>>>>>>>>>>>>>> P
        var a = an[0];
        allsele(mes, a);
        allaso(mes, a);
        allguber(mes, a);
        allfunc(mes, a);
        bootbox3(" Cargando datos");
    }


    //____________________________________________ ESTOS SON LAS FUNCIONES BARRA_________________________________
    //La función mesesAtletasA(array):
    //esta función recibe como paramétro un arreglo, ya sea que este compuesto
    //por 1 ó más selecciones, asociaciones o entidades. Solo se Itera este arreglo


    function mesesAtletasA(array) { //SELECCIONES, ASOCIACIONES O ENTIDADES
        if (array.length > 0) {
            porMesF(mes, array[0], grupo[0], an[0]); //-> Femenino
            porMesM(mes, array[0], grupo[0], an[0]); //-> Masculino
            array.shift(); //Esto elimina siempre la posición [0]
            mesesAtletasA(array);
        }
        else {
            allid = extra.slice();
            bootbox2(" Cargando datos");

        }
    }

    function mesesFuncionarios() { //FUNCIONARIOS

        porMesFFunc(mes, an[0]); //-> Femenino
        porMesMFunc(mes, an[0]); //-> Masculino
        bootbox2(" Cargando datos");
    }

    function mesesUsuario() {// USUARIO EN PARTICULAR
        porMesUs(mes, cedu, an[0]);
        bootbox2(" Cargando datos");
    }

    //--------------------------------------------------Selecciones, entidades, asociaciones AJAX
    // Estas funciones se componen de lo siguiente:
    // 1) Un arreglo con los meses seleccionados
    // 2) El grupo: sea selección, entidad o asociación
    // 3) El Id de la selección, entidad o asociación.
    function porMesF(mes, grupo, idas, an) {
        var txt;
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/PorMesAtletasF",
            data: { 'mes': mes, 'aso': grupo, 'id': idas, 'an': an },
            success: function (data) {
                if (idas == 1) {
                    txt = $("#Selecciones option[value=" + grupo + "]").text();
                }
                else if (idas == 2) {
                    txt = $("#Asociaciones option[value=" + grupo + "]").text();
                }
                else if (idas == 3) {
                    txt = $("#Entidades option[value=" + grupo + "]").text();
                }


                all.push({
                    label: txt + "-Fem",
                    data: data,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                });
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }

    function porMesM(mes, grupo, idas, an) {
        var txt;
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/PorMesAtletasM",
            data: { 'mes': mes, 'aso': grupo, 'id': idas, 'an': an },
            success: function (data) {
                if (idas == 1) {
                    txt = $("#Selecciones option[value=" + grupo + "]").text();
                }
                else if (idas == 2) {
                    txt = $("#Asociaciones option[value=" + grupo + "]").text();
                }
                else if (idas == 3) {
                    txt = $("#Entidades option[value=" + grupo + "]").text();
                }

                all.push({
                    label: txt + "-Masc",
                    data: data,
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 0.2)',
                    borderWidth: 1
                });
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }

    //----------------------------------------------Usuario específico
    //Esta funcción se compone de lo siguiente:
    // 1) Un arreglo de meses seleccionados
    // 2) La cédula del usuario
    function porMesUs(mes, cedu, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/PorMesUsuario",
            data: { 'mes': mes, 'cedu': cedu, 'an': an },
            success: function (data) {

                all.push({
                    label: $('#usuario').val(),
                    data: data,
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 0.2)',
                    borderWidth: 1
                });
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }

    //--------------------------------------------------Funcionarios
    //Esta función solo se compone de:
    // 1) Un arreglo de meses
    function porMesFFunc(mes, an) {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/PorMesFFunc",
            data: { 'mes': mes, 'an': an },
            success: function (data) {

                all.push({
                    label: "Funcionarios ICODER-Fem",
                    data: data,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                });
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }
    function porMesMFunc(mes, an) {

        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: "/SOGIP/ControlIngreso/PorMesMFunc",
            data: { 'mes': mes, 'an': an },
            success: function (data) {

                all.push({
                    label: "Funcionarios ICODER-Masc",
                    data: data,
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 0.2)',
                    borderWidth: 1
                });
            },
            error: function (error) {
                bootbox1(" Error al recolectar datos");
            }
        })
    }

    //----------------------------------------------------bootbox-----------------------------------------------------------
    function bootbox2(message) {
        var dialog = bootbox.dialog({//para cargas
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-spin fa-spinner"></i>' + message + '</p>'
        });
        dialog.init(function () {
            setTimeout(function () {
                chartSe();
            }, 10000);
            setTimeout(function () {
                dialog.modal('hide');
            }, 10000)
        });

    }
    function bootbox1(message) {//para errores
        bootbox.alert({
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-exclamation-triangle"></i>' + message + '</p>'
        });

    }
    function bootbox3(message) {
        var dialog = bootbox.dialog({//para cargas
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-spin fa-spinner"></i>' + message + '</p>'
        });
        dialog.init(function () {
            setTimeout(function () {
                chartLine();
            }, 10000);
            setTimeout(function () {
                dialog.modal('hide');
            }, 10000)
        });

    }
    function bootbox4(message) {
        var dialog = bootbox.dialog({//para cargas
            size: 'small',
            closeButton: false,
            message: '<p><i class="fa fa-spin fa-spinner"></i>' + message + '</p>'
        });
        dialog.init(function () {
            setTimeout(function () {
                chartPie();
            }, 10000);
            setTimeout(function () {
                dialog.modal('hide');
            }, 10000)
        });

    }
    //----------------------------------------------------tablilla
    $('#botón').click(function () {
        $('#myModal').modal('show');
    });

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
                "url": "/SOGIP/ControlIngreso/getUsuariosA",
                "type": "GET",
                "dataSrc": ""
            },
            columns: [
                { data: "Accion" },
                { data: "Cedula" },
                { data: "Nombre" },
                { data: "Apellido1" },
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


    $('#example').on('click', 'td.select-checkbox', function () {
        var td = $(this);
        var tr = td.closest('tr');

        $('#usuario').val(tr.find('td:eq(1)').text() + ' ' + tr.find('td:eq(2)').text() + ' ' + tr.find('td:eq(3)').text() + ' ' + tr.find('td:eq(4)').text());
        cedu = tr.find('td:eq(1)').text();
        $('#myModal').modal('hide');
    });
    //_______________________________________________ GRÁFICOS   ____________________________________
    function chartSe() { //-----------> GENERA EL GRÁFICO
        $('#chart').remove();
        $('#graph').append('<canvas id="chart"></canvas>');


        var ctx = document.getElementById('chart');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: meses,
                datasets: all //----------> este es el arreglo de información, digamos [{nombre:, cantidad:....}, {nombre:, cantidad:}]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }

    function chartLine() { //-----------> GENERA EL GRÁFICO
        $('#chart').remove();
        $('#graph').append('<canvas id="chart"></canvas>');
        var ctx = document.getElementById('chart');
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: meses,
                datasets: all
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                elements: {
                    line: {
                        tension: 0 // disables bezier curves
                    }
                }
            }
        });
    }

    function chartPie() {
        $('#chart').remove();
        $('#graph').append('<canvas id="chart"></canvas>');
        var ctx = document.getElementById('chart');
        var myChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ['Selecciones', 'Asociaciones', 'Entidades Gubernamentales', 'Funcionarios ICODER'],
                datasets: [{
                    data: pc,
                    backgroundColor: [
                        "#FF6384",
                        "#63FF84",
                        "#84FF63",
                        "#8463FF"
                    ]
                }]


            }
        });
    }
});