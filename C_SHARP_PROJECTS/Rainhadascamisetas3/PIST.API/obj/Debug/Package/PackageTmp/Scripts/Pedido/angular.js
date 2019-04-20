//var app = angular.module('precos', []);

app.controller('precosCtrl', ['$scope', '$http', '$filter','$timeout', function ($scope,$http, $filter,$timeout) {


   


    $scope.lista = httpGet("/api/precos/detalhado")
    $scope.lista = JSON.parse($scope.lista);
    //$scope.carrinho = [{ "ID": null, "OS": null, "DATA": null, "CPF": "03579496921", "MALHA": "PV", "CAT_PRODUTO": "BODYS", "PRODUTO": "BODY BB HL M/M", "COR": "AMARELO OURO", "CAT_TAMANHO": "ADULTO", "TAMANHO": "M", "VALOR": 11, "QUANTIDADE": 1, "STATUS": "PROCESSANDO", "DATA_ENTREGA": null, "ORCAMENTO": 0, "$$hashKey": "object:563" }];
    $scope.carrinho = [];
    



    $scope.labelValor = 0;
    $scope.labelQtd = 0;
    $scope.labelTotal = 0;
    $scope.cores = JSON.parse(httpGet("/api/cor/get"));
    $scope.statushttp = "";
    $scope.cliente_ = null;
    $scope.alterarFlag = -1;
    $scope.clientes = JSON.parse(httpGet("/api/usuario/get"));
    $scope.espelho;
    //console.log($scope.cores)
    

    $scope.editarPedido = function () {
        alert();
    }

    $scope.$watch('editar_os', function () {
        $scope.carrinho = ($scope.editar_os ? JSON.parse(httpGet("/api/os/GETPEDIDO?os=" + $scope.editar_os)) : []);
        $scope.espelho = ($scope.editar_os ? $scope.carrinho[0]:'');
        $scope.cliente_ = ($scope.editar_os ? $scope.espelho.CPF : $scope.cliente_);
        //alert($scope.editar_os);
    });

    

    

    $scope.editar = function () {
        if ($scope.alterarFlag > -1) {
            console.log($scope.alterarFlag);
            var item = $scope.BarCode();
            var val = ($scope.cor == "BRANCO" ? item[0].BRANCO : item[0].COLORIDO);
            $scope.carrinho[$scope.alterarFlag].MALHA = item[0].MALHA;
            $scope.carrinho[$scope.alterarFlag].CATEGORIA_PROD = item[0].CAT_PRODUTO;
            $scope.carrinho[$scope.alterarFlag].PRODUTO = item[0].ITEM;
            $scope.carrinho[$scope.alterarFlag].COR = $scope.cor;
            $scope.carrinho[$scope.alterarFlag].CATEGORIA_TAM = item[0].CATEGORIA_TAM;
            $scope.carrinho[$scope.alterarFlag].TAMANHO = item[0].TAMANHO;
            $scope.carrinho[$scope.alterarFlag].VALOR = val;
            $scope.carrinho[$scope.alterarFlag].QUANTIDADE = $scope.Quantidade;
            $scope.limpar();
            $scope.alert('Item Alterado com sucesso!',1);
            //$timeout(callAtTimeout, 3000);

            //$timeout(function () {$scope.success = '';}, 2000);
        }
    }

    $scope.currItem_ = function (x) {
        $scope.catProd = x.CATEGORIA_PROD;
        $scope.Tam = x.TAMANHO;
        $scope.malha = x.MALHA;
        $scope.produto = x.PRODUTO;
        $scope.cor = x.COR;
        $scope.Quantidade = x.QUANTIDADE;
        $scope.catProd = x.CAT_PRODUTO;
        $scope.error = '';
        $scope.success = '';
        $scope.cliente_ = x.CPF;
        $scope.alterarFlag = procurax(x.PRODUTO, "PRODUTO", x.TAMANHO, "TAMANHO", $scope.carrinho);
        console.log($scope.alterarFlag);
        $("#myModal").modal();
    }

    $scope.limpaLista = function () {
        $scope.carrinho.splice(0, $scope.carrinho.length);
        console.log(JSON.stringify($scope.carrinho));
    }
    
    $scope.removeItem = function (index) {
        $scope.carrinho.splice(index, 1);
    }

    $scope.total = function () {
        var t = 0;
        for (i = 0; i < $scope.carrinho.length; i++) {
            t = t + $scope.carrinho[i].QUANTIDADE * $scope.carrinho[i].VALOR;
        }
        return t;
    }

    $scope.funcCodBar = function () {
        if (typeof $scope.codBarr == 'undefined' || $scope.codBarr == '' || $scope.codBarr == null || ($scope.codBarr + '').length < 10 || ($scope.codBarr + '').length > 10) {
            var item = $scope.BarCode();
            $scope.codBarr = item[0].COD_UNICO;
        }
    }

    $scope.BarCode = function(){
        return $scope.lista.filter(
            function (x) {
                if (x.MALHA == $scope.malha && x.CATEGORIA_PROD == $scope.catProd && x.ITEM == $scope.produto && x.TAMANHO == $scope.Tam) {
                    return x;
                }

            });
    }

    $scope.atualizaCliente = function () {
        console.log("---------------------------------");
        console.log($scope.cliente_);
        for (i = 0; i < $scope.carrinho.length; i++) {
            $scope.carrinho[i].CPF = $scope.cliente_;
        }
        console.log($scope.carrinho);
        console.log(JSON.stringify($scope.carrinho));
        console.log("==================================");
    }

    $scope.keyupevent = function (e, type) {
        $scope.success = '';
        switch (type) {
            case 'codBarras':
                if (event.keyCode == 13) {
                    if (typeof $scope.codBarr == 'undefined' || $scope.codBarr == '' || $scope.codBarr == null || ($scope.codBarr + '').length < 10 || ($scope.codBarr + '').length > 10) {
                        $scope.alert(' Código de barras inválido',0);
                        //console.log($scope.codBarr + '');
                    } else {
                        var item = $scope.lista.filter(function (x) {
                            if (x.COD_UNICO == $scope.codBarr) {
                                return x;
                            }
                        });

                        console.log(item);

                        if (item.length > 0) {
                            $scope.catProd = item[0].CATEGORIA_PROD;
                            $scope.Tam = item[0].TAMANHO;
                            $scope.malha = item[0].MALHA;
                            $scope.produto = item[0].ITEM;
                            $scope.cor = 'BRANCO';
                            $scope.error = '';
                            //$scope.focusCor = true;
                            $scope.focusQtd = true;
                            //console.log($scope.catProd);
                        } else {
                            $scope.alert(' Produto não cadastrado!',0);
                        }
                    }
                }
                break;
            //case 'cor':
            //    if (event.keyCode == 13) {
            //        if (typeof $scope.cor == 'undefined' || $scope.cor == '') {
            //            $scope.error = ' Insira uma cor válida!';
            //        } else {
            //            $scope.error = '';
            //            $scope.focusQtd = true;
            //        }
            //    }
            //    break;
            case 'qtd':
                $scope.error = '';
                if (event.keyCode == 13) {
                    $scope.qtdEnter();
                }
                break;
        }
    }

    $scope.qtdEnter = function () {

        if (eVazio($scope.malha)) {
            $scope.alert(' Selecione uma Malha!',0);
            return;
        }

        if (eVazio($scope.catProd)) {
            $scope.alert(' Selecione a categoria do seu produto!', 0);
            return;
        }

        if (eVazio($scope.produto)) {
            $scope.alert(' Selecione o produto!', 0);
            return;
        }

        if (eVazio($scope.Tam)) {
            $scope.alert(' Selecione o tamanho!', 0);
            return;
        }

        if (eVazio($scope.Quantidade)) {
            $scope.alert(' Insira uma quantidade válida!', 0);
            return;
        }

        if (eVazio($scope.cor)) {
            $scope.alert(' Insira uma Cor válida!', 0);
            return;
        }
        if (eVazio($scope.cliente_) || $scope.cliente_ == 'ADM') {
            $scope.alert(' Insira um cliente válido!', 0);
            return;
        }

        var index = existeItem($scope.malha, $scope.produto, $scope.cor, $scope.Tam, $scope.carrinho);
        if (index > -1) {
            $scope.carrinho[index].QUANTIDADE += $scope.Quantidade;
        } else {
            $scope.funcCodBar();
            var item = $scope.lista.filter(function (x) { if (x.COD_UNICO == $scope.codBarr) { return x; } });
            var val = ($scope.cor == "BRANCO" ? item[0].BRANCO : item[0].COLORIDO);

            if (val == 0) {
                $scope.alert(' Cor indisponível',0);
                return;
            } else {
                var mascara = {
                    ID: null,
                    OS: ($scope.editar_os ? $scope.espelho.OS : null),
                    DATA: ($scope.editar_os ? $scope.espelho.DATA : null),//new Date().toISOString().slice(0, 19).replace('T', ' ').split(' ')[0],
                    CPF: $scope.cliente_,
                    MALHA: item[0].MALHA,
                    CAT_PRODUTO: item[0].CATEGORIA_PROD,
                    PRODUTO: item[0].ITEM,
                    COR: $scope.cor,
                    CAT_TAMANHO: item[0].CATEGORIA_TAM,
                    TAMANHO: item[0].TAMANHO,
                    VALOR: val,
                    QUANTIDADE: parseInt($scope.Quantidade),
                    STATUS: 'PROCESSANDO',
                    DATA_ENTREGA: null,
                    ORCAMENTO: 0
                };
                $scope.carrinho.push(mascara);
            }
        }

        $scope.alert('Produto adcionado com sucesso!',1);

        if (!$scope.check_manter_dados) {
            $scope.codBarr = "";
            $scope.catProd = "";
            $scope.Tam = "";
            $scope.malha = "";
            $scope.produto = "";
            $scope.cor = "";
            $scope.Quantidade = 1;
        }

        if ($scope.carrinho.length == 23) {
            $("#myModal").modal('hide');
            return;
        }

        if ($scope.check_codBarras) {
            $scope.focusBarCode = true;
        } else {
            $scope.focusMalha = true;
        }
    }

    $scope.novoItem = function () {
        $scope.limpar();
        if ($scope.carrinho.length == 23) {
            $("#ModalError").modal();
            $scope.statushttp = "São permitidos apenas 23 itens por nota!";
        } else {
            $("#myModal").modal();
        }
    }

    $scope.limpar = function () {
        $scope.catProd = null;
        //$scope.cliente_ = null;
        $scope.Tam = null;
        $scope.malha = null;
        $scope.produto = null;
        $scope.cor = null;
        $scope.Quantidade = 1;
        $scope.catProd = null;
        $scope.error = '';
        $scope.success = '';
        $scope.alterarFlag = -1;
    }

    $scope.fazerPedido = function () {

        if ($scope.carrinho.length == 0) {
            $("#ModalPedido").modal('hide');
            $("#ModalNota").modal('hide');
            $("#ModalError").modal();
            $scope.statushttp = "Sua lista de Pedidos está vazia! insira pelomenos 1 item a sua lista.";
            return;
        }

        console.log($scope.carrinho);
        document.getElementById("btnPedido").disabled = true;
        $scope.validando = true;
        

        x = new XMLHttpRequest();
        x.open("POST", ($scope.editar_os ? "/api/os/Editar":"/api/os/SalvaNota"), true);
        x.setRequestHeader("Content-type", "application/json");
        

        x.onreadystatechange = function () {
            //Your code here to handle readyState==4 and readyState==3
            if (x.readyState == 4 && x.status == 200) {
                console.log("====OK====");
                console.log(x.responseText);
                $scope.statushttp = x.responseText.replace("\"", "").replace("\"", "");
                $scope.limpaLista();
                $scope.$apply();
                $("#ModalPedido").modal('hide');
                $("#ModalNota").modal();
                document.getElementById("btnPedido").disabled = false;
                $scope.validando = false;
                //console.log($scope.statushttp);
            }
            else if (x.readyState == 4 && !(x.status == 200)) {
                console.log("====NOK====");
                console.log(x.statusText);
                $("#ModalPedido").modal('hide');
                $("#ModalNota").modal('hide');
                $("#ModalError").modal();
                $scope.statushttp = x.statusText;
                document.getElementById("btnPedido").disabled = false;
                $scope.validando = false;
                $scope.$apply();
            }
        }
        x.send(JSON.stringify($scope.carrinho));
    }

    $scope.alert = function(msg, tipo){
        if (tipo == 1) {
            $scope.success = msg;
        } else {
            $scope.error = msg;
        }
        $timeout(function () {
            $scope.success = "";
            $scope.error = "";
        }, 3000);
    }
}]);

