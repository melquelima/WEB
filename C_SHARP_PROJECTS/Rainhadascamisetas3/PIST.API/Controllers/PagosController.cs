
using Entidade;
using Newtonsoft.Json.Linq;
using PIST.NegocioInterface;
using Rainhadascamisetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Configuration;
using static Rainhadascamisetas.Models.Util;
using System.Web.Mvc;
using iTextSharp.text;
using System.IO;
using PIST.API.Models;

namespace PIST.API.Controllers
{

    public class PagosController : ApiController
    {

        IPagosNegocio pagosNegocio;
        public PagosController(IPagosNegocio _pagosNegocio)
        {
            pagosNegocio = _pagosNegocio;
        }

        [System.Web.Http.HttpPost]
        public Boolean Delete(List<Pago> entrada)
        {
            var x = pagosNegocio.Delete(entrada);

            return x;
        }


        [System.Web.Http.HttpGet]
        public List<Pago> Get()
        {
            try
            {
                return pagosNegocio.Get();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [System.Web.Http.HttpPost]
        public ObjectRetorno save(List<Pago> entrada)
        {
            ObjectRetorno objectRetorno = new ObjectRetorno();

            try
            {
                var erros = Util.getValidationErros(entrada);
                if (erros.Count() == 0)
                {
                    objectRetorno.data = pagosNegocio.Save(entrada);
                }
                else
                {
                    objectRetorno.data = erros.Select(p => p.ErrorMessage).ToList();
                }
            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                objectRetorno = new ObjectRetorno(codeEnum.ErroCritico);
                throw;
            }

            return objectRetorno;

        }


    }

    
}
