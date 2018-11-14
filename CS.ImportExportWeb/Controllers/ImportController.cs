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
using Microsoft.EntityFrameworkCore;
using Caly.Common;

namespace CS.ImportExportAPI.Controllers
{
    public class ImportController : Controller
    {
        CadSysContext context;
        IExcelConfigurationRepo excelConfiguration;
        IRepo repo;
        IDXFRepo dXFRepo;
        IServiceBuilder serviceBuilder;


        public ImportController(CadSysContext _context, IRepo _repo, IExcelConfigurationRepo _excelConfiguration, IDXFRepo _dXFRepo, IServiceBuilder _serviceBuilder)
        {
            context = _context;
            repo = _repo;
            excelConfiguration = _excelConfiguration;
            serviceBuilder = _serviceBuilder;
            dXFRepo = _dXFRepo;

        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {


            files = Request.Form.Files.Where(x => x != null);

            IEnumerable<IFormFile> excelFiles = OrderUploadedExcelFiles(files.Where(x => x.Name.Contains(".xls", StringComparison.InvariantCultureIgnoreCase)));

            IEnumerable<IFormFile> dxfFiles = files.Where(x => x.Name.Contains(".dxf", StringComparison.InvariantCultureIgnoreCase));

            Prepare(!excelFiles.Any() && dxfFiles.Any());

            await CycleExcels(excelFiles);

            await CycleDXFs(dxfFiles);

            if (files.Any())
            {
                return new RedirectToActionResult("download", "home", null);//"~/home/download");
            }

            return Ok("Mai baga 0 fisa");
        }

        async Task CycleExcels(IEnumerable<IFormFile> fileCollection)
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

        async Task CycleDXFs(IEnumerable<IFormFile> fileCollection)
        {
            foreach (var file in fileCollection.Where(x => x.Name.Contains(".dxf", StringComparison.InvariantCultureIgnoreCase)))
            {
                if (file != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        await dXFRepo.Store(stream, file.FileName);
                    }
                }
            }
        }

        async Task CycleCentralizator(string fileName, MemoryStream stream)
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

        async Task CycleParcele(string fileName, MemoryStream stream)
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

        async Task CycleActe(string fileName, MemoryStream stream)
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

        async Task CycleProprietari(string fileName, MemoryStream stream)
        {
            var judeteAllInclussive = new ConcurrentBag<Judet>(context.Set<Judet>().Include(z => z.UATs).ThenInclude(w => w.Localitati));

            var x = await Importer.GetDTOs<OutputProprietarAdresa>(stream, fileName, new ImportConfig(), excelConfiguration);

            IAddressParser addressParser = (IAddressParser)serviceBuilder.GetService("AddressParser");
            var addressMatcher = (IMatcher)serviceBuilder.GetService("AddressMatcher");
            var matchProcessor = (IMatchProcessor)serviceBuilder.GetService("AddressMatchProcessor");

            object locker = new object();


            Parallel.ForEach(x, y =>
             {
                 var z = new Proprietar();

                 lock (locker)
                 {
                     context.Proprietari.Add(z);
                 }

                 z.FromDTO(y);
                 z.Adresa.FromDTO(y, judeteAllInclussive, addressParser, addressMatcher, matchProcessor);
             });
        }


        /// <summary>
        /// Daca s-au uploadat numai dxf-uri nu curatam baza
        /// </summary>
        /// <param name="dxfOnly">If set to <c>true</c> dxf only.</param>
        private void Prepare(bool dxfOnly = false)
        {
            if (!dxfOnly)
            {
                excelConfiguration.ClearAll();
                repo.ClearDatabase();
            }

            dXFRepo.ClearAll();
        }

        /// <summary>
        /// Centralizator vine ultimul
        /// </summary>
        /// <returns>The uploaded files.</returns>
        private IEnumerable<IFormFile> OrderUploadedExcelFiles(IEnumerable<IFormFile> files)
        {
            var fileCollection = new List<IFormFile>();

            fileCollection.AddRange(files.Where(x => !x.FileName.Contains("centraliz", StringComparison.InvariantCultureIgnoreCase)));
            fileCollection.AddRange(files.Where(x => x.FileName.Contains("centraliz", StringComparison.InvariantCultureIgnoreCase)));

            return fileCollection;
        }
    }
}
