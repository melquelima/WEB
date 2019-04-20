using Entidade;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Rainhadascamisetas.Models
{
    public static class GeradorNota
    {
        public static string GerarNota(List<Pedido> produtos, User cliente)
        {
            string os = produtos[0].OS;
            Document doc = new Document(PageSize.A4.Rotate(), 0f, 0f, 0f, 0f);//criando e estipulando o tipo da folha usada
            doc.SetMargins(-1, -1, -1, 80);//estibulando o espaçamento das margens que queremos
            doc.AddCreationDate();//adicionando as configuracoes
                                  //caminho onde sera criado o pdf + nome desejado
                                  //OBS: o nome sempre deve ser terminado com .pdf
            string caminho = HttpContext.Current.Server.MapPath("/Content/images/Nota") + "/" + os +".pdf";

            if (File.Exists(caminho))
            {
                File.Delete(caminho);

            }
            
            //criando o arquivo pdf embranco, passando como parametro a variavel doc criada acima e a variavel caminho 
            //tambem criada acima.
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
            doc.SetMargins(0f, 0f, 0f, 0f);
            doc.Open();

            Font font = FontFactory.GetFont("/fonts/Sansation_Regular.ttf",
            BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 0.8f, Font.NORMAL, BaseColor.BLACK);


            var arial_italic_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialbi.ttf");
            var arial_italic_base = BaseFont.CreateFont(arial_italic_path, BaseFont.IDENTITY_H, false);

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetTextMatrix(122, 458);
            //cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(arial_italic_base, 15);
            cb.ShowText(produtos[0].DATA.ToString("dd/MM/yyyy"));
            cb.EndText();

            cb.BeginText();
            cb.SetTextMatrix(150, 425);
            //cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(arial_italic_base, 15);
            cb.ShowText(os.Substring(os.Length - 3));
            cb.EndText();

            cb.BeginText();
            cb.SetTextMatrix(640, 440);
            //cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(arial_italic_base, 30);
            cb.ShowText(os);
            cb.EndText();

            cb.BeginText();
            cb.SetTextMatrix(640, 440);
            //cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(arial_italic_base, 30);
            cb.ShowText(os);
            cb.EndText();

            cb.BeginText();
            cb.SetTextMatrix(300, 428);
            //cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(arial_italic_base, 12);
            cb.ShowText(cliente.Nome);
            cb.EndText();

            cb.BeginText();
            cb.SetTextMatrix(340, 460);
            //cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(arial_italic_base, 14);
            cb.ShowText(Convert.ToUInt64(cliente.CPF).ToString(@"000\.000\.000\-00"));
            cb.EndText();



            Image header = default(iTextSharp.text.Image);
            header = Image.GetInstance(HttpContext.Current.Server.MapPath("/Content/images/header.bmp"));
            header.SetAbsolutePosition(-1, 400);
            header.ScaleToFit(PageSize.A4.Height, PageSize.A4.Width);

            doc.Add(header);


            //-----------------------------------------------------------------------criando uma string vazia
            string dados = "";

            //criando a variavel para paragrafo
            Paragraph paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 14));
            //etipulando o alinhamneto
            paragrafo.Alignment = Element.ALIGN_LEFT;
            //Alinhamento Justificado
            //adicioando texto
            paragrafo.Leading = -1;
            paragrafo.Add("a");
            //acidionado paragrafo ao documento
            doc.Add(paragrafo);
            //fechando documento para que seja salva as alteraçoes.

            //------------------------------------------------------------------------------------TABELA

            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfPTable myTable = new PdfPTable(7);
            // Set the overall width of the table to render
            myTable.TotalWidth = PageSize.A4.Height;
            myTable.HorizontalAlignment = 0;
            myTable.SpacingBefore = 196f;
            //myTable.SpacingAfter = 0;

            float[] sglTblHdWidths = new float[7];
            sglTblHdWidths[0] = 0.139f;
            sglTblHdWidths[1] = 0.2033f;
            sglTblHdWidths[2] = 0.165f;
            sglTblHdWidths[3] = 0.088f;
            sglTblHdWidths[4] = 0.132f;
            sglTblHdWidths[5] = 0.133f;
            sglTblHdWidths[6] = 0.141f;

            // The following two settings are also important to set the overall width of the table to render
            myTable.SetWidths(sglTblHdWidths);
            myTable.LockedWidth = true;
            // We create and set the first column here to span our three rows.
            // Note that since we rotated the cell 90 degrees clockwise,
            // the vertical alignment: Top actually renders as the horizontal alignment: Right
            // and the horizontal alignment: Center renders as the vertical alignment for the middle of the cell

            double total_ = 0;
            int qtd = 0;

            foreach (Pedido it in produtos)
            {
                PdfPCell Tecido = new PdfPCell(new Phrase(it.MALHA, fntTableFont));
                PdfPCell Produto = new PdfPCell(new Phrase(it.PRODUTO, fntTableFont));
                PdfPCell Cor = new PdfPCell(new Phrase(it.COR, fntTableFont));
                PdfPCell Tamanho = new PdfPCell(new Phrase(it.TAMANHO, fntTableFont));
                PdfPCell Quantidade = new PdfPCell(new Phrase(it.QUANTIDADE.ToString(), fntTableFont));
                PdfPCell Unidade = new PdfPCell(new Phrase(it.VALOR.ToString("c2"), fntTableFont));
                PdfPCell Total = new PdfPCell(new Phrase((it.VALOR * it.QUANTIDADE).ToString("c2"), fntTableFont));
                Tecido.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                Produto.HorizontalAlignment = 1;
                Cor.HorizontalAlignment = 1;
                Tamanho.HorizontalAlignment = 1;
                Quantidade.HorizontalAlignment = 1;
                Unidade.HorizontalAlignment = 1;
                Total.HorizontalAlignment = 1;

                myTable.AddCell(Tecido);
                myTable.AddCell(Produto);
                myTable.AddCell(Cor);
                myTable.AddCell(Tamanho);
                myTable.AddCell(Unidade);
                myTable.AddCell(Quantidade);
                myTable.AddCell(Total);
                total_ = total_ + (it.VALOR * it.QUANTIDADE);
                qtd = qtd + it.QUANTIDADE;
            }


            PdfPCell cell = new PdfPCell(new Phrase("TOTAIS"));
            cell.Colspan = 4;
            cell.BackgroundColor = new BaseColor(217, 217, 217);
            cell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
            myTable.AddCell(cell);

            PdfPCell vazia = new PdfPCell(new Phrase(""));
            vazia.BackgroundColor = new BaseColor(217, 217, 217);
            myTable.AddCell(vazia);

            PdfPCell Quantidad = new PdfPCell(new Phrase(qtd.ToString()));
            Quantidad.BackgroundColor = new BaseColor(217, 217, 217);
            Quantidad.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            myTable.AddCell(Quantidad);

            PdfPCell cell2 = new PdfPCell(new Phrase(total_.ToString("c2")));
            cell2.BackgroundColor = new BaseColor(217, 217, 217);
            cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell2.VerticalAlignment = 0;
            myTable.AddCell(cell2);

            doc.Add(myTable);

            doc.Close();
            return caminho;
        }

        public static void pdf_image(String caminho)
        {
            String caminho2 = caminho.Split(new string[] { ".pdf" }, StringSplitOptions.None)[0] + "_temp.jpg";
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(caminho);
            if (f.PageCount > 0)
            {
                f.ImageOptions.Dpi = 300;
                f.ImageOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;

                f.ToImage(caminho2, 1);
                f.ClosePdf();
                //for (int page = 1; page <= f.PageCount; page++)
                //{
                //    f.ToImage(@"C:\Users\memel\source\repos\ConsoleApp1\ConsoleApp1\bin\Debug\page" + page + ".jpg", page);
                //}
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(caminho2);
                System.Drawing.Bitmap croppedBitmap = CropBitmap(bitmap, 0, 210, 3508, 2199);
                
                croppedBitmap.Save(caminho2.Split(new string[] { "_temp" }, StringSplitOptions.None)[0] + ".jpg");
                bitmap.Dispose();

                File.Delete(caminho);
                File.Delete(caminho2);
            }

        }
        public static System.Drawing.Bitmap CropBitmap(System.Drawing.Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(cropX, cropY, cropWidth, cropHeight);
            System.Drawing.Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }
    }
  

}