app.directive('focusMe', function ($timeout) {
    return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.focusMe, function (value) {
                if (value === true) {
                    console.log('value=', value);
                    //$timeout(function() {
                    element[0].focus();
                    scope[attrs.focusMe] = false;
                    //});
                }
            });
        }
    };
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

function procura(valor, campo, obj) {
    for (var j = 0; j < obj.length; j++) {
        if (obj[j][campo] == valor) {
            return j;
        }
    }
    return -1;
}

function procurax(valor, campo,valor2,campo2, obj) {
    for (var j = 0; j < obj.length; j++) {
        if (obj[j][campo] == valor && obj[j][campo2] == valor2) {
            return j;
        }
    }
    return -1;
}

function eVazio(value) {
    if (typeof value == 'undefined' || value == null || value == '') {
        return true;
    }
    return false;
}
function existeItem(malha,produto,cor, tamanho,obj) {
    for (var j = 0; j < obj.length; j++) {
        if (obj[j].MALHA == malha && obj[j].PRODUTO == produto && obj[j].COR == cor && obj[j].TAMANHO == tamanho) {
            return j;
        }
    }
    return -1;
}

function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}



//document.getElementById("tabela").visible = false; 
$(document).ready(function () {
    //$("#ModalError").modal();
    //document.getElementById("tabela").visible = true; 
});

