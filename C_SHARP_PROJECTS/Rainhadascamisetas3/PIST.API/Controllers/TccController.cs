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
using System.Web;
using System.IO;

namespace PIST.API.Controllers
{
    
    public class TccController : ApiController
	{
        ITccNegocio tccNegocio;
        public TccController(ITccNegocio _tccNegocio)
        {
            tccNegocio = _tccNegocio;
        }

        [HttpGet]
        public List<Tcc> teste()
        {
            return tccNegocio.Get();
        }

        [HttpPost]
        public string UploadFile()
        {
            string retorno = "ERROR";
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    var asd = HttpContext.Current.Request["teste"];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/images"), httpPostedFile.FileName);

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);
                        retorno = File.Exists(fileSavePath) ? "OK" : "ERROR";
                    }
                }
                return retorno;
            }
            catch (Exception e)
            {
                return e.Message;
            }
           
        }


        [HttpGet]
        public string Save(double temp,double oxi,double bat)
        {
            ObjectRetorno objectRetorno = new ObjectRetorno();
            Tcc entrada = new Tcc();
            entrada.Data = DateTime.Now;
            entrada.Temperatura = temp;
            entrada.Oximetria = oxi;
            entrada.Batimentos = bat;
            string retorno = "%Salvo";

            try
            {
                var erros = Util.getValidationErros(entrada);
                if (erros.Count() == 0)
                {
                    objectRetorno.data = tccNegocio.Save(entrada);
                }
                else
                {
                    objectRetorno.data = erros.Select(p => p.ErrorMessage).ToList();
                }
            }
            catch (Exception ex)
            {
                retorno = "%ERROR";
                throw;
            }

            return retorno;

        }
    }
}
