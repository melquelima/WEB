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

namespace PIST.API.Controllers
{
    
    public class DevedoresController : ApiController
	{
        IDevedoresOsNegocio DevedoresOsNegocio;
        public DevedoresController(IDevedoresOsNegocio _usuarioNegocio)
        {
            DevedoresOsNegocio = _usuarioNegocio;
        }
        // GET api/values

        [HttpGet]
		public  List<DevedoresOs> Get()
		{
            try
            {
                List <DevedoresOs> devedores = new List<DevedoresOs>();
                    devedores = DevedoresOsNegocio.Get();
                return devedores;
            }
            catch (Exception ex)
            {
                throw;
            }
			
		}

        [HttpGet]
        public List<DevedoresOs> Get(string cpf)
        {
            try
            {
                return DevedoresOsNegocio.Get(cpf);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
