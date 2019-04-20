   
    loop();
   
    /* Line dashboard chart */
var valores = JSON.parse(httpGet2("/api/tcc/teste"));

valores.sort(function (a, b) {
    // Turn your strings into dates, and then subtract them
    // to get a value that is either negative, positive, or zero.
    return new Date(b.date) - new Date(a.date);
});

var data = [];
var data2 = [];     console.log(valores);
    for (i = 0; i < valores.length; i++) {
        var mascara = { y: null, a: null,b: null};
        mascara.y = valores[i].Data;
        mascara.a = valores[i].Temperatura;
        mascara.b = valores[i].Batimentos;
        data.push(mascara);
    }
  var chart = Morris.Line({
        element: 'dashboard-line-1',
        data: data,
        xkey: 'y',
        ykeys: ['a', 'b'],
        labels: ['C', 'BPM'],
        resize: true,
        hideHover: true,
        xLabels: 'day',
        gridTextSize: '10px',
        lineColors: ['#1caf9a', '#33414E'],
        gridLineColor: '#E5E5E5',
        redraw: true,
        parseTime: false,
    }); 




function loop() {
    setTimeout(function () { console.log("Hello"); graf(); loop();}, 1000);
}

function graf() {

    var valores = JSON.parse(httpGet2("/api/tcc/teste"));
    var data = [];

    // console.log(valores);
    for (i = 0; i < valores.length; i++) {
        var mascara = { y: null, a: null, b:null};
        mascara.y = valores[i].Data;
        mascara.a = valores[i].Temperatura;
        mascara.b = valores[i].Batimentos;
        data.push(mascara);
    }
    // console.log(data);


    chart.setData(data);



   
    /* EMD Line dashboard chart */

}


function httpGet(theUrl) {

    $.ajax({
        headers:
        {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "GET",
        dataType: 'application/json',
        url: theUrl,
        success: function (result) {
            console.log(result);
        }
    });
}

function httpGet2(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}

