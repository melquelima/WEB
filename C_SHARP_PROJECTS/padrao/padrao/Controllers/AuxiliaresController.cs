using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Entidade;
using padrao.Interface;
using padrao.Models;
using padrao.Negocio;

namespace padrao.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuxiliaresController : ApiController
    {
        UfCidadeNegocio UfCidadeNegocio = new UfCidadeNegocio();

        [HttpGet]
        public Retorno ListAll()
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ret.Response = UfCidadeNegocio.Get();
            }
            catch (Exception ex)
            {
                ret.Message = ex.Message;
                ret.Status = false;
                throw;
            }
            return ret;
        }
    }
}
