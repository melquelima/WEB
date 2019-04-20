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
using PIST.Negocio;

namespace PIST.API.Controllers
{
    
    public class VisitantesController : ApiController
	{
        VisitantesNegocio visitantesNegocio = new VisitantesNegocio();
        

        
        [HttpGet]
		public  List<Visitantes> Get()
		{
            try
            {
                return visitantesNegocio.Get();
            }
            catch (Exception ex)
            {
                throw;
            }
			
		}
        
    }
}
