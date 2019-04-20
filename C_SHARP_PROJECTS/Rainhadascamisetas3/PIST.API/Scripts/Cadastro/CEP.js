
window.onload = function (e) {
    document.getElementById("cep").focus();
    habilitaCampos(false); 
    habilitaPessoal(false);
    id('tel1').onkeyup = function () {
        mascara(this, mtel);
    }
    id('cep').onkeyup = function () {
        mascara(this, soNumero);
    }
    id('cep').onchange = function () {
        this.value = this.value.replace(/\D/g, "");
        GetCEP(this);
    }
    id('tel2').onkeyup = function () {
        mascara(this, mtel);
    }
    id('tel3').onkeyup = function () {
        mascara(this, mtel);
    }
}


function cadastrar() {
    var dados = {
        CPF: getid("CPF"),
        DataCadastro: new Date().toISOString().slice(0, 19).replace('T', ' ').split(' ')[0],
        Nome: getid("nome").toUpperCase(),
        NomeEmpresa: document.getElementById("NomeEmpresa").innerText.toUpperCase(),
        CNPJ: getid("cnpj"),
        Tel1: getid("tel1"),
        Tel2: getid("tel2"),
        Tel3: getid("tel3"),
        Email: getid("email"),
        CEP: getid("cep"),
        UF: getid("uf").toUpperCase(),
        Endereco: getid("endereco").toUpperCase(),
        Numero: getid("numero"),
        Cidade: getid("cidade"),
        Bairro: getid("bairro"),
        Complemento: getid("complemento"),
        senha: getid("senha1")
    }
    console.log(dados);

   
    $.ajax({
        headers:
        {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "POST",
        data: JSON.stringify(dados),
        dataType: 'application/json',
        url: "/api/usuario/save",
        success: function (result) {
            console.log(result);
        }
    });

    document.getElementById("cadastrando").style.display = "";

    setTimeout(function () {
        if (CpfExiste(getid("CPF"))) {
            console.log("=======CADASTRADO=======");
            document.getElementById("cadastrando").style.display = "none";
            $("#myModal").modal();
        } else {
            alert("Erro ao cadastrar!");

        }

    }, 3000);




}

function confirmaSenha() {
    var senha = getid("senha1");
    var senha2 = getid("senha2");
    console.log(senha + ' - ' + senha2);
    if (senha != senha2) {
        document.getElementById("senhaalert").innerText = "Senhas não conferem!";
        document.getElementById("senhaalert").style.display = "";
        document.getElementById("finish").disabled = true;
        //alert(senha + ' - ' + senha2);
    } else {
        if (senha.length < 8) {
            document.getElementById("senhaalert").style.display = "";
            document.getElementById("senhaalert").innerText = "Sua senha deve conter no minimo 8 caracteres!";
            document.getElementById("finish").disabled = true;
        } else {
            document.getElementById("senhaalert").style.display = "none";
            document.getElementById("finish").disabled = false;
        }
    }
}

function getid(id) {
    return document.getElementById(id).value;
}

function CpfExiste(cpf) {
    var json = httpGet("/api/usuario/usuarioexiste?doc=" + cpf);
    return (json != 'null' ? true : false);
}

function GetCNPJName() {
    obj = document.getElementById("cnpj");
    if (obj.value.length >= 13) {                        //verifica se  o cnpj é valido
        //document.getElementById("carregandocnpj").style.display = "";
        document.getElementById("btn_cpj").style.display = "none";
        var cnpj = obj.value;
        var existebanco = ExisteCnpjBanco(cnpj);
        
        if (existebanco != "") {                        //verifica se o mesmo já existe no banco de dados
            document.getElementById("aok").style.display = "";
            document.getElementById("anok").style.display = "none";
            document.getElementById("btn_cpj").style.display = "none";
            document.getElementById("NomeEmpresa").innerText = existebanco;
            document.getElementById("cnpj").disabled = true;
        } else {
            var EmpresaNome = JSON.parse(httpGet("/home/VerificaCnpj?cnpj=" + cnpj));
            if (EmpresaNome.fantasia == null) {
                document.getElementById("anok").style.display = "";
                document.getElementById("NomeEmpresa").innerText = "CNPJ inválido"
                document.getElementById("btn_cpj").style.display = "";
            } else {
                nome = (EmpresaNome.fantasia == "" ? EmpresaNome.nome : EmpresaNome.fantasia);
                document.getElementById("aok").style.display = "";
                document.getElementById("anok").style.display = "none";
                document.getElementById("btn_cpj").style.display = "none";
                document.getElementById("NomeEmpresa").innerText = nome;
                document.getElementById("cnpj").disabled = true;
            }
        }
    } else {
        document.getElementById("NomeEmpresa").innerText = "CNPJ deve conter no mínimo 13 digitos"
    }
    //document.getElementById("carregandocnpj").style.display = "none";

}
function ExisteCnpjBanco(cnpj) {
    var response = httpGet("/api/usuario/usuarioexiste?doc=" + cnpj);
    if (response != "null") {
        return JSON.parse(response).Nome;
    } else {
        return "";
    }
}

    //$(function () {
    //    $("#verifica").on("click", function () {
    //        var val = $("#CNPJ").val();

    //        $.ajax({
    //            url: "@Url.Action("VerificaCnpj", "Exemplo")",
    //            type: "GET",
    //            dataType: "JSON",
    //            data: { cnpj: val },
    //            success: function (data) {
    //                $("#RAZAOSOCIAL").val(data.Razao);
    //            }
    //        });
    //    })
    //});


//function EmpresaNome() {
//    if (document.getElementById(a).value.length == 14) {
//        document.getElementById(b).disabled = false;
//    } else {
//        document.getElementById(b).value = '';
//        document.getElementById(b).disabled = true;
//    }
//}

function ativacampo(obj) {
    switch (obj) {
        case 1:
            if (validacao("uf", "endereco",1)) {
                popula_Cidade(document.getElementById("uf").value);
                document.getElementById("complemento").disabled = false;
            }
            break;
        case 2:
            validacao("endereco", "numero",5);
            break;
        case 3:
            a = document.getElementById("cep").disabled;
            if (!a) {
                validacao("numero", "cidade", 0);
            }
            break;
        case 4:
            if (validacao("cidade", "bairro", 0)) {
                Id = document.getElementById("cidade").value.split("|")[1];
                popula_Bairros(Id);
            }
            break;
        case 5:
            CPF = document.getElementById("CPF").value;
            document.getElementById("next").disabled = false;
            if (CPF.length > 10) {
                if (TestaCPF(CPF)) {
                    document.getElementById("errCPF").style.display = "none";
                    if (CpfExiste(CPF)) {
                        document.getElementById("errCPF").style.display = "";
                        document.getElementById("errCPF").innerText = "  Usuario já cadastrado!";
                        document.getElementById("next").disabled = true;
                    } else { habilitaPessoal(true);}
                } else {
                    document.getElementById("errCPF").style.display = "";
                    document.getElementById("errCPF").innerText = "  CPF inválido";
                    document.getElementById("next").disabled = true;
                    habilitaPessoal(false);
                }
            } else {
                document.getElementById("errCPF").style.display = "";
                document.getElementById("errCPF").innerText = "  CPF deve conter 11 Digitos";
                habilitaPessoal(false);
            }
            break;
    }
}

function validacao(a,b,value) {
    if (document.getElementById(a).value.length > value) {
        document.getElementById(b).disabled = false;
        return true;
    } else {
        document.getElementById(b).value = '';
        document.getElementById(b).disabled = true;
        return false;
    }
}

function popula_Bairros(Id) {
    request = httpGet("http://enderecos.metheora.com/api/cidade/" + Id + "/bairros/");
   
    ufs = JSON.parse(request);
    console.log(ufs);
    document.getElementById("bairro").innerHTML = "";
    $('#bairro').append($('<option>').text('Selecione').attr('value', ''));
    $.each(ufs, function (i, value) {
        $('#bairro').append($('<option>').text(value.Nome).attr('value', value.Nome));
    });
}

function popula_UF() {
    request = httpGet("http://enderecos.metheora.com/api/estados");
    ufs = JSON.parse(request);
    
    document.getElementById("uf").innerHTML = "";
    $('#uf').append($('<option>').text('Selecione').attr('value', ''));
    $.each(ufs, function (i, value) {
        $('#uf').append($('<option>').text(value.Nome).attr('value', value.Nome));
    });
}

function popula_Cidade(UF) {
    request = httpGet("http://enderecos.metheora.com/api/estado/" + UF + "/cidades/");
    cidades = JSON.parse(request);

    console.log(cidades.length);
    document.getElementById("cidade").innerHTML = "";

    $('#cidade').append($('<option>').text('Selecione').attr('value', ''));
    $.each(cidades, function (i, value) {
        $('#cidade').append($('<option>').text(value.Nome).attr('value', value.Nome + "|" + value.Id));
    });
}


function GetCEP(object) {
    cep = object.value;
    if (cep.length == 8) {
        response = httpGet("http://enderecos.metheora.com/api/cep/" + cep);
        jsonAddrs = JSON.parse(response);
        console.log(jsonAddrs);
        if (typeof jsonAddrs[0] != 'undefined') {
            $('#uf').append($('<option>').text(jsonAddrs[0].Bairro.Cidade.UF).attr('value', jsonAddrs[0].Bairro.Cidade.UF));
            $('#cidade').append($('<option>').text(jsonAddrs[0].Bairro.Cidade.Nome).attr('value', jsonAddrs[0].Bairro.Cidade.Nome));
            $('#bairro').append($('<option>').text(jsonAddrs[0].Bairro.Nome).attr('value', jsonAddrs[0].Bairro.Nome));

            document.getElementById("endereco").value = jsonAddrs[0].Tipo + " " + jsonAddrs[0].Nome;
            document.getElementById("bairro").value = jsonAddrs[0].Bairro.Nome;
            document.getElementById("cidade").value = jsonAddrs[0].Bairro.Cidade.Nome;
            document.getElementById("uf").value = jsonAddrs[0].Bairro.Cidade.UF;
            


            document.getElementById("cep").disabled = true;
            habilitaCampos(false);
            document.getElementById("numero").disabled = false
            document.getElementById("complemento").disabled = false;
            document.getElementById("numero").focus();
        } else {
            popula_UF();
            document.getElementById("uf").disabled = false;
            document.getElementById("uf").focus();
            console.log("CPF Não Encontrado na Base de Dados");
        }
    }

}

function habilitaCampos(par) {
    document.getElementById("endereco").disabled = !par;
    document.getElementById("bairro").disabled = !par;
    document.getElementById("cidade").disabled = !par;
    document.getElementById("uf").disabled = !par;
    document.getElementById("numero").disabled = !par;
    document.getElementById("complemento").disabled = !par;
}

function habilitaPessoal(par) {

    document.getElementById("nome").disabled = !par;
    document.getElementById("tel1").disabled = !par;
    document.getElementById("tel2").disabled = !par;
    document.getElementById("tel3").disabled = !par;
    document.getElementById("email").disabled = !par;
}


function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}

function httpGetAsync(theUrl, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.timeout = 2000;
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", theUrl, true); // true for asynchronous 
    xmlHttp.send(null);
}

function TestaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;
    if (strCPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
}



//---------------------------MASCARA TEL-------------------------------------

function mascara(o, f) {
    v_obj = o
    v_fun = f
    setTimeout("execmascara()", 1)
}
function execmascara() {
    v_obj.value = v_fun(v_obj.value)
}
function mtel(v) {
    v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}
function soNumero(v) {
    v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
    return v;
}
function id(el) {
    return document.getElementById(el);
}



