$(document).ready(function () {

});


//window.App = angular.module('App', ['ui.bootstrap', 'ngResource']);
app.controller('mainCtrl', ['$scope', function ($scope) {

    $scope.Visitantes = JSON.parse(httpGet("/api/visitantes/get"));

}]);

function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}