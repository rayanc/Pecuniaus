
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;

namespace Pecuniaus.Utilities.PDF
{
    public class PdfHelper
    {
        public bool CreatePDF(object data, string xslFileName, string outPutPath)
        {
            var html = new XsltTransform().Transform(data, xslFileName);
            return CreatePDF(html, outPutPath);
        }

        public bool CreatePDF(string text, string outPutPath)
        {
            var returnValue = false;
            try
            {
                StringReader sr = new StringReader(text);

              //  Document pdfDoc = new Document(PageSize.LETTER, 30, 30, 40, 30);
                Document pdfDoc = new Document(PageSize.A4, 30, 30, 40, 30);

                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //PdfWriter.GetInstance(pdfDoc, new FileStream(@"d:\Temp\Test.pdf", FileMode.Create));

                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, new FileStream(outPutPath, FileMode.Create));
                //pdfWriter.PageEvent = new ITextEvents();

                pdfDoc.Open();

                htmlparser.Parse(sr);
                pdfDoc.Close();
                sr.Close();
                returnValue = true;
            }
            catch { }

            return returnValue;
        }

        public  void CombineMultiplePDFs(string[] fileNames, string outFile)
        {
            // step 1: creation of a document-object
            Document document = new Document();

            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(outFile, FileMode.Create));
            if (writer == null)
            {
                return;
            }

            // step 3: we open the document
            document.Open();

            foreach (string fileName in fileNames)
            {
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(fileName);
                reader.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }

                PRAcroForm form = reader.AcroForm;
                if (form != null)
                {
                    writer.CopyAcroForm(reader);
                }

                reader.Close();
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();
        }

    }
}
