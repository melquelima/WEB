$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


window.App = angular.module('App', ['ui.bootstrap', 'ngResource']);
App.controller('mainCtrl', ['$scope', function ($scope) {

    $scope.clientes = JSON.parse(httpGet("/api/Devedores/get"));
    $scope.pagamentos = JSON.parse(httpGet("/api/pagos/get"));
    $scope.clientes_filtro = [];
    $scope.totalItems = $scope.clientes_filtro.length;
    $scope.currentPage = 1;
    $scope.numPerPage = 10;
    $scope.clientes_filtro = $scope.clientes;
    $scope.totais = [];
    $scope.totais_c = [];
    $scope.listapagos = [];
    $scope.baixa = [];
    $scope.today = new Date().toISOString().slice(0, 19).replace('T', ' ').split(' ')[0];
    $scope.today_ant = '0001-01-01';


   
   
    $scope.totaisc_ = function () {
        var por_cliente = []
        for (var i = 0; i < $scope.clientes.length; i++) {
            if (procura($scope.clientes[i].CLIENTE, 'CLIENTE', por_cliente)> -1){
                var index = procura($scope.clientes[i].CLIENTE, 'CLIENTE', por_cliente)
                por_cliente[index].VALOR += $scope.clientes[i].VALOR; 
                por_cliente[index].PAGO += $scope.clientes[i].PAGO; 
                por_cliente[index].RESTA += $scope.clientes[i].RESTA; 
            } else {
                por_cliente.push($scope.clientes[i]);
            }
        }
        $scope.totais_c = por_cliente.filter(function (x) { if(x.RESTA > 0) return x }).sort();
        console.log($scope.totais_c);
        $scope.totalItems = $scope.totais_c.length;
    }

    $scope.filtros = function () {
        $scope.clientes_filtro = $scope.clientes.filter(
            function (x) {
                if (($scope.filtroStatus == null || $scope.filtroStatus == "" || typeof $scope.filtroStatus =="undefined" ? true : ($scope.filtroStatus == 1 ? x.RESTA == 0 : x.RESTA > 0))) {
                    return x;
                }
            });
        if (!($scope.cpf == null || $scope.cpf == 'ADM' || typeof $scope.cpf == "undefined")) {
            $scope.clientes_filtro = $scope.clientes.filter(
                function (x) {
                    if (x.CLIENTE_CPF == $scope.cpf) {
                        return x;
                    } else {
                        console.log($scope.cpf);
                    }
                });
        }

        $scope.totalItems = $scope.clientes_filtro.length;
        $scope.totais_();
        console.log($scope.filtro);
        //clientes_filtro = (clientes | filter:filtro); totalItems = clientes_filtro.length; totais_();
        console.log("ENTROU");
    }
    $scope.totais_ = function () {
        total1 = 0;
        total2 = 0;
        total3 = 0;
        for (var i = 0; i < $scope.totalItems; i++) {
            total1 += parseFloat($scope.totais_c[i].VALOR);
            total2 += parseFloat($scope.totais_c[i].PAGO);
            total3 += parseFloat($scope.totais_c[i].RESTA);
        }
        $scope.totais[0] = total1;
        $scope.totais[1] = total2;
        $scope.totais[2] = total3;
    }
   
    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.totais_c.indexOf(value);
        return (begin <= index && index < end);
    };
    
    $scope.totaisc_();
    $scope.totais_();
}]);


function procura(valor,campo, obj) {
    for (var j = 0; j < obj.length; j++) {
        if (obj[j][campo] == valor){
            return j;
        }
    }
    return -1;
}

App.filter('combodef', function () { //--------------------------- coloca valor default em combobox

    var x = function (original, campo, valor) {
        var itens2 = [];
        var objs = Object.getOwnPropertyNames(original[0]).sort()
        var strr = '{';
        for (i = 1; i < objs.length; i++) {
            strr = strr.concat('"', objs[i], '":"",');
            //console.log(original[0][objs[i]]);
        }
        strr = strr.substring(0, strr.length - 1).concat('}');
        strr = JSON.parse(strr);
        strr[campo] = valor;
        itens2.push(strr);
        Array.prototype.push.apply(itens2, original);

        console.log(valor);
        return itens2;
    };
    return x;
});

App.filter('unique', function () { //--------------------------- retira duplicatas no campo

    var i = 0;

    var x = function (original, filtro) {
        var valor = 0;
        var itens2 = [];
        var itens = [];

        for (i = 0; i < original.length; i++) {
            if (!contains(itens2, original[i][filtro])) {
                itens2.push(original[i][filtro]);
                itens.push(original[i]);
            }
        }
        return itens;
    };
    console.log(x);
    return x;
});

function contains(a, obj) {
    var i = a.length;

    while (i--) {
        if (a[i] == obj) {
            return true;

        }

    }
    return false;
}

function contains2(a, obj) {
    var i = a.length;

    while (i--) {
        if (a[i] === obj) {
            return i;

        }

    }
    return false;
}

function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}