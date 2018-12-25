using System;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using System.Linq;
using iTextSharp.text.pdf;
using CS.EF;
using CS.Services;

namespace CS.PDF
{
    public static class Exporter
    {
        public static void AggregateAndExport(string outputFilePath, params string[] inputFilePaths)
        {
            Document document = new Document();

            using (var stream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();

                for (var i = 0; i < inputFilePaths.Count(); i++)
                {
                    var ext = Path.GetExtension(inputFilePaths[i]);

                    switch (ext.ToLower())
                    {
                        case "jpg":
                        case "jpeg":
                            using (var imageStream = new FileStream(inputFilePaths[i], FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                var image = Image.GetInstance(imageStream);
                                document.Add(image);
                            }

                            break;
                        case "pdf":
                            using (var pdfReader = new PdfReader(inputFilePaths[i]))
                            {
                                var pageCount = pdfReader.NumberOfPages;
                                for (var pageno = 0; pageno < pageCount; pageno++)
                                {
                                    var readDoc = new Document(pdfReader.GetPageSizeWithRotation(pageno));
                                    var pdfCopy = new PdfCopy(readDoc, stream);
                                    readDoc.Open();


                                }
                            }

                            break;
                    }

                }

                document.Close();
            }
        }

        public static void Gather(CadSysContext context)
        {

        }
    }
}
