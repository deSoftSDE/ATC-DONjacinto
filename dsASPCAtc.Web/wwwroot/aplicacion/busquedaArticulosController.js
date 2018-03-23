atc.controller('busquedaArticulos', function($scope, $http, Llamada, $timeout) {
    $scope.parametrosBusqueda = c;
    var b = document.getElementById("parametros").value;
    var c = JSON.parse(b);
    $scope.parametrosBusqueda = c;
    console.log(c);
});