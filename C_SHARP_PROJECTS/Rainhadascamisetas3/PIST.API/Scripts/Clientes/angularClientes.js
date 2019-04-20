$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


//window.App = angular.module('App', ['ui.bootstrap', 'ngResource']);
app.controller('mainCtrl', ['$scope', function ($scope) {

    $scope.clientes = JSON.parse(httpGet("/api/usuario/get"));
    $scope.clientes_filtro = [];
    $scope.totalItems = $scope.clientes_filtro.length;
    $scope.currentPage = 1;
    $scope.numPerPage = 10;

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.clientes_filtro.indexOf(value);
        return (begin <= index && index < end);
    };

    $scope.currentuser = function (obj) {
        $scope.curruser = obj;
        $("#myModal").modal();
    }
    $scope.inicial = function () {
        $scope.clientes_filtro = $scope.clientes;
        $scope.totalItems = $scope.clientes_filtro.length;
    }


}]);

function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}