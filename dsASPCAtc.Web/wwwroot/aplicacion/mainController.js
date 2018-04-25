atc.controller('main', function ($scope, $http, Llamada, $timeout, Carrito) {
    mensajeError = function (error) {
        DevExpress.ui.notify(error, "error", 2000);
    }
    mensajeExito = function (mensaje) {
        DevExpress.ui.notify(mensaje, "success", 2000);
    }

    $scope.dataGridOptions = {
        dataSource: "data/customers.json",
        columns: ["Código", "Características", "Almacenes", "PVP", "%", "Neto", "Pedir"]
    };
    $scope.ocultarSidebar = function () {
        alert("Holi")
    }

    $scope.rutaBuscador = rutaBuscador;
    $scope.rutaArticulo = rutaArticulo;

    $scope.popupOptions = {
        width: 660,
        height: 300,
        showTitle: true,
        title: "Bajo Pedido",
        fullScreen:false,
        dragEnabled: false,
        bindingOptions: {
            visible: 'bajopedidoVisible'
        },
        closeOnOutsideClick: true
    };

    $scope.bajoPedidoPopup = function(descripcion, eurocode) {
        $scope.bajopedidoVisible = true;
        $scope.bajoPedidoForm = { 
            descripcionArticulo: descripcion,
            eurocodeArticulo: eurocode,
            comentario: "",
            idCliente: document.getElementById("idCliente").value,
            nombreCliente: document.getElementById("nombreCliente").innerHTML,
        }
    }
    $scope.enviarBajoPedido = function() {
        $scope.bajopedidoVisible = false;
        Llamada.post("BajoPedidoEnviar", $scope.bajoPedidoForm)
            .then(function(respuesta) { 
                mensajeExito("Mensaje enviado correctamente");
                
            });
    }
    
    $scope.tamanoanterior = 0;
    $scope.mostrardesplegable = false;
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        //inputChangedPromise = $timeout(buscarArticulos, 1000);
        inputChangedPromise = $timeout(buscarMarcaModelos, 1000);
    }
    $scope.imagenArticulo = function (idArticulo) {
        return Llamada.rutaStreamingArticulo() + idArticulo;
    }
    $scope.imagenPorID = function (idImagenArticulo) {
        return Llamada.rutaStreamingImagenArticulo() + idImagenArticulo;
    }
    $scope.verResultados = function () {
        verResultados($scope.cadena);
    }
    buscarMarcaModelos = function () {
        $scope.loading = true;
        if ($scope.cadena.length > 3) {
            Llamada.get("MarcasYModelosLeerPorCadena?cadena=" + $scope.cadena)
                .then(function (respuesta) {
                    $scope.resultadobusqueda = respuesta.data.articulos;
                    $scope.resultadomarcas = respuesta.data.marcas;
                    $scope.NumReg = 0;
                    $scope.mostrardesplegable = true;
                    $scope.loading = false;
                    document.getElementById("desplegable").style.display = "inline";
                })
        } else {
            $scope.resultadobusqueda = [];
            $scope.resultadomarcas = [];
            $scope.NumReg = 0;
            $scope.loading = false;
        }
    }
    buscarArticulos = function () {
        $scope.loading = true;
        if ($scope.cadena.length > 0) {
            Llamada.get("ArticulosLeerPorCadena?cadena=" + $scope.cadena)
                .then(function (respuesta) {
                    $scope.resultadobusqueda = respuesta.data.articulos;
                    $scope.NumReg = respuesta.data.numReg;
                    $scope.mostrardesplegable = true;
                    $scope.loading = false;
                    document.getElementById("desplegable").style.display = "inline";
                })
        } else {
            $scope.resultadobusqueda = [];
            $scope.NumReg = 0;
            $scope.loading = false;
        }
    }
    fnocultar2 = function () {
        $scope.mostrardesplegable = false;
    }
    $scope.focofuera = function () {
        $timeout(fnocultar2, 1000);
    };
    $scope.entrafoco = function () {
        if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda)) {
            $scope.mostrardesplegable = true;
        }
    }
    $scope.buscarConCadena = function () {
        if ($scope.cadena.length > 3) {
            buscarCadena($scope.cadena);
            //window.location.href = "/AreaCliente/Resultados?euro=" + $scope.cadena;
        }

    }
    $scope.NotNull = function (val) {
        return NotNullNotUndefinedNotEmpty(val);
    }
    $scope.estaSeleccionado = function (id) {
        return $scope.domicilio == id;
    }

    $scope.esRutaActiva = function (ruta) {

        if (window.location.href.indexOf(ruta) > -1) {
            if (ruta == "Pedido") {
                if (window.location.pathname.indexOf("Pedidos") > -1) {
                    return "";
                } else {
                    return "active open";
                }
            } else {
                return "active open";
            }
        } else {
            return "";
        }
    }
    $scope.esRutaDefecto = function () {
        if (window.location.pathname == "/AreaCliente") {
            return "active open";
        } else {
            return "";
        }
    }
    $scope.anadirCarrito = function (idArticulo, cantidad, idUnidadManipulacion, noabrir) {
        if (noabrir === true) {
            ponerCapaCargando();
        }
        
        Carrito.anadirArticulo(idArticulo, cantidad, idUnidadManipulacion, noabrir)
            .then(function (respuesta) {

                console.log(respuesta.data);
                if (noabrir !== true) {
                    $("body").addClass("page-quick-sidebar-open");
                }
                quitarCapaCargando();

                $scope.carrito = respuesta.data;
                if (NotNullNotUndefinedNotEmpty($scope.carrito.mensaje)) {
                    mensajeError($scope.carrito.mensaje)
                }

                
            })
    }
    $scope.marcarMensajeLeido = function (idMensaje, idCliente, otrocliente) {
        //alert("EEOO")
        if (otrocliente > 0) {
            Llamada.get("MensajeMarcarLeido?idMensaje=" + idMensaje + "&IDCliente=" + idCliente)
            document.getElementById("mj " + idMensaje).style.display = "none";
            var cantidad = parseInt(document.getElementById("canmensajes").innerHTML);
            cantidad--;
            document.getElementById("canmensajes").innerHTML = cantidad;
            document.getElementById("canmensajes2").innerHTML = cantidad;
        }
        
    }
    $scope.eliminarCarrito = function () {

        result = DevExpress.ui.dialog.custom(
            {
                message: '¿Estás seguro de que deseas vaciar tu carrito?',
                buttons: [{
                    text: 'Si',
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
            Carrito.eliminarCarrito()
                .then(function (respuesta) {
                    $scope.carrito = respuesta.data;
                })
        });



        
    }
    $scope.eliminarArticulo = function (idArticulo, enProcesar) {
        if (enProcesar === true) {
            ponerCapaCargando();
        }
        Carrito.eliminarArticulo(idArticulo, enProcesar)
            .then(function (respuesta) {
                quitarCapaCargando();
                console.log(respuesta.data);
                if (enProcesar !== true) {
                    $("body").addClass("page-quick-sidebar-open");
                }
                
                $scope.carrito = respuesta.data;
            })
    }
    $scope.verCarrito = function () {
        Carrito.verCarrito()
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.carrito = respuesta.data;
            })
    }
    $scope.verPaginaCarrito = function() { 
            //window.location.href = "/AreaCliente/Pedido";
        verCarrito();
    }
    if (window.location.href.indexOf("/AreaCliente/Pedido") > -1) {
        Carrito.verCarrito(true)
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.carrito = respuesta.data;
                quitarCapaCargando();
            })
    } else {
        Carrito.verCarrito()
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.carrito = respuesta.data;
            })
    }
    quitarCapaCargando = function () {
        document.getElementById("capa_cargando").setAttribute("style", "width: 100%; height: 100%; z-index:1000;position:fixed; background-color:rgba(255,255,255,0.5); display:none;margin-top: -25px;margin-left: -20px;");
    }
    ponerCapaCargando = function () {
        document.getElementById("capa_cargando").setAttribute("style", "width: 100%; height: 100%; z-index:1000;position:fixed; background-color:rgba(255,255,255,0.5);margin-top: -25px;margin-left: -20px;");   
    }
    //quitarCapaCargando();

    
});
atc.controller('listaArticulos', function ($scope, $http, Llamada, $timeout) {
    var obj = {
        cadena: document.getElementById("cadena").value,
    }
    $scope.loading = true;
    Llamada.post("ArticulosBusquedaPaginada", obj)
        .then(function (respuesta) {
            $scope.loading = false;
            cambiarBotonesPaginacionIniciales("disabled");
            $scope.vm = respuesta.data;
            if (respuesta.data.articulos.length < 1) {
                cambiarBotonesPaginacion("disabled");

            }
            loopClase("listaArticulos")
        })
    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        Llamada.post("ArticulosBusquedaPaginada", $scope.vm.cm)
            .then(function (respuesta) {
                if (respuesta.data.articulos.length < 1) {
                    switch (val) {
                        case "N":
                            cambiarBotonesPaginacionFinales("disabled");
                            break;
                        case "P":
                            cambiarBotonesPaginacionIniciales("disabled");
                            break;
                    }
                } else {
                    $scope.vm = respuesta.data;

                }

            })
    }
    cambiarBotonesPaginacion = function (val) {
        console.log("Desactivando");
        document.getElementById("primera").className = val;
        document.getElementById("anterior").className = val;
        document.getElementById("siguiente").className = val;
        document.getElementById("ultima").className = val;
        document.getElementById("primera_b").className = val;
        document.getElementById("anterior_b").className = val;
        document.getElementById("siguiente_b").className = val;
        document.getElementById("ultima_b").className = val;
    }
    cambiarBotonesPaginacionIniciales = function (val) {
        console.log("Desactivando");
        document.getElementById("primera").className = val;
        document.getElementById("anterior").className = val;
        document.getElementById("primera_b").className = val;
        document.getElementById("anterior_b").className = val;
    }
    cambiarBotonesPaginacionFinales = function (val) {
        console.log("Desactivando");
        document.getElementById("siguiente").className = val;
        document.getElementById("ultima").className = val;
        document.getElementById("siguiente_b").className = val;
        document.getElementById("ultima_b").className = val;
    }
    loopClase = function (clase) {
        var elem = document.getElementsByClassName(clase);
        for (i = 0; i < elem.length; i++) {
            console.log(elem[i]);
            elem[i].setAttribute("style", "");
        }
        document.getElementById("loading" + clase).style.display = "none";
    }
});
atc.controller('DemoController', function DemoController($scope) {

});
atc.controller('productos', function ($scope, $http, Llamada, $timeout, Carrito) {
    $scope.tabCat = idcat;
    $scope.cambiarTabCategorias = function (val) {
        //alert(val)
        $scope.tabCat = val;
    }
    $scope.esActivo = function (val) {
        if (val == $scope.tabCat) {
            return "seleccionado";
        } else {
            return "";
        }
    }
});
atc.controller('productosvidrio', function ($scope, $http, Llamada, $timeout, Carrito) {
    $scope.tabVidrio = idtipovid;
    $scope.cambiarTabVidrio = function (val) {
        //alert(val)
        //alert("Holiii");
        $scope.tabVidrio = val;
    }
    $scope.esActivo = function (val) {
        if (val == $scope.tabVidrio) {
            return "seleccionado";
        } else {
            return "";
        }
    }
});

atc.controller('resultadosBusqueda', function ($scope, $http, Llamada, $timeout, Carrito) {
    document.getElementById("contenedorResultados").style.display = "inline";

    quitarCapaCargando()
});
atc.controller('generico', function ($scope, $http, Llamada, $timeout, Carrito) {
    quitarCapaCargando();
});
