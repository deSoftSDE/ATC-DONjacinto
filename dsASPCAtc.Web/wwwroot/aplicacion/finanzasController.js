atc.controller('finanzas', function($scope, $http, Llamada, $timeout) {
    var b = document.getElementById("idCliente").value;
    $scope.activetab = 1;
    document.getElementById("activetab").value;
    if (NotNullNotUndefinedNotEmpty(document.getElementById("activetab").value)) {
        $scope.activetab = parseInt(document.getElementById("activetab").value);
    }
    $scope.pagina = 1;
    $scope.bloque = 20; 
    $scope.paginacionFinanzas = {
        pc: {
            pagina: 1,
            bloque: 20,
            llamada: function () {
                Llamada.get("FinanzasDebitosPendientesLeer?idcliente=" + b + "&bloque=" + this.bloque + "&pagina=" + this.pagina)
                    .then(function (respuesta) {
                        $scope.paginacionFinanzas.pc.registros = respuesta.data.registros;
                        $scope.paginacionFinanzas.pc.actualPag = JSON.parse("" + JSON.stringify($scope.paginacionFinanzas.pc.pagina));
                        $scope.paginacionFinanzas.pc.numPag = parseInt(+($scope.paginacionFinanzas.pc.registros / $scope.paginacionFinanzas.pc.bloque));
                        if ($scope.paginacionFinanzas.pc.registros % $scope.paginacionFinanzas.pc.bloque > 0) {
                            $scope.paginacionFinanzas.pc.numPag++;
                        }
                        $scope.datagridpendientes.option("dataSource", respuesta.data.contenido);
                        quitarCapaCargando();
                    })
            }
        },
        em: {
            pagina: 1,
            bloque: 20,
            llamada: function () {
                Llamada.get("FinanzasExtractosLeer?idDeudor=" + b + "&bloque=" + this.bloque + "&pagina=" + this.pagina)
                    .then(function (respuesta) {
                        $scope.paginacionFinanzas.em.registros = respuesta.data.registros;
                        $scope.paginacionFinanzas.em.actualPag = JSON.parse("" + JSON.stringify($scope.paginacionFinanzas.em.pagina));
                        $scope.paginacionFinanzas.em.numPag = parseInt(+($scope.paginacionFinanzas.em.registros / $scope.paginacionFinanzas.em.bloque));
                        if ($scope.paginacionFinanzas.em.registros % $scope.paginacionFinanzas.em.bloque > 0) {
                            $scope.paginacionFinanzas.em.numPag++;
                        }
                        $scope.datagridextractos.option("dataSource", respuesta.data.contenido);
                        quitarCapaCargando();
                    })
            }
        },
        ec: {
            pagina: 1,
            bloque: 20,
            llamada: function () {
                Llamada.get("FinanzasEfectosCursoLeer?idDeudor=" + b + "&bloque=" + this.bloque + "&pagina=" + this.pagina)
                    .then(function (respuesta) {
                        $scope.paginacionFinanzas.ec.registros = respuesta.data.registros;
                        $scope.paginacionFinanzas.ec.actualPag = JSON.parse("" + JSON.stringify($scope.paginacionFinanzas.ec.pagina));
                        $scope.paginacionFinanzas.ec.numPag = parseInt(+($scope.paginacionFinanzas.ec.registros / $scope.paginacionFinanzas.ec.bloque));
                        if ($scope.paginacionFinanzas.ec.registros % $scope.paginacionFinanzas.ec.bloque > 0) {
                            $scope.paginacionFinanzas.ec.numPag++;
                        }
                        $scope.datagridefectos.option("dataSource", respuesta.data.contenido);
                        quitarCapaCargando();
                    })
            }
        }
    }
    leerCosas = function (where) {
        switch (where) {
            case 'pc':
                $scope.paginacionFinanzas.pc.llamada();
                break;
            case 'em':
                $scope.paginacionFinanzas.em.llamada();
                break;
            case 'ec':
                $scope.paginacionFinanzas.ec.llamada();
                break;
        }
    }
    var inputPaginaPromise;
    $scope.cambioPagina = function (where) {
        if (inputPaginaPromise) {
            $timeout.cancel(inputPaginaPromise);
        }
        inputPaginaPromise = $timeout(leerCosas(where), 1000);
    };
    $scope.paginaAnterior = function (where) {
        switch (where) {
            case 'pc':
                if ($scope.paginacionFinanzas.pc.pagina > 1) {
                    $scope.paginacionFinanzas.pc.pagina--;
                    $scope.paginacionFinanzas.pc.llamada();
                }
                
                break;
            case 'em':
                if ($scope.paginacionFinanzas.em.pagina > 1) {
                    $scope.paginacionFinanzas.em.pagina--;
                    $scope.paginacionFinanzas.em.llamada();
                }
                break;
            case 'ec':
                if ($scope.paginacionFinanzas.ec.pagina > 1) {
                    $scope.paginacionFinanzas.ec.pagina--;
                    $scope.paginacionFinanzas.ec.llamada();
                }
                break;
        }
    };
    $scope.paginaSiguiente = function (where) {
        switch (where) {
            case 'pc':
                if ($scope.paginacionFinanzas.pc.pagina < $scope.paginacionFinanzas.pc.numPag) {
                    $scope.paginacionFinanzas.pc.pagina++;
                    $scope.paginacionFinanzas.pc.llamada();
                }
                break;
            case 'em':
                if ($scope.paginacionFinanzas.em.pagina < $scope.paginacionFinanzas.em.numPag) {
                    $scope.paginacionFinanzas.em.pagina++;
                    $scope.paginacionFinanzas.em.llamada();
                }
                break;
            case 'ec':
                if ($scope.paginacionFinanzas.ec.pagina < $scope.paginacionFinanzas.ec.numPag) {
                    $scope.paginacionFinanzas.ec.pagina++;
                    $scope.paginacionFinanzas.ec.llamada();
                }
                break;
            default:
                alert("Palo");
                break;
        }
        
    };
    


    $scope.dataGridEfectos = {
        dataSource: [],
        keyExpr: "idEfectoCobro",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        noDataText: "No hay datos para mostrar",
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "descripcionTipoEfecto",
                caption: "Factura",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "numeroDocumento",
                caption: "Nº Documento",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "documentoOrigen",
                caption: "VtosCobrados",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "fechaRecepcion",
                caption: "Fecha Recep.",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "fechaVto",
                caption: "Fecha Vto.",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "fechaCobro",
                caption: "Fecha Cobro",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "importe",
                caption: "Importe",
                precision: 2,
                width: "30%",
                cellTemplate: "currencyTemplate",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "estado",
                caption: "Estado",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
                cellTemplate:"estadoTemplate"
            }, 
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagridefectos = e.component;
            leerCosas('ec');
        }
    };
    $scope.dataGridExtractos = {
        dataSource: [],
        keyExpr: "idOrigen",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        noDataText: "No hay datos para mostrar",
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "fecha",
                caption: "Fecha",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "comentario",
                caption: "Concepto",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "importeCargo",
                caption: "Cargo",
                width: "30%",
                precision: 2,
                cellTemplate: "currencyTemplate",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "importeAbono",
                caption: "Abono",
                width: "30%",
                precision: 2,
                cellTemplate: "currencyTemplate",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            },
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagridextractos = e.component;
            leerCosas('em');
        }
    };
    $scope.dataGridPendientes = {
        dataSource: [],
        keyExpr: "idDebito",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        noDataText: "No hay datos para mostrar",
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "origen",
                caption: "Factura",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "numeroDocumentoOrigen",
                caption: "Nº Documento",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            },  {
                dataField: "fechaDocumento",
                caption: "Fecha Doc.",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "fechaVtoPrevista",
                caption: "Fecha Vto. Prevista",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "importeCobrado",
                caption: "Importe Vto.",
                width: "30%",
                cellTemplate: "currencyTemplate",
                precision: 2,
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "importePendiente",
                caption: "Importe Pendiente",
                width: "30%",
                cellTemplate: "currencyTemplate",
                precision: 2,
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "importeRiesgo",
                caption: "Importe Riesgo",
                cellTemplate: "currencyTemplate",
                precision: 2,
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
            }, {
                dataField: "estado",
                caption: "Estado",
                width: "30%",
                dataType: "date",
                format: 'dd/MM/yyyy',
                format: 'dd/MM/yyyy',
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "estadoTemplate"
            },
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagridpendientes = e.component;
            leerCosas('pc');
        }
    };
    document.getElementById("ocultocargando").style.display = "inline";
});