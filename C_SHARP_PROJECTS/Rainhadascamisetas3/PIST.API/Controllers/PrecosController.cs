using Entidade;
using Newtonsoft.Json.Linq;
using PIST.NegocioInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PIST.API.Controllers
{
    
    public class PrecosController : ApiController
	{
       
        IPrecosNegocio precosNegocio;
        public PrecosController(IPrecosNegocio _precosNegocio)
        {
            precosNegocio = _precosNegocio;
        }
        // GET api/values


        [HttpGet]
		public  List<Precos> Get()
		{
            try
            {
                return precosNegocio.Get();
            }
            catch (Exception ex)
            {
                throw;
            }
			
		}

        [HttpGet]
        public List<PrecosDetalhado> detalhado()
        {
            try
            {
                return precosNegocio.Detalhado();
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
