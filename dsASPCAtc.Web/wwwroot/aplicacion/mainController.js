atc.controller('main', function ($scope, $http, Llamada, $timeout) {
    $scope.tamanoanterior = 0;
    $scope.mostrardesplegable = false;
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscarArticulos, 1000);
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
    $scope.focofuera = function () {
        $scope.mostrardesplegable = false;
    }
    $scope.entrafoco = function () {
        if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda)) {
            $scope.mostrardesplegable = true;
        }
    }
});
atc.controller('listaArticulos', function ($scope, $http, Llamada, $timeout) {
    var obj = {
        cadena: document.getElementById("cadena").value,
    }
    Llamada.post("ArticulosBusquedaPaginada", obj)
        .then(function (respuesta) {
            $scope.vm = respuesta.data;
        })
    $scope.cambiarPagina = function (val) {
        $scope.vm.cm.accionPagina = val;
        Llamada.post("ArticulosBusquedaPaginada", $scope.vm.cm)
            .then(function (respuesta) {
                if (respuesta.data.articulos.length < 1) {

                } else {
                    $scope.vm = respuesta.data;
                }
                
            })
    }
});