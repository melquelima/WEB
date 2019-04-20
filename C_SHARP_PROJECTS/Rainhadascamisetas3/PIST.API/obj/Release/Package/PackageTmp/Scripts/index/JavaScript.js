$(document).ready(function () {
    var data = localStorage.getItem("R_Visitor");
    var hoje = new Date().toISOString().slice(0, 19).replace('T', ' ').split(' ')[0];
    
    if (data != null) {
        if (data != hoje) {
            httpGet("/home/visit");
            localStorage.setItem("R_Visitor", hoje);
            console.log("1");
        }
    } else {
        httpGet("/home/visit");
        localStorage.setItem("R_Visitor", hoje);
        console.log("2");
    }
});


function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}