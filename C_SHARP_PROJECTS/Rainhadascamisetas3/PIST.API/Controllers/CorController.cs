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
    
    public class CorController : ApiController
	{
        ICorNegocio corNegocio;
        public CorController(ICorNegocio _corNegocio)
        {
            corNegocio = _corNegocio;
        }

        [HttpGet]
        public List<Cor> Get()
        {
            try
            {
                return corNegocio.Get();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public Cor Delete(string Cor)
        {
            try
            {
                Cor x = new Entidade.Cor();
                x.COR = Cor;
                return corNegocio.Delete(x);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public Cor Save(string Cor)
        {
            try
            {
                Cor x = new Entidade.Cor();
                x.COR = Cor;
                return corNegocio.Save(x);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
