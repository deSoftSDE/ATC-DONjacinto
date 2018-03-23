atc = angular.module('atcapp', ['dx', 'ngSanitize',  'ui.bootstrap'] )

atc.factory('Llamada', function ($http, $q) {
    var api_url = "http://" + location.host + "/Data/";
    var api_stream = "http://" + location.host + "/StreamFiles/";
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