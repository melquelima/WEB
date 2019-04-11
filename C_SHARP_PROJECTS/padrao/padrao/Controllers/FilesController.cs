using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Entidade;
using padrao.Models;
using padrao.Negocio;


namespace padrao.Controllers
{
    public class FilesController : ApiController
    {
        FileUpload FileUpload = new FileUpload();
        ObraNegocio ObraNegocio = new ObraNegocio();
        ArtPicturesNegocio ArtPicturesNegocio = new ArtPicturesNegocio();

        public async Task<Retorno> Upload()
        {
            List<Obras> Obras = new List<Obras>();
            List<ArtPictures> ArtPictures = new List<ArtPictures>();
            Retorno ret = new Retorno();
            List<string> newNames = new List<string>();
            ret.Status = true;
            ret.Message = "Arquivos Carregados com Sucesso!";

            var ID_OBRA = HttpContext.Current.Request.Params["ID_OBRA"];

            if (ID_OBRA != null)
            {
                Obras = ObraNegocio.GetByID(Convert.ToInt32(ID_OBRA));
                if (Obras.Count() > 0)
                {                    
                    //SOBE O ARQUIVO PARA O SERVIDOR
                    try
                    {
                        for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                        {
                            newNames.Add(Guid.NewGuid().ToString() + Path.GetExtension(HttpContext.Current.Request.Files[i].FileName));
                        }
                            
                        await FileUpload.Upload(Request, "~/documents", newNames);
                    }
                    catch (Exception ex)
                    {
                        ret.Message = ex.Message;
                        ret.Status = false;
                    }
                    //INSERE A IMAGEM NO BANCO
                    try
                    {
                        foreach (var newName in newNames)
                        {
                            ArtPictures ArtPicture = new ArtPictures();
                            ArtPicture.ID_OBRA = Convert.ToInt32(ID_OBRA);
                            ArtPicture.FILE = newName;
                            ArtPictures.Add(ArtPicture);
                        }
                        ArtPicturesNegocio.Save(ArtPictures);
                        ret.Response = ArtPicturesNegocio.GetByObra(Convert.ToInt32(ID_OBRA));
                    }
                    catch (Exception ex)
                    {
                        ret.Message = ex.Message;
                        ret.Status = false;
                        throw;
                    }

                }
                else
                {
                    ret.Message = "Obra não encontrada";
                    ret.Status = false;
                }
            }
            else
            {
                ret.Message = "Insira a obra relacionada ao arquivo!";
                ret.Status = false;
            }
            
            return ret;
        }

        [HttpGet]
        public Retorno Pictures()
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ret.Response = ArtPicturesNegocio.Get();
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
        public Retorno Pictures(int ID_OBRA)
        {
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ret.Response = ArtPicturesNegocio.GetByObra(ID_OBRA);
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
        public Retorno Delete()
        {
            List<ArtPictures> ArtPictures = new List<ArtPictures>();
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ArtPictures = ArtPicturesNegocio.Get();

                foreach (var Pictures in ArtPictures)
                {
                    FileUpload.delete(Pictures.FILE);
                }
                ArtPicturesNegocio.Delete();
                ret.Message = ArtPictures.Count.ToString() + " Arquivo(s) deletados com sucesso!";
            }
            catch (Exception ex)
            {
                ret.Message = ex.Message;
                ret.Status = false;
            }
            return ret;
        }

        [HttpGet]
        public Retorno DeleteByArte(int ID_ARTE)
        {
            List<ArtPictures> ArtPictures = new List<ArtPictures>();
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ArtPictures = ArtPicturesNegocio.GetByArte(ID_ARTE);

                foreach (var a in ArtPictures)
                {
                    FileUpload.delete(a.FILE);
                }
               
                ArtPicturesNegocio.DeleteByArte(ID_ARTE);
                ret.Message = ArtPictures.Count.ToString() + " Arquivo(s) deletados com sucesso!";
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
        public Retorno DeleteByObra(int ID_OBRA)
        {
            List<ArtPictures> ArtPictures = new List<ArtPictures>();
            Retorno ret = new Retorno();
            ret.Status = true;
            try
            {
                ArtPictures = ArtPicturesNegocio.GetByObra(ID_OBRA); //LISTA DODOS OS ARQUIVOS NO BANCO

                foreach (var Pictures in ArtPictures) //DELETA TODOS
                {
                    FileUpload.delete(Pictures.FILE);
                }

                ArtPicturesNegocio.DeleteByObra(ID_OBRA);//DELETA TODOS OS REGISTROS NO BANCO

                ret.Message = ArtPictures.Count.ToString() + " Arquivo(s) deletados com sucesso!";
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
