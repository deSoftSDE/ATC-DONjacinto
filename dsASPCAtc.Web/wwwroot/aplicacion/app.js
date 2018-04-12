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

function mostrarCargando() {
    anchoCargando();
    $("#capa_cargando").css({ opacity: 0, display: "block" }).animate({ opacity: 1, zIndex: 100 }, 500);
}

//oculta la capa emergente Cargando...
function ocultarCargando() {
    //display->none no funciona en animate
    $("#capa_cargando").css({ opacity: 1 }).animate({ opacity: 0, zIndex: 0 }, 500);
    $("#capa_cargando").css({ display: "none" });
}

//redimensiona el ancho del modal cargando
function anchoCargando() {
    //ancho
    var menu_vertical = $("#menu_vertical").width();
    if (menu_vertical > 0) {
        var ancho_ventana = $(window).width();

        var ancho_cargando = ancho_ventana - menu_vertical;
        $("#capa_cargando").width(ancho_cargando);
    }

    //alto
    var menu_superior = $("#menu_superior").height();
    if (menu_superior > 0) {
        var alto_ventana = $(window).height();

        var alto_cargando = alto_ventana - menu_superior;
        $("#capa_cargando").height(alto_cargando);
    }
}
function clickCapaCargando() {
    document.getElementById("capa_cargando").setAttribute("style", "width: 100%; height: 100%; z-index:1000;position:fixed; background-color:rgba(255,255,255,0.5);");
}