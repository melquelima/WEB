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
    public class ArtistaController : ApiController
    {
        ArtistaNegocio ArtistaNegocio = new ArtistaNegocio();
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
                ret.Response =  ArtistaNegocio.Get();
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
                ret.Response = ArtistaNegocio.Get(CPF);
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
                ret.Response = ArtistaNegocio.GetVW();
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
                ret.Response = ArtistaNegocio.GetVW(CPF);
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
        public Retorno Save(List<Artistas> artistas)
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ArtistaNegocio.Save(artistas);
                ret.Message = "Artista salvo com sucesso!";
            }
            catch (Exception ex)
            {
                ret.Status = false;
                String msg = ex.InnerException.InnerException.ToString();
                ret.Message = msg.IndexOf("PRIMARY KEY") > -1 ? "Artista já existe na base de dados!" : msg;
                //throw;
            }
            return ret;
        }

        [HttpPost]
        public Retorno DeleteAll(List<Artistas> artistas)
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

                ArtistaNegocio.Delete(artistas);
                ret.Message = "Artista e obras deletados com sucesso!";
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
