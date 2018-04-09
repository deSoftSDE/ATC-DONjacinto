atc.controller('facturas', function($scope, $http, Llamada, $timeout) {
    var b = document.getElementById("idCliente").value;
    $scope.pagina = 1;
    $scope.bloque = 20; 
    $scope.nFactura = "";
    $scope.fechaDesde = null;
    $scope.fechaHasta = null;
    leerFacturas = function () {
        $scope.fechaDesde = document.getElementById("fechaDesde").value;
        $scope.fechaHasta = document.getElementById("fechaHasta").value;
        Llamada.get("FacturasLeer?idCliente=" + b + "&pagina=" + $scope.pagina + "&nFactura=" + $scope.nFactura + "&bloque=" + $scope.bloque + "&fechaDesde=" + $scope.fechaDesde + "&fechaHasta=" + $scope.fechaHasta)
            .then(function (respuesta) {
                $scope.registros = respuesta.data.registros;
                $scope.facturas = respuesta.data.facturas;
                $scope.actualPag = JSON.parse("" + JSON.stringify($scope.pagina));
                $scope.numPag = parseInt(+($scope.registros / $scope.bloque) + +1);
                $scope.datagridoptions.option("dataSource", $scope.facturas);
                if ($scope.actualPag > $scope.numPag) {
                    $scope.datagridoptions.option("dataSource", []);
                }
            });
    }
    var inputPaginaPromise;
    $scope.cambioPagina = function () {
        if (inputPaginaPromise) {
            $timeout.cancel(inputPaginaPromise);
        }
        inputPaginaPromise = $timeout(leerFacturas(), 1000);
    };
    $scope.cambioPaginaDesdeUno = function () {
        if (inputPaginaPromise) {
            $timeout.cancel(inputPaginaPromise);
        }
        inputPaginaPromise = $timeout(leerFacturasPag1(), 1000);
    };
    leerFacturasPag1 = function () {
        $scope.pagina = 1;
        leerFacturas();
    }
    $scope.limpiarFacturas = function () {
        //alert("Holi");
        $scope.pagina = 1;
        $scope.nFactura = "";
        document.getElementById("fechaDesde").value = "";
        document.getElementById("fechaHasta").value = "";
        leerFacturas();

    }
    $scope.filtrarFacturas = function () {
        $scope.pagina = 1;
        leerFacturas();
    }
    $scope.paginaAnterior = function () {
        if ($scope.pagina > 1) {
            $scope.pagina--;
            leerFacturas();
        }
    };
    $scope.paginaSiguiente = function () {
        if ($scope.pagina < $scope.numPag) {
            $scope.pagina++;
            leerFacturas();
        }
        
    };
    


    $scope.dataGridOptions = {
        dataSource: [],
        keyExpr: "idFactura",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        noDataText: "No hay facturas para mostrar",
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "documento",
                caption: "Factura",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "fechaDocumento",
                caption: "Fecha",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                dataType: "date",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "totalBaseImponible",
                caption: "Base Imponible",
                width: "30%",
                allowFiltering: false,
                cellTemplate: "currencyTemplate",
                dataType: 'number',
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "importeLiquido",
                caption: "Importe Líquido",
                cellTemplate: "currencyTemplate",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, 
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagridoptions = e.component;
            leerFacturas();
        }
    };


    $scope.chartOptions = {
        dataSource: [],
        legend: {
            visible: false
        },
        series: {
            type: "bar"
        },
        argumentAxis: {
            label: {
                format: {
                    type: "decimal"
                }
            }
        },
        title: "Evolución mensual",
        onInitialized: function (e) {
            console.log(e);
            $scope.chartoptions = e.component;
            Llamada.get("FacturasMensualesLeer?idcliente=" + b)
                .then(function (respuesta) {
                    $scope.chartoptions.option("dataSource", respuesta.data)
                })
        }
    };
    document.getElementById("ocultocargando").style.display = "inline";
});