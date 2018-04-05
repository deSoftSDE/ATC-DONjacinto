atc.controller('vehiculos', function ($scope, $http, Llamada, $timeout) {
    
    buscarMarcas = function () {
        Llamada.get("MarcasLeer?IDTipoVehiculo=" + $scope.objetoBusqueda.idTipoVehiculo)
            .then(function (respuesta) {
                console.log(respuesta);
                $scope.inicialesMarcas = respuesta.data.iniciales;
                $scope.marcas = respuesta.data;
                for (i = 0; i < $scope.marcas.marcas.length; i++) {
                    $scope.marcas.marcas[i].url = Llamada.getRuta($scope.marcas.marcas[i].imagen);
                }
                for (o = 0; o < $scope.inicialesMarcas.length; o++) {
                    for (r = 0; r < $scope.inicialesMarcas[o].marcas.length; r++) {
                        $scope.inicialesMarcas[o].marcas[r].url = Llamada.getRuta($scope.inicialesMarcas[o].marcas[r].imagen);
                    }
                }
                $scope.selectbox.option("dataSource", $scope.marcas.marcas);
                //alert("Ok, data source colocado");
                console.log($scope.selectbox);
                if ($scope.marcas.marcas.length < 1) {
                    $scope.finalizarBusquedaTipoVehiculo();
                }
            })
    }
    buscarModelos = function () {
        Llamada.get("ModelosLeer?IDTipoVehiculo=" + $scope.objetoBusqueda.idTipoVehiculo + "&IDSeccion=" + $scope.objetoBusqueda.marca.idSeccion)
            .then(function (respuesta) {
                console.log(respuesta);
                $scope.modelos = respuesta.data;
                for (i = 0; i < $scope.modelos.length; i++) {
                    $scope.modelos[i].url = Llamada.getRuta($scope.modelos[i].imagen);
                }
                $scope.selectboxmodelos.option("dataSource", $scope.modelos);
                console.log($scope.modelos)
                if ($scope.modelos.length < 1) {
                    $scope.finalizaBusca();
                }
            })
    }
    buscarCarrocerias = function () {
        Llamada.get("ModelosLeerPorID?IDFamilia=" + $scope.objetoBusqueda.modelo.idFamilia)
            .then(function (respuesta) {
                console.log(respuesta);
                $scope.currentmodelo = respuesta.data;
                $scope.currentmodelo.url = Llamada.getRuta($scope.currentmodelo.imagen);
                if (!NotNullNotUndefinedNotEmpty($scope.currentmodelo.imagenes)) {
                    $scope.currentmodelo.imagenes = [];
                }
                for (i = 0; i < $scope.currentmodelo.imagenes.length; i++) {
                    $scope.currentmodelo.imagenes[i].url = Llamada.getRuta($scope.currentmodelo.imagenes[i].valor);

                }
                for (i = 0; i < $scope.currentmodelo.carrocerias.length; i++) {
                    $scope.currentmodelo.carrocerias[i].url = Llamada.getRuta($scope.currentmodelo.carrocerias[i].imagen);
                }
                $scope.objetoBusqueda.infoModelo = $scope.currentmodelo;
                if ($scope.currentmodelo.carrocerias.length < 1) {
                    $scope.finalizaBusca();
                }
            })
    }
    buscarAnos = function () {
        Llamada.get("AnosLeerPor?idmodelocarroceria=" + $scope.objetoBusqueda.carroceria.idModeloCarroceria + "&idfamilia=" + $scope.objetoBusqueda.modelo.idFamilia)
            .then(function (respuesta) {
                $scope.anos = respuesta.data;
                for (i = 0; i < $scope.anos.length; i++) {
                    $scope.anos[i].ano = $scope.anos[i].valor;
                }
                //$scope.anos.splice(0, 0, { valor: 'Todos los años', ano: 0, cantidadArticulos: 'muchos ' });
                //$scope.selectboxanos.option("dataSource", $scope.anos);
                if ($scope.anos.length < 1) {
                    $scope.anos.splice(0, 0, { valor: 'Todos los años', ano: 0, cantidadArticulos: 'muchos ' });
                    //$scope.anos.splice(0, 0, { valor: 'Todos los años', ano: 0, cantidadArticulos: 'muchos ' });
                    $scope.selectboxanos.option("dataSource", $scope.anos);
                    $scope.finalizarBusqueda();
                    
                } else {
                    $scope.anos.splice(0, 0, { valor: 'Todos los años', ano: 0, cantidadArticulos: 'muchos ' });
                    $scope.selectboxanos.option("dataSource", $scope.anos);
                }
            })
    }
    mostrarCarroceria = function () {
        Llamada.get("CarroceriasLeerEsquema?idmodelocarroceria=" + $scope.objetoBusqueda.carroceria.idModeloCarroceria + "&ano=" + $scope.objetoBusqueda.ano.ano)
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.tabla = respuesta.data.filas;
                for (i = 0; i < $scope.tabla.length; i++) {
                    for (h = 0; h < $scope.tabla[i].celdas.length; h++) {
                        if (NotNullNotUndefinedNotEmpty($scope.tabla[i].celdas[h].vidrio)) {
                            $scope.tabla[i].celdas[h].vidrio.url = Llamada.getRuta($scope.tabla[i].celdas[h].vidrio.imagen);
                        }

                    }
                }
                if (respuesta.data.hayArticulos === true) {
                    
                } else {
                    $scope.finalizarBusqueda();
                }
                
            })
    }
    $scope.objetoBusqueda = {
        idTipoVehiculo: 0,
        NombreTipoVehiculo: 'Todos los vehículos',
        marca: {
            descripcionSeccion: 'Todas las marcas',
            idSeccion: 0,
        },
        modelo: {
            descripcionFamilia: 'Todos los modelos',
            idFamilia: 0
        },
        carroceria: {
            descripcion: 'Todas las carrocerías',
            idCarroceria: 0,
            idModeloCarroceria: 0
        },
        ano: {
            valor: 'Todos los años',
            ano: 0,
            cantidadArticulos: 'muchos '
        },
        vidrio: {
            idVidrio: 0
        },
        tabs: [
            false,
            false,
            true,
            true,
            true,
            true,
            true
        ]
    }
    
    document.getElementById("contenedorBuscador").style.display = "inline";
    $scope.selectedTipoVehiculo = function (e) {
        if (e == $scope.objetoBusqueda.idTipoVehiculo) {
            return "seleccionado";
        } else {
            return "";
        }
    }
    $scope.seleccionarVehiculo = function (a, nombre) {
        console.log(a);
        $scope.objetoBusqueda.idTipoVehiculo = a;
        $scope.objetoBusqueda.NombreTipoVehiculo = nombre;
        $scope.objetoBusqueda.tabs[2] = false;
        $scope.objetoBusqueda.tabs[3] = true;
        $scope.objetoBusqueda.tabs[4] = true;
        $scope.objetoBusqueda.tabs[5] = true;
        $scope.objetoBusqueda.tabs[6] = true;
        changeUrl("vehiculo", a);
        cambiarPagina(2);
    }
    $scope.seleccionarMarca = function (mc) {
        console.log(mc);
        setMarca(mc);
        changeUrl("marca", mc.idSeccion);
    }
    $scope.seleccionarModelo = function (mc) {
        console.log(mc);
        setModelo(mc);
        changeUrl("modelo", mc.idFamilia);
    }
    $scope.seleccionarVidrio = function(vd) {
        $scope.objetoBusqueda.vidrio = vd;
        $scope.finalizaBusca();
    }
    $scope.seleccionarCarroceria = function (car) {
        $scope.objetoBusqueda.carroceria = JSON.parse("" + JSON.stringify(car));
        $scope.objetoBusqueda.tabs[5] = false;
        $scope.objetoBusqueda.tabs[6] = false;
        cambiarPagina(5);
    }
    $scope.seleccionarAno = function (ano) {
        var newano = JSON.parse(""+ JSON.stringify(ano));
        $scope.objetoBusqueda.ano = newano;
        $scope.objetoBusqueda.tabs[6] = false;
        cambiarPagina(6);
    }
    setMarca = function (mc) {
        var newmarca = JSON.parse("" + JSON.stringify(mc));
        $scope.objetoBusqueda.marca = newmarca;
        $scope.objetoBusqueda.tabs[3] = false;
        $scope.objetoBusqueda.tabs[4] = true;
        $scope.objetoBusqueda.tabs[5] = true;
        $scope.objetoBusqueda.tabs[6] = true;
        cambiarPagina(3);
    }
    setModelo = function (newmodelo) {
        $scope.objetoBusqueda.modelo = newmodelo;
        $scope.objetoBusqueda.tabs[4] = false;
        $scope.objetoBusqueda.tabs[5] = true;
        $scope.objetoBusqueda.tabs[6] = true;
        cambiarPagina(4);
    }
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscaMarcas, 1000);
    }
    cambiarPagina = function (val) {
        if (!$scope.objetoBusqueda.tabs[val]) {
            $scope.activePill = val;
            lanzarCambioPagina()
        } else {
            //alert("No puedo cambiar...")
        }
        
    }

    $scope.selectBox = {
        dataSource: [],
        placeholder:"Escriba aquí el nombre de la marca ",
        //dropDownButtonTemplate:"dropDownButton",
        displayExpr: "descripcionSeccion",
        valueExpr: "idSeccion",
        searchEnabled: true,
        noDataText: 'No se ha encontrado ninguna marca',
        //value: products[0].idSeccion,
        //fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectbox = e.component;
        },
        onValueChanged: function (e) {
            console.log(e);
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options)
                console.log(e.component._options.selectedItem)
                
                $scope.lastMarca = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                console.log($scope.lastMarca);
                //alert("holiiii");
                $scope.seleccionarMarca($scope.lastMarca);
            } else {
                console.log(e);
                console.log("Está vacío!!");
                oldValue = e.previousValue
            }

        }
    }
    $scope.classTieneArticulos = function (celda) {
        if (NotNullNotUndefinedNotEmpty(celda)) {
            if (NotNullNotUndefinedNotEmpty(celda.vidrio)) {
                if (NotNullNotUndefinedNotEmpty(celda.vidrio.cantidadArticulos)) {
                    if (celda.vidrio.cantidadArticulos > 0) {
                        return "celda";
                    } else {
                        return "";
                    }

                } else {

                    return "";
                }
            } else {
                return "";
            }
        } else {
            return "";
        }
    }
    $scope.claseestaGris = function (celda) {
        if (NotNullNotUndefinedNotEmpty(celda)) {
            if (NotNullNotUndefinedNotEmpty(celda.vidrio)) {
                if (NotNullNotUndefinedNotEmpty(celda.vidrio.cantidadArticulos)) {
                    if (celda.vidrio.cantidadArticulos > 0) {
                        return "nogris";
                    } else {
                        return "gris";
                    }

                } else {

                    return "gris";
                }
            } else {
                return "gris";
            }
        } else {
            return "gris";
        }
    }

    $scope.selectBoxAnos = {
        dataSource: [],
        placeholder: "Escriba aquí el año del vehículo ",
        displayExpr: "valor",
        valueExpr: "valor",
        searchEnabled: true,
        noDataText: 'No se ha encontrado ningún año',
        //value: products[0].idSeccion,
        //fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectboxanos = e.component;
        },
        onValueChanged: function (e) {
            console.log(e);
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options)
                console.log(e.component._options.selectedItem);
                $scope.lastAno = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                $scope.seleccionarAno($scope.lastAno);
            } else {
                console.log(e);
                console.log("Está vacío!!");
                oldValue = e.previousValue;
            }

        }
    }

    $scope.selectBoxModelos = {
        dataSource: [],
        placeholder: "Escriba aquí el modelo del vehículo ",
        displayExpr: "descripcionFamilia",
        valueExpr: "idFamilia",
        searchEnabled: true,
        noDataText: 'No se ha encontrado ningún modelo',
        //value: products[0].idSeccion,
        //fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectboxmodelos = e.component;
        },
        onValueChanged: function (e) {
            console.log(e);
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options)
                console.log(e.component._options.selectedItem)
                $scope.lastModelo = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                $scope.seleccionarModelo($scope.lastModelo);
            } else {
                console.log(e);
                console.log("Está vacío!!");
                oldValue = e.previousValue
            }

        }
    }
    $scope.activePill = 1;
    $scope.numPags = 6;
    $scope.progreso = function () {
        var res = parseInt(($scope.activePill * 100) / $scope.numPags);
        return res + "%";
    }
    $scope.botonAnterior = function () {
        if ($scope.activePill == 1) {
            return "disabled";
        } else if ($scope.objetoBusqueda.tabs[$scope.activePill-1]) {
            return "disabled";
        } else {
            return "";
        }
    }
    $scope.botonSiguiente = function () {
        if ($scope.activePill == $scope.numPags) {
            return "disabled";
        } else if ($scope.objetoBusqueda.tabs[$scope.activePill + 1]) {
            return "disabled";
        } else {
            return "";
        }
    }
    $scope.botonFinalizar = function () {
        if ($scope.objetoBusqueda.tabs[4] === false) {
            return "";
        } else {
            return "disabled";
        }
    }
    $scope.finalizarBusqueda = function () {
        var busquedaactual = $scope.objetoBusqueda.NombreTipoVehiculo + " > " + $scope.objetoBusqueda.marca.descripcionSeccion + " > " + $scope.objetoBusqueda.modelo.descripcionFamilia;
        //if (val === true) {
        //    busquedaactual = busquedaactual + " > " + $scope.objetoBusqueda.ano.valor;
        //}
       // result = DevExpress.ui.dialog.confirm("No hay resultados que coincidan con los parámetros introducidos hasta ahora. ¿Deseas buscar en " + busquedaactual + "?");
        result = DevExpress.ui.dialog.custom(
            {
                message: 'No hay productos que coincidan con los parámetros introducidos ¿te mostramos los resultados de ' + busquedaactual + '?',
                buttons: [{
                    text: 'Si, muestrame todos los resultados',
                    onClick: function () {
                        return true;
                    }
                },
                {
                    text: 'No',
                    onClick: function () {
                        return false;
                    }
                }
                ]

            }).show();
        result.then(function (val) {
            if (val) {
                //alert("OK, pues me salgo de aquí");
                $scope.finalizaBusca();
            } else {
                var c = +($scope.activePill) + +1;
                $scope.objetoBusqueda.tabs[c] = false;
                cambiarPagina(c);
            }
        });
    }
    $scope.finalizaBusca = function () {
        if (isNaN($scope.objetoBusqueda.ano.valor)) {
            $scope.objetoBusqueda.ano.valor = 0;
        }
        if ($scope.objetoBusqueda.modelo.idFamilia < 1) {
            var parametros = "?marca=" + $scope.objetoBusqueda.marca.idSeccion;
            verResultados(parametros);
        } else {
            //alert("Resultados normales")
            var parametros = "?modelo=" + $scope.objetoBusqueda.modelo.idFamilia + "&periodo=" + $scope.objetoBusqueda.ano.valor + "&carroceria=" + $scope.objetoBusqueda.carroceria.idModeloCarroceria + "&vidrio=" + $scope.objetoBusqueda.vidrio.idVidrio;
            verResultados(parametros);
        }
        
        //alert("Saliendo...");
    }
    $scope.finalizarBusquedaTipoVehiculo = function () {
        //result = DevExpress.ui.dialog.confirm("No hay resultados que coincidan con los parámetros introducidos hasta ahora. ¿Deseas buscar en todos los vehículos?");
        result = DevExpress.ui.dialog.custom({
            message: 'No hay productos asociados a este tipo de vehículo. ¿Deseas buscar en  todos los tipos de vehículo?',
            buttons: [{
                text: 'Si',
                onClick: function () {
                    return true;
                }
            },
                //{
                //    text: 'No',
                //    onClick: function () {
                //        return false;
                //    }
                //}
            ]
        }).show();
        result.then(function (val) {
            if (val) {
                $scope.objetoBusqueda.NombreTipoVehiculo = "Todos los vehículos";
                $scope.objetoBusqueda.idTipoVehiculo = 0;
                cambiarPagina($scope.activePill);
                //alert("OK, pues me salgo de aquí");
            }
        });
    }


    $scope.pestanaSiguiente = function () {
        if ($scope.botonSiguiente() == "") {
            $scope.activePill++;
            lanzarCambioPagina();
        }
        
    }
    $scope.pestanaAnterior = function () {
        console.log($scope.activePill);
        if ($scope.botonAnterior() == "") {
            $scope.activePill--;
            lanzarCambioPagina();
        }
       
    }
    $scope.lanzarCambioPagina = function (val) {
        lanzarCambioPagina(val);
        console.log($scope.objetoBusqueda);
    }
    lanzarCambioPagina = function (val) {
        console.log($scope.objetoBusqueda);
        if (NotNullNotUndefinedNotEmpty(val)) {
            resolverSwitchTab(val);
        } else {
            resolverSwitchTab($scope.activePill);
        }
        
    }
    resolverSwitchTab = function (val) {
        switch (parseInt(val)) {
            case 1:
                break;
            case 2:
                buscarMarcas();
                break;
            case 3:
                buscarModelos();
                break;
            case 4:
                buscarCarrocerias();
                break;
            case 5:
                buscarAnos();
                break;
            case 6:
                mostrarCarroceria();
                break;
        }
    }
    var objQueryString = {};
    getParameterByName = function(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
    changeUrl = function(key, value) {
        //Get query string value
        var searchUrl = location.search;
        if (searchUrl.indexOf("?") == "-1") {
            var urlValue = '?' + key + '=' + value;
            history.pushState({ state: 1, rand: Math.random() }, '', urlValue);
        }
        else {
            //Check for key in query string, if not present
            if (searchUrl.indexOf(key) == "-1") {
                var urlValue = searchUrl + '&' + key + '=' + value;
            }
            else {	//If key present in query string
                oldValue = getParameterByName(key);
                if (searchUrl.indexOf("?" + key + "=") != "-1") {
                    urlValue = searchUrl.replace('?' + key + '=' + oldValue, '?' + key + '=' + value);
                }
                else {
                    urlValue = searchUrl.replace('&' + key + '=' + oldValue, '&' + key + '=' + value);
                }
            }
            history.pushState({ state: 1, rand: Math.random() }, '', urlValue);
            //history.pushState function is used to add history state.
            //It takes three parameters: a state object, a title (which is currently ignored), and (optionally) a URL.
        }
        objQueryString.key = value;
        //sendAjaxReq(objQueryString);
    }




    try {
        var pr = document.getElementById("parametros").value;
        console.log(pr);
        console.log(JSON.parse(pr));
        var parametros = JSON.parse(pr);
        if (NotNullNotUndefinedNotEmpty(parametros.idTipoVehiculo)) {
            if (!isNaN(parametros.idTipoVehiculo)) {
                $scope.objetoBusqueda.idTipoVehiculo = parametros.idTipoVehiculo;
                $scope.objetoBusqueda.NombreTipoVehiculo = parametros.descripcionTipoVehiculo;
                $scope.objetoBusqueda.tabs[2] = false;
                cambiarPagina(2);
            }
        }
        if (NotNullNotUndefinedNotEmpty(parametros.idSeccion)) {
            if (parametros.idSeccion > 0) {
                $scope.objetoBusqueda.marca.idSeccion = parametros.idSeccion;
                $scope.objetoBusqueda.marca.descripcionSeccion = parametros.descripcionSeccion;
                $scope.objetoBusqueda.tabs[2] = false;
                $scope.objetoBusqueda.tabs[3] = false;
                $scope.objetoBusqueda.tabs[4] = true;
                $scope.objetoBusqueda.tabs[5] = true;
                $scope.objetoBusqueda.tabs[6] = true;
                cambiarPagina(3);
            }
        }
        if (NotNullNotUndefinedNotEmpty(parametros.idFamilia)) {
            if (parametros.idFamilia > 0) {
                $scope.objetoBusqueda.modelo.idFamilia = parametros.idFamilia;
                $scope.objetoBusqueda.modelo.descripcionFamilia = parametros.descripcionFamilia;
                $scope.objetoBusqueda.tabs[2] = false;
                $scope.objetoBusqueda.tabs[3] = false;
                $scope.objetoBusqueda.tabs[4] = false;
                $scope.objetoBusqueda.tabs[5] = true;
                $scope.objetoBusqueda.tabs[6] = true;
                console.log($scope.objetoBusqueda);
                cambiarPagina(4);
            }
        }
        if (!NotNullNotUndefinedNotEmpty($scope.objetoBusqueda.NombreTipoVehiculo)) {
            $scope.objetoBusqueda.idTipoVehiculo = 0;
            $scope.objetoBusqueda.NombreTipoVehiculo = 'Todos los vehículos';
        }
    }
    catch (Ex) {
        console.log(Ex);
    }

});