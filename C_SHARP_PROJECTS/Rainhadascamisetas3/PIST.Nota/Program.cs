using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ESSAS SAO AS BIBLIOTECAS QUE DEVEREMOS ADICIONAR EM NOSSO PROJETO
using System.IO;// A BIBLIOTECA DE ENTRADA E SAIDA DE ARQUIVOS

using iTextSharp;//E A BIBLIOTECA ITEXTSHARP E SUAS EXTENÇÕES
using iTextSharp.text;//ESTENSAO 1 (TEXT)
using iTextSharp.text.pdf;//ESTENSAO 2 (PDF)
using PIST.Nota;
using System.Reflection;

namespace PIST.Nota
{
    public class Program
    {
        static void Main(string[] args)
        {
            Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
            doc.SetMargins(-1, -1, -1, 80);//estibulando o espaçamento das margens que queremos
            doc.AddCreationDate();//adicionando as configuracoes

            //caminho onde sera criado o pdf + nome desejado
            //OBS: o nome sempre deve ser terminado com .pdf
            string caminho = @"c:\users\memel\source\repos\ConsoleApp1\ConsoleApp1\bin\Debug\" + "xxx.pdf";

            //criando o arquivo pdf embranco, passando como parametro a variavel doc criada acima e a variavel caminho 
            //tambem criada acima.
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            Font font = FontFactory.GetFont("/fonts/Sansation_Regular.ttf",
            BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 0.8f, Font.NORMAL, BaseColor.BLACK);


            var arial_italic_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialbi.ttf");
            var arial_italic_base = BaseFont.CreateFont(arial_italic_path, BaseFont.IDENTITY_H, false);

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetTextMatrix(640, 490);
            //cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(arial_italic_base, 30);
            cb.ShowText("20180422001");
            cb.EndText();


            Image header = default(iTextSharp.text.Image);
            Image total = default(iTextSharp.text.Image);
            header = Image.GetInstance("header.bmp");
            total = Image.GetInstance("total.png");
            header.SetAbsolutePosition(-1, 450);
            header.ScaleToFit(PageSize.A4.Height, PageSize.A4.Width);
            total.ScaleToFit(PageSize.A4.Height, PageSize.A4.Width);

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
            myTable.SpacingBefore = 147.2f;
            //myTable.SpacingAfter = 0;

            float[] sglTblHdWidths = new float[7];
            sglTblHdWidths[0] = 0.139f;
            sglTblHdWidths[1] = 0.2033f;
            sglTblHdWidths[2] = 0.165f;
            sglTblHdWidths[3] = 0.088f;
            sglTblHdWidths[4] = 0.132f;
            sglTblHdWidths[5] = 0.133f;
            sglTblHdWidths[6] = 0.1397f;

            // The following two settings are also important to set the overall width of the table to render
            myTable.SetWidths(sglTblHdWidths);
            myTable.LockedWidth = true;
            // We create and set the first column here to span our three rows.
            // Note that since we rotated the cell 90 degrees clockwise,
            // the vertical alignment: Top actually renders as the horizontal alignment: Right
            // and the horizontal alignment: Center renders as the vertical alignment for the middle of the cell

            List<linha> produtos = new List<linha>();
            linha x = new linha();
            x.PRODUTO = "blusa";
            x.TECIDO = "Poliester";
            x.COR = "Branco";
            x.TAMANHO = "P";
            x.QUANTIDADE = 2;
            x.UNIDADE = 25;
            x.TOTAL = 2.3f;
            produtos.Add(x);
            produtos.Add(x);
            produtos.Add(x);

            double total_ = 0;
            int qtd = 0;

            foreach (linha it in produtos)
            {
                PdfPCell Tecido = new PdfPCell(new Phrase(it.TECIDO, fntTableFont));
                PdfPCell Produto = new PdfPCell(new Phrase(it.PRODUTO, fntTableFont));
                PdfPCell Cor = new PdfPCell(new Phrase(it.COR, fntTableFont));
                PdfPCell Tamanho = new PdfPCell(new Phrase(it.TAMANHO, fntTableFont));
                PdfPCell Quantidade = new PdfPCell(new Phrase(it.QUANTIDADE.ToString(), fntTableFont));
                PdfPCell Unidade = new PdfPCell(new Phrase(it.UNIDADE.ToString("c2"), fntTableFont));
                PdfPCell Total = new PdfPCell(new Phrase(it.TOTAL.ToString("c2"), fntTableFont));
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
                total_ = total_ + it.TOTAL;
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
        }
    }
}