﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Data;
using DeathByCaptcha;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //teste1();
            //teste2();
            Image img = Image.FromFile(@"IMG_7980.png");
            img = resize(img,100,100);
            img.Save("teste.bmp");
            //Console.ReadKey();

        }

        static Image resize(Image image,int w, int h)
        {
            Bitmap bmp = new Bitmap(w,h);
            Graphics graphic = Graphics.FromImage(bmp);
            graphic.DrawImage(image, 0, 0, w, h);
            graphic.Dispose();
            return bmp;
        }

        static void teste2()
        {
            String b64 = "Qk2KFQAAAAAAADYAAAAoAAAAVgAAABUAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////////////AADMAADMAADMAADM////////////////////////////AADMAADMAADMAADM////////////////AADMAADMAADMAADMAADMAADMAADMAADM////////AADMAADM////////////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////8AAMwAAMwAAMwAAMwAAMwAAMwAAMwAAMz///////////////////////////////8AAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////////wAAzAAAzP///////////////wAAzAAAzP///////////wAAzAAAzP///////////wAAzAAAzP///////////wAAzAAAzP///////////////////////////////////////////////wAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wAA////////////////AADMAADM////////////////AADMAADM////////AADMAADM////////////////AADMAADM////////////////////AADMAADM////////////////////////////AADMAADMAADM////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAD///////////////8AAMwAAMz///////////////8AAMwAAMz///////////8AAMwAAMz///////////////8AAMwAAMz///////////////////8AAMwAAMz///////////////////////8AAMwAAMwAAMwAAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AAP///////////////wAAzAAAzP///////////////wAAzAAAzP///////wAAzAAAzAAAzP///////////wAAzAAAzP///////////////////////wAAzAAAzP///////////////wAAzAAAzP///////////wAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////AADMAADMAADMAADMAADMAADM////////////////AADMAADMAADMAADMAADMAADMAADM////////////////////////////////AADMAADM////////////AADMAADM////////////////AADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////////////8AAMwAAMwAAMwAAMwAAMwAAMz///////////8AAMwAAMz///8AAMwAAMwAAMz///////////////////////////////////////8AAMwAAMz///8AAMwAAMz///////////////8AAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////wAAzAAAzP///////////////wAAzAAAzP///////wAAzAAAzP///////////////////////////////////////////////////////wAAzAAAzP///wAAzAAAzP///////////////wAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////AADMAADM////////////////AADMAADM////////AADMAADM////////////////////////////AADMAADM////////////////AADMAADM////////////AADMAADMAADMAADMAADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////8AAMwAAMz///////////////8AAMwAAMz///////////8AAMwAAMz///////////8AAMwAAMz///////8AAMwAAMwAAMz///////8AAMwAAMwAAMz///////////////////8AAMwAAMwAAMwAAMz///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////wAAzAAAzAAAzAAAzAAAzAAAzP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/////////////////////////AADMAADMAADMAADM////////////////////////AADMAADMAADMAADM////////////////////////////AADMAADMAADMAADM////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////GP////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8Y/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////xj/";
            var bytes = Convert.FromBase64String(b64);


            Client client = (Client)new SocketClient("siteseeing", "siteseeing");

            // Put your CAPTCHA file name, stream, or vector of bytes,
            // and desired timeout (in seconds) here:
            Captcha captcha = client.Decode(bytes, 3000);
            if (captcha.Solved && captcha.Correct)
            {
                Console.WriteLine("CAPTCHA {0}: {1}", captcha.Id, captcha.Text);

                // Report the CAPTCHA if solved incorrectly.
                // Make sure the CAPTCHA was in fact incorrectly solved!

            }
        }
        static void teste1()
        {
            using (FileStream stream = File.Open(@"captcha.bmp", FileMode.Open))
            {
                Bitmap originalBMP = new Bitmap(stream);
            }

            string strPhoto = (@"captcha.bmp");
            FileStream fs = new FileStream(strPhoto, FileMode.Open, FileAccess.Read);

            Byte[] imgByte = new byte[fs.Length];

            fs.Read(imgByte, 0, System.Convert.ToInt32(fs.Length));

            fs.Close();


            // Put your DBC credentials here.
            // Use HttpClient class if you want to use HTTP API.



            Client client = (Client)new SocketClient("siteseeing", "siteseeing");

            // Put your CAPTCHA file name, stream, or vector of bytes,
            // and desired timeout (in seconds) here:
            Captcha captcha = client.Decode(imgByte, 3000);
            if (captcha.Solved && captcha.Correct)
            {
                
                Console.WriteLine("CAPTCHA {0}: {1}", captcha.Id, captcha.Text);

                // Report the CAPTCHA if solved incorrectly.
                // Make sure the CAPTCHA was in fact incorrectly solved!

            }
            Console.ReadKey();
        }
    }
}

