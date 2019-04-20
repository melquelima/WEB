using Rainhadascamisetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIST.NegocioInterface;
using PIST.API.Models;
using Entidade;
using PIST.Negocio;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace PIST.API.Controllers
{
    public class HomeController : Controller
    {
        VisitantesNegocio visitors = new VisitantesNegocio();



        public void barcode()
        {
            // Get the Requested code to be created.
            string Code = Request["code"].ToString();

            // Multiply the lenght of the code by 40 (just to have enough width)
            int w = Code.Length * 100;

            // Create a bitmap object of the width that we calculated and height of 100
            Bitmap oBitmap = new Bitmap(w, 200);

            // then create a Graphic object for the bitmap we just created.
            Graphics oGraphics = Graphics.FromImage(oBitmap);

            // Now create a Font object for the Barcode Font
            // (in this case the IDAutomationHC39M) of 18 point size
            //FontFamily a = new FontFamily(@"file:\\" + Server.MapPath("/fonts/code128.ttf"));
            Font oFont = new Font("Code 128", 60);

            // Let's create the Point and Brushes for the barcode
            PointF oPoint = new PointF(2f, 2f);
            SolidBrush oBrushWrite = new SolidBrush(Color.Black);
            SolidBrush oBrush = new SolidBrush(Color.White);

            // Now lets create the actual barcode image
            // with a rectangle filled with white color
            oGraphics.FillRectangle(oBrush, 0, 0, w, 300);

            // We have to put prefix and sufix of an asterisk (*),
            // in order to be a valid barcode
            oGraphics.DrawString(Code, oFont, oBrushWrite, oPoint);

            // Then we send the Graphics with the actual barcode
            Response.ContentType = "image/jpeg";
            oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        }

        public ActionResult EditarProdutos()
        {
            ViewBag.Title = "Home Page";

            return navbar("all", View(), true);
        }

        public ActionResult tcc()
        {
            ViewBag.Title = "Home Page";

            return navbar("all", View(), true);
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return navbar("all", View(), true);
        }

        public ActionResult teste()
        {
            ViewBag.Title = "Home Page";

            return navbar("all", View(), false);
        }

        public ActionResult Precos()
        {
            ViewBag.Message = "Your application description page.";

            return navbar("all", View(), true);
        }

        public ActionResult EmAberto()
        {
            ViewBag.Message = "Your application description page.";

            return navbar("adm", View(), false);
        }

        public ActionResult cadastro()
        {
            ViewBag.Message = "Your contact page.";

            return navbar("all", View(), true);
        }

        public ActionResult Clientes()
        {
            ViewBag.Message = "Your contact page.";

            return navbar("adm", View(), true);
        }

        public ActionResult Cores()
        {
            return navbar("adm", View(), true);
        }

        public ActionResult Acessos()
        {
            ViewBag.Message = "Your contact page.";

            return navbar("adm", View(), true);
        }

        public ActionResult MinhasCompras()
        {
            ViewBag.Message = "Your contact page.";

            return navbar("logged", View(), false);
        }

        public ActionResult Pedido()
        {
            ViewBag.Message = "Your contact page.";
            return navbar("logged", View(), true);
        }
        public ActionResult Editar(string OS)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.OS = OS;
            return navbar("adm", View("Pedido"), false);
        }

        public JsonResult VerificaCnpj(string cnpj)
        {

            //Faça a requisicao a API aqui
            var obj = getCNPJ.ConsultaCnpj(cnpj);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Http.HttpGet]
        public Boolean DeletarPedido(string OS)
        {
            ListaPedidoNegocio z = new ListaPedidoNegocio();
            return z.Delete(OS);
        }

        public ImageResult Nota(string id)
        {
            try
            {
                string caminho = Server.MapPath("/Content/images/Nota/") + id + ".jpg";
                if (!System.IO.File.Exists(caminho))
                {
                    CPF_OSNegocio x = new CPF_OSNegocio();
                    string cpf = x.Get_Cliente_Os(id);
                    if (cpf != "")
                    {
                        ListaPedidoNegocio z = new ListaPedidoNegocio();
                        List<Pedido> listagem = z.Get(id);
                        Usuario2Negocio y = new Usuario2Negocio();
                        User cliente = y.Get(cpf);
                        string caminho2 = GeradorNota.GerarNota(listagem, cliente);

                        GeradorNota.pdf_image(caminho2);
                    }

                }
                byte[] image = System.IO.File.ReadAllBytes(caminho);
                string contentType = "image/jpg";
                return ControllerExtensions.Image(image, contentType);
            }
            catch (Exception)
            {
                //throw;
                byte[] image = System.IO.File.ReadAllBytes(Server.MapPath("/Content/Images/error.jpg"));
                string contentType = "image/jpg";
                return ControllerExtensions.Image(image, contentType);

            }
        }

        public ActionResult logar(string doc, string Senha)
        {
            if (doc == "adm" && Senha == "adm123")
            {

                Session.Timeout = 200;
                Session["LOGADO"] = "OK";
                Session["CPF"] = "ADM";
                Session["CLIENTE"] = "Administrador";
            }
            else
            {
                LoginNegocio x = new LoginNegocio();
                User saida = x.Get(doc, Senha);
                if (saida != null)
                {
                    Session["LOGADO"] = "OK";
                    Session["CPF"] = saida.CPF;
                    Session["CLIENTE"] = saida.Nome;
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult logout()
        {
            Session["LOGADO"] = null;
            Session["CPF"] = null;
            Session["CLIENTE"] = null;
            return RedirectToAction("Index", "Home");
        }

        private ViewResult navbar(string param, ViewResult vw, Boolean funcionario)
        {
            if (Session["LOGADO"] == "OK")
            {
                ViewBag.usuario = Session["CLIENTE"];
                ViewBag.logado = "";
                ViewBag.login = "some";
                ViewBag.cpf = Session["CPF"];
                ViewBag.adm = Session["CPF"].ToString() == "12345678912" ? "FUNC" : Session["CPF"];
                ViewBag.aux = Session["CPF"].ToString() == "12345678912" || Session["CPF"].ToString() == "ADM" ? "" : "some";
                //   return RedirectToAction("cadastro", "Home");
            }
            else
            {
                ViewBag.usuario = "";
                ViewBag.logado = "some";
                ViewBag.login = "";
                ViewBag.adm = "some";
            }

            if (Session["LOGADO"] == "OK" && funcionario)
            {
                string cpf = Session["CPF"].ToString();
                if (cpf == "12345678912")
                {
                    return vw;
                }
            }

            switch (param)
            {
                case "all":
                    return vw;
                    break;
                case "logged":
                    if (Session["LOGADO"] == "OK")
                        return vw;
                    else
                        return View("Index");
                    break;
                case "adm":
                    if (Session["LOGADO"] == "OK" && Session["CPF"] == "ADM")
                        return vw;
                    else
                        return View("Index");
                default:
                    return View("Index");
            }



        }

        public void Visit()
        {
            visitors.atd(DateTime.Now);
        }


    }
}