atc = angular.module('atcapp', [] )

atc.factory('Llamada', function ($http, $q) {
    var api_url = "http://" + location.host + "/Data/";
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