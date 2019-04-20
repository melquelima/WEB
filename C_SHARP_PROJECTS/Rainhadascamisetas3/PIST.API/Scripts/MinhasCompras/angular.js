$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


//window.app = angular.module('app', ['ui.bootstrap', 'ngResource']);
app.controller('mainCtrl', ['$scope', function ($scope) {
    //$scope.a = httpGet("/api/Devedores/get");
   // console.log(a);
    $scope.clientes = JSON.parse(httpGet("/api/Devedores/get"));
    $scope.pagamentos = JSON.parse(httpGet("/api/pagos/get"));
    $scope.clientes_filtro = [];
    $scope.totalItems = $scope.clientes_filtro.length;
    $scope.currentPage = 1;
    $scope.numPerPage = 10;
    $scope.clientes_filtro = $scope.clientes;
    $scope.totais = [];
    $scope.listapagos = [];
    $scope.baixa = [];
    $scope.today = new Date().toISOString().slice(0, 19).replace('T', ' ').split(' ')[0];
    $scope.today_ant = '0001-01-01';
    $scope.currOs = null;

    $scope.modalConfirm = function (obj) {
        $scope.currOs = obj.OS;
        $("#ModalConfirm").modal();
    }

    $scope.editar = function () {
        window.location.replace("/home/Editar?os=" + $scope.currOs);
    }

    $scope.BotaoDeletaPagos = function () {
        for (i = 0; i < $scope.listapagos.length; i++) {
            if ($scope.listapagos[i].cheked){
                return true;
            }
        }
        return false;
    }
    $scope.modal2 = function () {
        //alert();
        $("#ModalEdit").modal('hide')
        $("#ModalConfirm2").modal()
    }

    $scope.DeletarNota = function () {
        var os = $scope.curruser
        x = new XMLHttpRequest();
        x.open("POST", "/api/pagos/Delete", true);
        x.setRequestHeader("Content-type", "application/json");
        x.onreadystatechange = function () {
            //Your code here to handle readyState==4 and readyState==3
            if (x.readyState == 4 && x.status == 200) {
                y = new XMLHttpRequest();
                y.open("GET", "/Home/DeletarPedido?OS=" + os.OS, true);
                y.setRequestHeader("Content-type", "application/json");
                y.onreadystatechange = function () {
                    //Your code here to handle readyState==4 and readyState==3
                    if (y.readyState == 4 && y.status == 200) {
                        $scope.curruser = os;
                        //alert($scope.curruser.OS);
                        $scope.atualiza();
                        $("#ModalConfirm3").modal('hide')
                    }
                }
                y.send();
            }
        }
        x.send(JSON.stringify($scope.listapagos));
    }

    $scope.modal3 = function () {
        //alert();
        $("#ModalEdit").modal('hide')
        $("#ModalConfirm3").modal()
    }

    $scope.DeletarPagamento = function () {
        var deletar = [];
        for (i = 0; i < $scope.listapagos.length; i++) {
            if ($scope.listapagos[i].cheked) {
                deletar.push($scope.listapagos[i]);
            }
        }
        x = new XMLHttpRequest();
        x.open("POST", "/api/pagos/Delete", true);
        x.setRequestHeader("Content-type", "application/json");
        x.onreadystatechange = function () {
            //Your code here to handle readyState==4 and readyState==3
            if (x.readyState == 4 && x.status == 200) {
                $scope.alertSucess = 'Deletado com sucesso!';
                //$scope.clientes = JSON.parse(httpGet("/api/Devedores/get"));
                //$scope.pagamentos = JSON.parse(httpGet("/api/pagos/get"));

                $scope.atualiza();
                

                $scope.baixa = [];
                $scope.alertSucess = '';
                $scope.errVlr = '';
                $("#ModalConfirm2").modal('hide')
                $("#ModalEdit").modal()
                $scope.$apply();
                

               
                return;
            }
            else if (x.readyState == 4 && !(x.status == 200)) {
                console.log("====NOK====");
                $scope.alertSucess = '';
                $scope.errVlr = x.statusText;
                $scope.$apply();
                console.log(x.statusText);
            }
        }
        x.send(JSON.stringify(deletar));
        console.log(deletar)
    }

    $scope.atualiza = function () {
        //console.log("asdasd");
        //alert($scope.curruser.OS);
        y = new XMLHttpRequest();
        y.open("GET", "/api/pagos/get", true);
        y.setRequestHeader("Content-type", "application/json");
        y.onreadystatechange = function () {
            //Your code here to handle readyState==4 and readyState==3
            if (y.readyState == 4 && y.status == 200) {
                //console.log("kkkkkkkkkkkkkkkkkkkkkkkkk");
                //console.log(y);
                $scope.pagamentos = JSON.parse(y.response);
                $scope.$apply();
              
            }
            //console.log(y.readyState);
        }
        y.send();

        var Oss = $scope.curruser.OS;
        z = new XMLHttpRequest();
        z.open("GET", "/api/Devedores/get", true);
        z.setRequestHeader("Content-type", "application/json");
        z.onreadystatechange = function () {
            if (z.readyState == 4 && z.status == 200) {
                console.log("zzzzzzzzzzzzzzzz");
                $scope.clientes = JSON.parse(z.response);

                $scope.curruser = $scope.clientes.filter(function (x) { if (x.OS == $scope.curruser.OS) { return x; } })[0];
                $scope.listapagos = $scope.pagamentos.filter(
                    function (x) {
                        if (x.OS == Oss) {return x; }
                    }
                );
                $scope.$apply();
                $scope.filtros();
            }
        }
        z.send();
    }

    $scope.efetuarBaixas = function () {
        console.log($scope.baixa);
        $scope.errVlr = '';
        httpGet("/api/os/update?Os=" + $scope.curruser.OS + "&entrada=" + $scope.curruser.STATUS + '&data=' + $scope.today);
        if ($scope.baixa.length > 0) {
            x = new XMLHttpRequest();
            x.open("POST", "/api/pagos/save", true);
            x.setRequestHeader("Content-type", "application/json");
            x.onreadystatechange = function () {
                //Your code here to handle readyState==4 and readyState==3
                if (x.readyState == 4 && x.status == 200) {
                    $scope.alertSucess = 'Salvo com sucesso!';
                    $scope.clientes = JSON.parse(httpGet("/api/Devedores/get"));
                    $scope.pagamentos = JSON.parse(httpGet("/api/pagos/get"));
                    console.log("====OK====");
                    console.log(x.responseText);
                    $scope.$apply();
                    return;
                }
                else if (x.readyState == 4 && !(x.status == 200)) {
                    console.log("====NOK====");
                    $scope.alertSucess = '';
                    $scope.errVlr = x.statusText;
                    $scope.$apply();
                    console.log(x.statusText);
                }
            }
            x.send(JSON.stringify($scope.baixa));
        }
        $scope.alertSucess = 'Salvo com sucesso!';
    }


    $scope.adicionarListaBaixa = function () {

        if (typeof $scope.pg_valor == 'undefined' || $scope.pg_valor == null || $scope.pg_valor <= 0) {
            $scope.errVlr = ' Insira um valor válido!';
            return;
        }

        if (typeof $scope.pg_categoria == 'undefined' || $scope.pg_categoria == null) {
            $scope.errVlr = ' Selecione uma categoria!';
            return;
        }

        if ((parseFloat($scope.curruser.VALOR).toFixed(2)) < parseFloat($scope.tPago()).toFixed(2) + parseFloat($scope.pg_valor)){
            $scope.errVlr = 'Valor da nota Exedido!' + parseFloat($scope.curruser.VALOR) + ' ' + parseFloat($scope.tPago()) + parseFloat($scope.pg_valor);
            return;
        }
        var mascara = {
            ID:null,
            DATA: new Date().toISOString().slice(0, 19).replace('T', ' ').split(' ')[0],
            OS: $scope.curruser.OS,
            CPF: $scope.curruser.CLIENTE_CPF,
            CATEGORIA: $scope.pg_categoria,
            OBS: $scope.pg_obs,
            VALOR: parseFloat($scope.pg_valor),
            NOTA: null
        }

        $scope.baixa.push(mascara);
        $scope.listapagos.push(mascara);
    }

    $scope.tPago = function () {
        total = 0;
        for (i = 0; i < $scope.listapagos.length; i++) {
            total +=  parseFloat($scope.listapagos[i].VALOR);
            //console.log(i + '- ' + $scope.listapagos[i].VALOR);
        }
        return total;
    }

    $scope.$watch('cpf', function () {
        
        $scope.filtros();
    });

    $scope.filtros = function () {
        $scope.clientes_filtro = $scope.clientes.filter(
            function (x) {
                if (($scope.filtroStatus == null || $scope.filtroStatus == "" || typeof $scope.filtroStatus =="undefined" ? true : ($scope.filtroStatus == 1 ? x.RESTA == 0 : x.RESTA > 0))) {
                    return x;
                }
            });
        
        if (!($scope.cpf == null || $scope.cpf == 'ADM' || $scope.cpf == '12345678912' || typeof $scope.cpf == "undefined")) {
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
        //console.log($scope.filtro);
        //clientes_filtro = (clientes | filter:filtro); totalItems = clientes_filtro.length; totais_();
       // console.log("ENTROU");
    }
    $scope.totais_ = function () {
        total1 = 0;
        total2 = 0;
        total3 = 0;

        for (var i = 0; i < $scope.totalItems; i++) {
            total1 += parseFloat($scope.clientes_filtro[i].VALOR);
            total2 += parseFloat($scope.clientes_filtro[i].PAGO);
            total3 += parseFloat($scope.clientes_filtro[i].RESTA);
        }
        $scope.totais[0] = total1;
        $scope.totais[1] = total2;
        $scope.totais[2] = total3;
    }
    //$scope.filtros();
    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.clientes_filtro.indexOf(value);
        return (begin <= index && index < end);
    };

    $scope.currentuser = function (obj) {
        $scope.curruser = obj.OS;
        $("#ModalNota").modal();
    }
    $scope.currentuser2 = function (obj) {
        $scope.curruser = obj;//$scope.clientes_filtro.filter(function (x) { if (x.OS == obj) { return x; } });
        //console.log($scope.curruser);
        $scope.listapagos = $scope.pagamentos.filter(
            function (x) {
                if (x.OS == obj.OS) { return x;}
            }
        );
        $scope.baixa = [];
        $scope.alertSucess = '';
        $scope.errVlr = '';
        $("#ModalEdit").modal();
    }
    $scope.inicial = function () {
       
    }

}]);

