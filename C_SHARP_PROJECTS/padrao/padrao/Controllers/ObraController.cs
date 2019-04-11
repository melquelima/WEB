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
    public class ObraController : ApiController
    {
        ObraNegocio ObraNegocio = new ObraNegocio();
        ArtPicturesNegocio ArtPicturesNegocio = new ArtPicturesNegocio();
        FileUpload FileUpload = new FileUpload();

        [HttpGet]
        public Retorno ListTbl()
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                
                ret.Response = ObraNegocio.Get();
            }
            catch (Exception ex)
            {
                ret.Message = ex.Message;
                ret.Status = false;
                throw;
            }
            return ret;
        }

        [HttpGet]
        public Retorno ListTbl(String CPF)
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ret.Response = ObraNegocio.Get(CPF);
            }
            catch (Exception ex)
            {
                ret.Message = ex.Message;
                ret.Status = false;
                throw;
            }
            return ret;
        }

        [HttpGet]
        public Retorno ListView()
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {

                ret.Response = ObraNegocio.GetVW();
            }
            catch (Exception ex)
            {
                ret.Message = ex.Message;
                ret.Status = false;
                throw;
            }
            return ret;
        }

        [HttpGet]
        public Retorno ListView(String CPF)
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ret.Response = ObraNegocio.GetVW(CPF);
            }
            catch (Exception ex)
            {
                ret.Message = ex.Message;
                ret.Status = false;
                throw;
            }
            return ret;
        }

        [HttpPost]
        public Retorno Save(List<Obras> Obras)
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ObraNegocio.Save(Obras);
                ret.Message = "Obras salvas com sucesso!";
            }
            catch (Exception ex)
            {
                ret.Status = false;
                String msg = ex.InnerException.InnerException.ToString();
                //throw;
            }
            return ret;
        }

        [HttpPost]
        public Retorno Delete(List<Artistas> artistas)
        {
            List<Obras> Obras = new List<Obras>();
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                //===============================DELETANDO ARQUIVOS
                Obras = ObraNegocio.Get(artistas);
                List<ArtPictures> ArtPictures = new List<ArtPictures>();
                ArtPictures = ArtPicturesNegocio.Get(Obras); //LISTA DODOS OS ARQUIVOS NO BANCO
                foreach (var Pictures in ArtPictures) //DELETA TODOS
                {
                    FileUpload.delete(Pictures.FILE);
                }
                ArtPicturesNegocio.DeleteByObra(Obras);//DELETA TODOS OS REGISTROS NO BANCO

                ObraNegocio.Delete(artistas);
                ret.Message = "Obras deletadas com sucesso!";
            }
            catch (Exception ex)
            {
                ret.Status = false;
                ret.Message = ex.Message;
                //throw;
            }
            return ret;
        }
        [HttpPost]
        public Retorno Delete(List<Obras> obras)
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ObraNegocio.Delete(obras);
                //===============================DELETANDO ARQUIVOS
                List<ArtPictures> ArtPictures = new List<ArtPictures>();
                ArtPictures = ArtPicturesNegocio.Get(obras); //LISTA DODOS OS ARQUIVOS NO BANCO

                foreach (var Pictures in ArtPictures) //DELETA TODOS
                {
                    FileUpload.delete(Pictures.FILE);
                }

                ArtPicturesNegocio.DeleteByObra(obras);//DELETA TODOS OS REGISTROS NO BANCO

                ret.Message = "Obras deletadas com sucesso!";
            }
            catch (Exception ex)
            {
                ret.Status = false;
                ret.Message = ex.Message;
                //throw;
            }
            return ret;
        }

    }
}
