using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS.Excel;
using CS.EF;
using CS.Data.Entities;
using CS.Data.Mappers;
using CS.Data.DTO.Excel;
using CS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace CS.ImportExportAPI.Controllers
{
    public class ImportController : Controller
    {
        CadSysContext context;
        IExcelConfigurationRepo excelConfiguration;
        IRepo repo;


        public ImportController(CadSysContext _context, IRepo _repo, IExcelConfigurationRepo _excelConfiguration)
        {
            context = _context;
            repo = _repo;
            excelConfiguration = _excelConfiguration;
            
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {
            Prepare();

            List<IFormFile> fileCollection = OrderUploadedFiles(Request.Form.Files);

            await CycleFiles(fileCollection);

            return Ok("Asswipe");

        }

        private async Task CycleFiles(List<IFormFile> fileCollection)
        {
            foreach (var file in fileCollection)
            {
                if (file != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        if (file.FileName.Contains("Proprietar", StringComparison.InvariantCultureIgnoreCase))
                        {
                            await CycleProprietari(file.FileName, stream);
                        }

                        if (file.FileName.Contains("Acte", StringComparison.InvariantCultureIgnoreCase))
                        {
                            await CycleActe(file.FileName, stream);
                        }

                        if (file.FileName.Contains("Parcel", StringComparison.InvariantCultureIgnoreCase))
                        {
                            await CycleParcele(file.FileName, stream);
                        }

                        if (file.FileName.Contains("Centraliz", StringComparison.InvariantCultureIgnoreCase))
                        {
                            await CycleCentralizator(file.FileName, stream);
                        }

                        await context.SaveChangesAsync();
                    }
                }
            }

            

        }

        private async Task CycleCentralizator(string fileName, MemoryStream stream)
        {
            var x = await Importer.GetGroupedDTOs<OutputInscriereDetaliu>(stream, fileName, new ImportConfig(), excelConfiguration);

            context.SaveChanges();

            x.ForEach(y =>
            {
                var z = new InscriereDetaliu();
                context.InscrieriDetaliu.Add(z);
                z.FromDTO(y, context.Proprietari, context.ActeProprietate, context.Parcele);

            });
        }

        private async Task CycleParcele(string fileName, MemoryStream stream)
        {
            var x = await Importer.GetDTOs<OutputParcela>(stream, fileName, new ImportConfig(), excelConfiguration);

            object locker = new object();
            var tarlale = new ConcurrentBag<Tarla>(context.Tarlale);

            Parallel.ForEach(x, y =>
            {
                var z = new Parcela();

                lock (locker)
                {
                    context.Parcele.Add(z);
                }

                z.FromDTO(y, tarlale);
            });
        }

        private async Task CycleActe(string fileName, MemoryStream stream)
        {
            var x = await Importer.GetDTOs<OutputActProprietate>(stream, fileName, new ImportConfig(), excelConfiguration);

            object locker = new object();
            var tipuriActe = new ConcurrentBag<TipActProprietate>(context.TipuriActProprietate);


            Parallel.ForEach(x, y =>
            {
                var z = new ActProprietate();

                lock (locker)
                {
                    context.ActeProprietate.Add(z);
                }

                z.FromDTO(y, tipuriActe);
            });
        }

        private async Task CycleProprietari(string fileName, MemoryStream stream)
        {
            var x = await Importer.GetDTOs<OutputProprietar>(stream, fileName, new ImportConfig(), excelConfiguration);

            object locker = new object();

            Parallel.ForEach(x, y =>
            {
                var z = new Proprietar();
                lock (locker)
                {
                    context.Proprietari.Add(z);
                }

                z.FromDTO(y);
            });
        }

        private void Prepare()
        {
            excelConfiguration.ClearAll();
            repo.ClearDatabase();
        }

        /// <summary>
        /// Centralizator vine ultimul
        /// </summary>
        /// <returns>The uploaded files.</returns>
        private List<IFormFile> OrderUploadedFiles(IFormFileCollection files)
        {
            var fileCollection = new List<IFormFile>();

            fileCollection.AddRange(files.Where(x => !x.FileName.Contains("centraliz", StringComparison.InvariantCultureIgnoreCase)));
            fileCollection.AddRange(files.Where(x => x.FileName.Contains("centraliz", StringComparison.InvariantCultureIgnoreCase)));

            return fileCollection;
        }
    }
}
