atc = angular.module('atcapp', ['dx', 'ngSanitize',  'ui.bootstrap'] )

atc.factory('Llamada', function ($http, $q) {
    var api_url = "http://" + location.host + "/Data/";
    var api_stream = "http://" + location.host + "/StreamFiles/";
    var api_carrito = "http://" + location.host + "/Carrito/";
    var http = {
        get: function (url) {
            var deferred = $q.defer();
            $http.get(api_url + url)
                .then(function (respuesta) {
                    console.log(respuesta);
                    deferred.resolve(respuesta);
                })
            return deferred.promise;
        },
        getRuta: function (imagen) {
            return api_stream + "GetImagen?filename=" + imagen;
        },
        rutaStreamingArticulo: function () {
            return api_stream + "GetImagenPorIDArticulo?idarticulo=";
        },
        post: function (url, body) {
            console.log(body);
            var deferred = $q.defer();
            $.ajax({
                data: JSON.stringify(body),
                url: api_url + url,
                type: 'post',
                contentType: 'application/json',
                success: function (response) {
                    var res = { data: response }
                    console.log(res);
                    deferred.resolve(res);
                }
            });
            return deferred.promise;
        }
    }
    return http;
});
atc.factory('Carrito', function ($http, $q) {
    var api_carrito = "http://" + location.host + "/Carrito/";
    var idUsuario = document.getElementById("idusuarioweb").value;
    var http = {
        anadirArticulo: function (idArticulo, Cantidad, idUnidadManipulacion, enProcesar) {
            var deferred = $q.defer();
            $http.get(api_carrito + "AnadirArticulo?IDArticulo=" + idArticulo + "&IDUsuario=" + idUsuario + "&Cantidad=" + Cantidad + "&IDUnidadManipulacion=" + idUnidadManipulacion + "&EnProcesar=" + enProcesar)
                .then(function (respuesta) {
                    console.log(respuesta);
                    deferred.resolve(respuesta);
                })
            return deferred.promise;
        },
        eliminarArticulo: function (idArticulo, enProcesar) {
            var deferred = $q.defer();
            $http.get(api_carrito + "EliminarArticulo?IDArticulo=" + idArticulo + "&IDUsuario=" + idUsuario + "&EnProcesar=" + enProcesar)
                .then(function (respuesta) {
                    console.log(respuesta);
                    deferred.resolve(respuesta);
                })
            return deferred.promise;
        },
        eliminarCarrito: function () {
            var deferred = $q.defer();
            $http.get(api_carrito + "EliminarCarrito?&IDUsuario=" + idUsuario)
                .then(function (respuesta) {
                    console.log(respuesta);
                    deferred.resolve(respuesta);
                })
            return deferred.promise;
        },
        verCarrito: function (enProcesar) {
            var deferred = $q.defer();
            $http.get(api_carrito + "LeerCarrito?IDUsuario=" + idUsuario + "&EnProcesar=" + enProcesar)
                .then(function (respuesta) {
                    console.log(respuesta);
                    deferred.resolve(respuesta);
                })
            return deferred.promise;
        }
    }
    return http;
});
function NotNullNotUndefinedNotEmpty(val) {
    if (val !== null && val !== undefined && val !== "") {
        return true;
    } else {
        return false;
    }
}
function VacioSiUndefined(val) {
    if (NotNullNotUndefinedNotEmpty(val)) {
        return val;
    } else {
        return "";
    }
}
function cambiarPagina(a) {
    document.getElementById("accionpagina").value = a;
}