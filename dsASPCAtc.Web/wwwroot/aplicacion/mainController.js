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

    $scope.tamanoanterior = 0;
    $scope.mostrardesplegable = false;
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscarArticulos, 1000);
    }
    $scope.imagenArticulo = function (idArticulo) {
        return Llamada.rutaStreamingArticulo() + idArticulo;
    }
    $scope.verResultados = function () {
        verResultados($scope.cadena);
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
    fnocultar = function () {
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
    $scope.NotNull = function (val) {
        return NotNullNotUndefinedNotEmpty(val);
    }
    $scope.anadirCarrito = function (idArticulo, cantidad) {
        Carrito.anadirArticulo(idArticulo, cantidad)
            .then(function (respuesta) {

                console.log(respuesta.data);
                $("body").addClass("page-quick-sidebar-open");
                $scope.carrito = respuesta.data;
            })
    }
    $scope.eliminarArticulo = function (idArticulo) {
        Carrito.eliminarArticulo(idArticulo)
            .then(function (respuesta) {

                console.log(respuesta.data);
                $("body").addClass("page-quick-sidebar-open");
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
    Carrito.verCarrito()
        .then(function (respuesta) {
            console.log(respuesta.data);
            $scope.carrito = respuesta.data;
        })
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
});