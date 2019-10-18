﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Entidade;
using padrao.Interface;
using padrao.Models;
using padrao.Negocio;
using PIST.API.Models;
using PIST.DeathByCaptcha_;

namespace padrao.Controllers
{
    public class CaptchaController : ApiController
    {
        [HttpGet]
        public string deathByCaptchal(String b64)
        {
            Capt captcha = new Capt();
            //String b642 = "Qk2KFQAAAAAAADYAAAAoAAAAVgAAABUAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////////////AADMAADMAADMAADM////////////////////////////AADMAADMAADMAADM////////////////AADMAADMAADMAADMAADMAADMAADMAADM////////AADMAADM////////////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////8AAMwAAMwAAMwAAMwAAMwAAMwAAMwAAMz///////////////////////////////8AAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////wAAzAAAzP///////////////wAAzAAAzP///////////wAAzAAAzP///////////wAAzAAAzP///////////wAAzAAAzP///////////////////////////////////////////////wAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////AADMAADM////////////////AADMAADM////////AADMAADM////////////////AADMAADM////////////////////AADMAADM////////////////////////////AADMAADMAADM////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////8AAMwAAMz///////////////8AAMwAAMz///////////8AAMwAAMz///////////////8AAMwAAMz///////////////////8AAMwAAMz///////////////////////8AAMwAAMwAAMwAAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////wAAzAAAzP///////////////wAAzAAAzP///////wAAzAAAzAAAzP///////////wAAzAAAzP///////////////////////wAAzAAAzP///////////////wAAzAAAzP///////////wAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////AADMAADMAADMAADMAADMAADM////////////////AADMAADMAADMAADMAADMAADMAADM////////////////////////////////AADMAADM////////////AADMAADM////////////////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////8AAMwAAMz///8AAMwAAMwAAMz///////////////////////////////////////8AAMwAAMz///8AAMwAAMz///////////////8AAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////wAAzAAAzP///////////////wAAzAAAzP///////wAAzAAAzP///////////////////////////////////////////////////////wAAzAAAzP///wAAzAAAzP///////////////wAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////AADMAADM////////////////AADMAADM////////AADMAADM////////////////////////////AADMAADM////////////////AADMAADM////////////AADMAADMAADMAADMAADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////8AAMwAAMz///////////////8AAMwAAMz///////////8AAMwAAMz///////////8AAMwAAMz///////8AAMwAAMwAAMz///////8AAMwAAMwAAMz///////////////////8AAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////////AADMAADMAADMAADM////////////////////////AADMAADMAADMAADM////////////////////////////AADMAADMAADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/";
            string result = captcha.resolveCaptcha(b64);
            return result;
        }
        public async Task<Retorno> Upload()
        {
            Retorno ret = new Retorno();
            FileUpload FileUpload = new FileUpload();
            List<string> newNames = new List<string>();
            ImageLib ImageLib = new ImageLib();
            ret.Status = true;
            ret.Message = "Arquivos Carregados com Sucesso!";

            newNames.Add("file.jpg");
            await FileUpload.Upload(Request, "~/documents", newNames);

            string b64 = ImageLib.imageToB64(HttpContext.Current.Server.MapPath("~/documents") + "\\file.jpg");


            Capt captcha = new Capt();
            //String b64 = File.ReadAllText(HttpContext.Current.Server.MapPath("~/documents") + "\\file.txt");
            string result = captcha.resolveCaptcha(b64);
            ret.Response = result;
            return ret;

        }
        //[HttpGet]
        //public ImageResult converter(string b644)
        //{
        //    String b64 = "Qk2KFQAAAAAAADYAAAAoAAAAVgAAABUAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////////////AADMAADMAADMAADM////////////////////////////AADMAADMAADMAADM////////////////AADMAADMAADMAADMAADMAADMAADMAADM////////AADMAADM////////////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////8AAMwAAMwAAMwAAMwAAMwAAMwAAMwAAMz///////////////////////////////8AAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////wAAzAAAzP///////////////wAAzAAAzP///////////wAAzAAAzP///////////wAAzAAAzP///////////wAAzAAAzP///////////////////////////////////////////////wAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////AADMAADM////////////////AADMAADM////////AADMAADM////////////////AADMAADM////////////////////AADMAADM////////////////////////////AADMAADMAADM////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////8AAMwAAMz///////////////8AAMwAAMz///////////8AAMwAAMz///////////////8AAMwAAMz///////////////////8AAMwAAMz///////////////////////8AAMwAAMwAAMwAAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////wAAzAAAzP///////////////wAAzAAAzP///////wAAzAAAzAAAzP///////////wAAzAAAzP///////////////////////wAAzAAAzP///////////////wAAzAAAzP///////////wAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////AADMAADMAADMAADMAADMAADM////////////////AADMAADMAADMAADMAADMAADMAADM////////////////////////////////AADMAADM////////////AADMAADM////////////////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////8AAMwAAMz///8AAMwAAMwAAMz///////////////////////////////////////8AAMwAAMz///8AAMwAAMz///////////////8AAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////wAAzAAAzP///////////////wAAzAAAzP///////wAAzAAAzP///////////////////////////////////////////////////////wAAzAAAzP///wAAzAAAzP///////////////wAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////AADMAADM////////////////AADMAADM////////AADMAADM////////////////////////////AADMAADM////////////////AADMAADM////////////AADMAADMAADMAADMAADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////8AAMwAAMz///////////////8AAMwAAMz///////////8AAMwAAMz///////////8AAMwAAMz///////8AAMwAAMwAAMz///////8AAMwAAMwAAMz///////////////////8AAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////////AADMAADMAADMAADM////////////////////////AADMAADMAADMAADM////////////////////////////AADMAADMAADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/";
        //    byte[] image = Convert.FromBase64String(b64);
        //    using (var ms = new MemoryStream(image))
        //    {

        //        string contentType = "image/jpg";
        //        return ControllerExtensions.Image(image, contentType);
        //    }
        //}

    }
}