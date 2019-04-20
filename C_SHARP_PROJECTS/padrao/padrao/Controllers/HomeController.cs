using padrao.Models;
using PIST.API.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace padrao.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ImageResult Nota(string file)
        {
            try
            {
                ImageLib imageLib = new ImageLib();
                string caminho = Server.MapPath("/Content/" +  file);
                Image img = Image.FromFile(caminho);
                img = imageLib.resize(img, 300, 100);
                byte[] image = imageLib.ImageToByteArray(img);
                //img.Save(Server.MapPath("/Content/123.png"));
                return ControllerExtensions.Image(image, "image/png");
            }
            catch (Exception e)
            {
                throw;
                byte[] image = System.IO.File.ReadAllBytes(Server.MapPath("/Content/Images/error.jpg"));
                string contentType = "image/png";
                return ControllerExtensions.Image(image, contentType);

            }
        }
    }
}
