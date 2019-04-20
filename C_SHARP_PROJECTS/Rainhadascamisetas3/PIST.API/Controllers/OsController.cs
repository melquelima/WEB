
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
using System.Web.Mvc;
using iTextSharp.text;
using System.IO;
using PIST.API.Models;
using System.Web;


namespace PIST.API.Controllers
{

    public class OsController : ApiController
    {

        IOsNegocio osNegocio;
        IUsuarioNegocio2 usuarioNegocio;
        public OsController(IOsNegocio _corNegocio, IUsuarioNegocio2 _usuarioNegocio)
        {
            osNegocio = _corNegocio;
            usuarioNegocio = _usuarioNegocio;
        }
        

        [System.Web.Http.HttpGet]
        public String Get()
        {
            try
            {                                                         
                return osNegocio.Get();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [System.Web.Http.HttpGet]
        public List<Pedido> GETPEDIDO(string OS)
        {
            List<Pedido> lista = new List<Pedido>();

            try
            {
                lista = osNegocio.Get_pedidos(OS);
            }
            catch (Exception)
            {

                throw;
            }

            return lista;

        }

        [System.Web.Http.HttpPost]
        public string Editar(List<Pedido> listagem)
        {
            try
            {
                osNegocio.Delete(listagem[0].OS);
                ObjectRetorno objectRetorno = new ObjectRetorno();

                try
                {
                    var erros = Util.getValidationErros(listagem);
                    if (erros.Count() == 0)
                    {
                        objectRetorno.data = osNegocio.Save(listagem);
                        User cliente = usuarioNegocio.Get(listagem[0].CPF);

                        try
                        {
                            string caminho = GeradorNota.GerarNota(listagem, cliente);

                            GeradorNota.pdf_image(caminho);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

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

            }
            catch (Exception)
            {

                throw;
            }
            
            return listagem[0].OS;
        }

        [System.Web.Http.HttpGet]
        public ObjectRetorno Update(string Os,string entrada,DateTime data)
        {
            ObjectRetorno objectRetorno = new ObjectRetorno();
            try
            {
                var erros = Util.getValidationErros(entrada);
                if (erros.Count() == 0)
                {
                    string saida = osNegocio.Update_status(Os,entrada,data);
                    if (saida != null)
                    {
                        objectRetorno.data = saida;
                    }
                    else
                    {
                        objectRetorno = new ObjectRetorno(codeEnum.RegistroNaoEncontrado);
                    }
                }
                else
                {
                    objectRetorno.data = erros.Select(p => p.ErrorMessage).ToList();
                }
            }
            catch (Exception ex)
            {
                objectRetorno = new ObjectRetorno(codeEnum.ErroCritico);
                throw;
            }
            return objectRetorno;
        }


        [System.Web.Http.HttpPost]
        public string SalvaNota(List<Pedido> listagem)
        {
            string os = osNegocio.Get();

            for (int i = 0; i < listagem.Count(); i++)
            {
                listagem[i].DATA = DateTime.Now.AddHours(-2);
                listagem[i].OS = os;
                listagem[i].ORCAMENTO = 0;
            }

            ObjectRetorno objectRetorno = new ObjectRetorno();

            try
            {
                var erros = Util.getValidationErros(listagem);
                if (erros.Count() == 0)
                {
                    objectRetorno.data = osNegocio.Save(listagem);
                    //User cliente = usuarioNegocio.Get(listagem[0].CPF);

                    //try
                    //{
                    //    string caminho = GeradorNota.GerarNota(listagem, cliente);

                    //    GeradorNota.pdf_image(caminho);
                    //}
                    //catch (Exception)
                    //{

                    //    throw;
                    //}
                    
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

            //List<Pedido> produtos = new List<Pedido>();
            //Pedido x = new Pedido();
            //x.PRODUTO = "blusa";
            //x.MALHA = "Poliester";
            //x.COR = "Branco";
            //x.TAMANHO = "P";
            //x.QUANTIDADE = 2;
            //x.VALOR = 25;
            //x.CPF = "28756984863";
            //for (int i = 0; i < 23; i++)
            //{
            //    produtos.Add(x);
            //}



            return os;

        }


       
    }

    
}
