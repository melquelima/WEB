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
    
    public class UsuarioController : ApiController
	{
        IUsuarioNegocio2 usuarioNegocio;
        public UsuarioController(IUsuarioNegocio2 _usuarioNegocio)
        {
            usuarioNegocio = _usuarioNegocio;
        }
        // GET api/values

        [HttpGet]
        public User usuarioexiste(String doc)
        {
            User saida = usuarioNegocio.Get(doc);
            return saida;
        }

        [HttpGet]
		public  List<User> Get()
		{
            try
            {
                return usuarioNegocio.Get();
            }
            catch (Exception ex)
            {
                throw;
            }
			
		}

        [HttpPost]
        public ObjectRetorno Save(User entrada)
        {
            ObjectRetorno objectRetorno = new ObjectRetorno();

            try
            {
                var erros = Util.getValidationErros(entrada);
                if (erros.Count() == 0)
                {
                    objectRetorno.data = usuarioNegocio.Save(entrada);
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
