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
                    $scope.resultadobusqueda = respuesta.data;
                    $scope.mostrardesplegable = true;
                    $scope.loading = false;
                })
        }
    }
    /*cambiobusqueda = function () {
        if ($scope.cadena.length == 4 && $scope.tamanoanterior < 4) {
            $scope.loading = true;
            
        } else if ($scope.cadena.length < 4) {
            $scope.mostrardesplegable = false;
        } else {
            if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda)) {
                var count = 0;
                for (i = 0; i < $scope.resultadobusqueda.length; i++) {
                    if ($scope.resultadobusqueda[i].descripcion.toLowerCase().indexOf($scope.cadena.toLowerCase()) > -1) {
                        count++;
                    }
                }
                if (count > 0) {
                    $scope.mostrardesplegable = true;
                } else {
                    $scope.loading = true;
                    Llamada.get("ArticulosLeerPorCadena?cadena=" + $scope.cadena)
                        .then(function (respuesta) {
                            $scope.resultadobusqueda = respuesta.data;
                            $scope.mostrardesplegable = true;
                            $scope.loading = false;
                        })
                }
            }
        }
        $scope.tamanoanterior = parseInt("" + $scope.cadena.length);
    }
    */
});