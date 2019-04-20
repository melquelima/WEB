window.onload = function (e) {


}

//var app = angular.module('cores', []);

app.controller('coresCtrl', function ($scope, filterFilter) {
    $scope.data_field_angular;
    $scope.itens = [{}]; //JSON.parse(httpGet("/api/values/"))
    $scope.itens_filtrados;
    $scope.mostra = false;

    $scope.itens = "";
    $scope.mostra = false;
    
    $scope.itens = JSON.parse(httpGet("/api/cor/get")).sort();
   
    $scope.mostra = true;

    $scope.adcionarCor = function () {
        $('#ModalCores').modal('hide');

        if (typeof $scope.addCor == 'undefined') {
            $scope.error = 'Insira uma cor válida';
            $('#ModalError').modal();
        } else
         if ($scope.addCor.length == 0) {
            $scope.error = 'Insira uma cor válida';
            $('#ModalError').modal();
         } else {
             var a = $scope.itens.filter(function (x) { if (x.COR.trim().toUpperCase() == $scope.addCor.trim().toUpperCase()) { return x;}})
             if (a.length > 0) {
                 $scope.error = 'Esta Cor já existe!';
                 $('#ModalError').modal();
             } else {
                 httpGet("/api/cor/save?cor=" + $scope.addCor.trim().toUpperCase());
                 $scope.itens = JSON.parse(httpGet("/api/cor/get")).sort();
                 $scope.OK = "Cor Adcionada com sucesso!";
                 $('#ModalOk').modal();
             }
        }
       
    }

    $scope.DeletarCor = function () {
        httpGet("/api/cor/Delete?cor=" + $scope.currItem.COR);
        $scope.itens = JSON.parse(httpGet("/api/cor/get")).sort();
        $scope.OK = "Item Deletado com sucesso!";
        $('#ModalDelete').modal('hide');
        $('#ModalOk').modal();

    }
    $scope.delete = function (obj) {
        $scope.currItem = obj;
        $('#ModalDelete').modal();
    }
    //$scope.filtroCombos();
});


//angular.element(function () {
//    var a = $scope.itens.filter(function (x) {
//        if (x.MALHA == 'ALGODÃO') {
//            return x;
//        }
//    });

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

app.directive('elemReady', function ($parse) {
    return {
        restrict: 'A',
        link: function ($scope, elem, attrs) {
            elem.ready(function () {
                $scope.$apply(function () {
                    var func = $parse(attrs.elemReady);
                    func($scope);
                })
            })
        }
    }
})

app.filter('combodef', function () { //--------------------------- coloca valor default em combobox

    var x = function (original, campo, valor) {
        var itens2 = [];
        var objs = Object.getOwnPropertyNames(original[0]).sort()
        var strr = '{';
        for (i = 1; i < objs.length; i++) {
            strr = strr.concat('"', objs[i], '":"', original[0][objs[i]],'",');
        }
        strr = strr.substring(0, strr.length - 1).concat('}');
        strr = JSON.parse(strr);
        strr[campo] = valor;
        itens2.push(strr);
        Array.prototype.push.apply(itens2, original);

        console.log(itens2);
        return itens2;
    };
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