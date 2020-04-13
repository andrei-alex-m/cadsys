using System;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using System.Linq;
using iTextSharp.text.pdf;
using CS.EF;
using CS.Services;
using System.Collections.Generic;
using Caly.Dropbox;
using System.Threading.Tasks;

namespace CS.PDF
{
    public static class Exporter
    {
        private static string sourceLocalDir = "SourceDir";
        private static string outputLocalDir = "OutDir";
        private static DropBoxBase dropBoxBase;

        public static void Export(CadSysContext context, DropBoxBase dropBoxClient, string ai, string ap, string f, string o)
        {

            dropBoxClient.Delete(o);
            dropBoxClient.CreateFolder(o);
            CreateOutputDir();

            var fise = dropBoxClient.ListFolder(f, false, true, ".pdf");

            //Parallel.ForEach(fise, new ParallelOptions { MaxDegreeOfParallelism = 4 }, x =>
            fise.ToList().ForEach(x =>
            {
                Pizdificator pizd;
                pizd = GatherFromContext(context, Path.GetFileNameWithoutExtension(x));

                var files = GatherFromFiles(pizd, dropBoxClient, ai, ap, f);

                AggregateAndExport(dropBoxClient, $"{outputLocalDir}/{Path.GetFileName(x)}", o, files);

            });
        }

        public static void AggregateAndExport(DropBoxBase dropBoxClient, string outputLocalFile, string dropBoxOutputFolder, params string[] inputFilePaths)
        {


            Document document = new Document();
            //var stream = new FileStream(outputLocalFile, FileMode.Create);
            var writer = PdfWriter.GetInstance(document, new FileStream(outputLocalFile, FileMode.Create));

            var nrCadGeneral = "";// Path.GetFileNameWithoutExtension(outputLocalFile);
            //PdfCopy pdfCopy = new PdfCopy(document, new FileStream(outputLocalFile, FileMode.Create));
            document.Open();
            PdfContentByte pcb = writer.DirectContentUnder;

            for (var i = 0; i < inputFilePaths.Count(); i++)
            {

                CreateSourceDir(nrCadGeneral);

                var copied = dropBoxClient.Download(inputFilePaths[i], $"{sourceLocalDir}{nrCadGeneral}");

                if (!copied)
                    continue;

                var sourceFile = $"{sourceLocalDir}{nrCadGeneral}/{Path.GetFileName(inputFilePaths[i])}";

                var ext = Path.GetExtension(inputFilePaths[i]);

                switch (ext.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":


                        var image = Image.GetInstance(sourceFile);

                        image.ScaleToFit(PageSize.A4);
                        image.SetAbsolutePosition(0, 0);

                        //pdfCopy.AddPage(PageSize.A4,0);
                        //pdfCopy.AddDirectImageSimple(image);

                        //pdfCopy.DirectContent.AddImage(image);

                        document.SetPageSize(PageSize.A4);
                        document.NewPage();
                        document.Add(image);



                        break;
                    case ".pdf":

                        var reader = new PdfReader(sourceFile);
                        for (var p = 1; p <= reader.NumberOfPages; p++)
                        {
                            document.SetPageSize(reader.GetPageSizeWithRotation(p));
                            document.NewPage();
                            PdfImportedPage page = writer.GetImportedPage(reader, p);
                            pcb.AddTemplate(page, 0, 0);

                        }
                        ////pdfCopy.AddDocument(new PdfReader(sourceFile));
                        //reader.Close();

                        break;
                }

                DeleteSourceDir(nrCadGeneral);

            }

            document.Close();
            writer.Close();


            Task.Factory.StartNew(() => dropBoxClient.Upload(dropBoxOutputFolder, Path.GetFileName(inputFilePaths[0]), outputLocalFile));

        }

