using ASP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Rainhadascamisetas.Models
{
    public class getCNPJ
    {
        public class ReceitaWsResponse
        {
            public string cnpj { get; set; }
            public string nome { get; set; }
            public string fantasia { get; set; }
            public string telefone { get; set; }
            public string email { get; set; }
            public string cep { get; set; }
            public string logradouro { get; set; }
            public string numero { get; set; }
            public string complemento { get; set; }
            public string bairro { get; set; }
            public string municipio { get; set; }
            public string uf { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public string situacao { get; set; }
            public string abertura { get; set; }
            public string natureza_juridica { get; set; }
            public string tipo { get; set; }

        }

        public static ReceitaWsResponse ConsultaCnpj(string cnpj)
        {
            var url = string.Format("https://www.receitaws.com.br/v1/cnpj/{0}", cnpj);
            var wc = new WebClient();
            string resp;

            try
            {
                resp = wc.DownloadString(url);
            }
            catch (Exception)
            {
                return null;
            }

            return SimpleJson.DeserializeObject<ReceitaWsResponse>(resp);
        }
    }
}