app.filter('status', function () { //--------------------------- retira duplicatas no campo

    var i = 0;

    var x = function (original, filtro) {
        var valor = 0;
        var itens2 = [];
        var itens = [];

        for (i = 0; i < original.length; i++) {
            if ((filtro == 0 || filtro == null || typeof filtro == "undefined" ? true : (filtro == 1 ? (original[i].RESTA == 0) : (original[i].RESTA > 0)))) {
                itens.push(original[i]);
            }
        }
        return itens;
    };
    return x;
});

app.filter('status2', function () { //--------------------------- retira duplicatas no campo

    var i = 0;

    var x = function (original, filtro) {
        var valor = 0;
        var itens2 = [];
        var itens = [];
        
        for (i = 0; i < original.length; i++) {
            if ((filtro == 0 || filtro == null || typeof filtro == "undefined" ? true : original[i].STATUS == filtro)) {
                itens.push(original[i]);
            }
        }
        return itens;
    };
    return x;
});

app.filter('filterorc', function () { 

    var i = 0;

    var x = function (original, filtro) {
        var valor = 0;
        var itens2 = [];
        var itens = [];

        for (i = 0; i < original.length; i++) {
            if ((filtro == 0 || filtro == null || typeof filtro == "undefined" ? true : (filtro ? true : original[i].CLIENTE != "ORÇAMENTO"))) {
                itens.push(original[i]);
            }
        }
        console.log("====x====");
        console.log(itens);
        return itens;
    };
   
    return x;
});



app.filter('combodef', function () { //--------------------------- coloca valor default em combobox

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

        //console.log(valor);
        return itens2;
    };
    return x;
});

app.filter('unique', function () { //--------------------------- retira duplicatas no campo

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
    //console.log(x);
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

function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}