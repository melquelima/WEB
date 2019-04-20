using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PIST.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter
            .SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            
        }
    }
   
}

//var dados = {
//           Email           : 'melque@asdad.com'
//           ,IdUsuario       : 458
//           ,NomeUsuario     : 'MELQUESEDEQUE'
//           ,Senha           : 'teste123'
//        };
//        $.ajax({
//    headers:
//    {
//        'Accept': 'application/json',
//        'Content-Type': 'application/json'
//        },
//            type: "POST",
//            data: JSON.stringify(dados),
//            dataType: 'application/json',
//            url: "http://localhost:60911/api/values/PostaUsuario",
//            success: function(result) {
//        console.log(result);
//    }
//});

//var dados = [{"ID":8888, "OS":null, "DATA": null, "CPF":"01075444586", "MALHA":"poliester", "CAT_PRODUTO":"A", "PRODUTO":"B", "COR":"BRANCO", "CAT_TAMANHO":"C", "TAMANHO":"GRANDE", "VALOR":12.5, "QUANTIDADE":2, "DATA_ENTREGA":null},{"ID":9999,"OS":null,"DATA": null,"CPF":"01075444586","MALHA":"poliester","CAT_PRODUTO":"A","PRODUTO":"B","COR":"BRANCO","CAT_TAMANHO":"C","TAMANHO":"GRANDE","VALOR":12.5,"QUANTIDADE":2,"DATA_ENTREGA":null},{"ID":9999,"OS":null,"DATA": null,"CPF":"01075444586","MALHA":"poliester","CAT_PRODUTO":"A","PRODUTO":"B","COR":"BRANCO","CAT_TAMANHO":"C","TAMANHO":"GRANDE","VALOR":12.5,"QUANTIDADE":2,"DATA_ENTREGA":null},{"ID":9999,"OS":null,"DATA": null,"CPF":"01075444586","MALHA":"poliester","CAT_PRODUTO":"A","PRODUTO":"B","COR":"BRANCO","CAT_TAMANHO":"C","TAMANHO":"GRANDE","VALOR":12.5,"QUANTIDADE":2,"DATA_ENTREGA":null}]


//x = new XMLHttpRequest();
//x.open("POST", "/api/os/pdf",true);
//	x.setRequestHeader("Content-type", "application/json");

//x.onreadystatechange = function()
//{
//    //Your code here to handle readyState==4 and readyState==3
//    if (x.readyState == 4 && x.status == 200)
//    {
//        console.log("====OK====");
//        console.log(x.responseText);
//    }
//    else if (x.readyState == 4 && !(x.status == 200))
//    {
//        console.log("====NOK====");
//        console.log(x.statusText);
//    }
//}
//x.send(JSON.stringify(dados));