        public static Pizdificator GatherFromContext(CadSysContext context, string nrCadGeneral)
        {
            if (string.IsNullOrEmpty(nrCadGeneral))
                return null;

            var imobil = context.Imobile.FirstOrDefault(x => x.NumarCadGeneral == nrCadGeneral);

            if (imobil == null)
                return new Pizdificator() { NrCadGeneral = nrCadGeneral };

            var result = new Pizdificator();

            context.Entry(imobil).Collection(x => x.InscrieriDetaliu).Load();
            //context.Entry(imobil.Parcele.FirstOrDefault()).Reference(x => x.Tarla).LoadAsync();

            foreach (var iD in imobil.InscrieriDetaliu)
            {
                context.Entry(iD).Collection(x => x.InscrieriActe).Load();
                //context.Entry(iD).Collection(x => x.InscrieriImobile).LoadAsync();
                context.Entry(iD).Collection(x => x.InscrieriProprietari).Load();

                foreach (var ia in iD.InscrieriActe)
                {
                    context.Entry(ia).Reference(x => x.ActProprietate).Load();
                }

                foreach (var ip in iD.InscrieriProprietari)
                {
                    context.Entry(ip).Reference(x => x.Proprietar).Load();
                }
            }

            result.IndecsiProprietari = imobil.InscrieriDetaliu.SelectMany(x => x.InscrieriProprietari).Select(y => y.Proprietar).Select(x => x.Index).Distinct().ToList();
            result.IndecsiActe = imobil.InscrieriDetaliu.Where(id => id.InscrieriProprietari.Count > 0).SelectMany(x => x.InscrieriActe).Select(y => y.ActProprietate).Select(x => x.Index).Distinct().ToList();
            result.Sector = imobil.SectorCadastral;
            result.NrCadGeneral = nrCadGeneral;

            return result;
        }

        public static string[] GatherFromFiles(Pizdificator pizdificator, DropBoxBase dropBoxClient, string ai, string ap, string f)
        {
            var result = new List<string>();
            result.Add($"{f}/{pizdificator.NrCadGeneral}.pdf");



            pizdificator.IndecsiProprietari?.ForEach(x =>
            {
                if (dropBoxClient.FolderExists($"{ai}/{x}"))
                {
                    var files = dropBoxClient.ListFolder($"{ai}/{x}/", false, true).OrderBy(y => y);

                    files.ToList().ForEach(y => result.Add($"{ai}/{x}/{y}"));
                }
            });


            pizdificator.IndecsiActe?.ForEach(x =>
            {
                if (dropBoxClient.FolderExists($"{ap}/{x}"))
                {
                    var files = dropBoxClient.ListFolder($"{ap}/{x}/", false, true).OrderBy(y => y);

                    files.ToList().ForEach(y => result.Add($"{ap}/{x}/{y}"));
                }

                if (dropBoxClient.FileExists($"{ap}/{x}.pdf"))
                    result.Add($"{ap}/{x}.pdf");

            });

            return result.ToArray();

        }

        private static void CopyToLocalSource(params string[] inputFilePaths)
        {

        }

        //public static void Match ()

        private static void CreateSourceDir(string nrCadGeneral)
        {

            if (Directory.Exists($"{sourceLocalDir}{nrCadGeneral}"))
            {
                Directory.Delete($"{sourceLocalDir}{nrCadGeneral}", true);
            }

            Directory.CreateDirectory($"{sourceLocalDir}{nrCadGeneral}");
        }

        private static void DeleteSourceDir(string nrCadGeneral)
        {
            if (Directory.Exists($"{sourceLocalDir}{nrCadGeneral}"))
            {
                Directory.Delete($"{sourceLocalDir}{nrCadGeneral}", true);
            }
        }


        private static void CreateOutputDir()
        {
            if (Directory.Exists(outputLocalDir))
            {
                Directory.Delete(outputLocalDir, true);
            }
            Directory.CreateDirectory(outputLocalDir);
        }

    }

    public class Pizdificator
    {
        public string NrCadGeneral { get; set; }
        public string Sector { get; set; }
        public List<int> IndecsiActe { get; set; }
        public List<int> IndecsiProprietari { get; set; }
    }

}
