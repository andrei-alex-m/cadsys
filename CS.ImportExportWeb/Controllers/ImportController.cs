using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS.Excel;
using CS.EF;
using Caly.Common;
using CS.Data.Entities;
using CS.Data.Mappers;
using CS.Data.DTO.Excel;
using System.Collections.Generic;
using System.Linq;

namespace CS.ImportExportAPI.Controllers
{
    public class ImportController:Controller
    {
        private CadSysContext context;

        public ImportController(CadSysContext _context)
        {
            context = _context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        if (file.Name.Contains("Proprietar", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var x = await Importer.GetDTOs<OutputProprietar>(stream, new ImportConfig());

                            context.DeleteAll<Proprietar>();
                            context.SaveChanges();

                            x.ForEach(y =>
                            {
                                var z = new Proprietar();
                                z.FromDTO(y);
                                context.Proprietari.Attach(z);

                            });
                            context.SaveChanges();
                        }

                        if (file.Name.Contains("Acte", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var x = await Importer.GetDTOs<OutputActProprietate>(stream, new ImportConfig());

                            context.DeleteAll<ActProprietate>();
                            context.SaveChanges();

                            x.ForEach(y =>
                            {
                                var z = new ActProprietate();
                                z.FromDTO(y, context.TipuriActProprietate.ToList());
                                context.ActeProprietate.Attach(z);

                            });
                            context.SaveChanges();
                        }

                        if (file.Name.Contains("Parcel", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var x = await Importer.GetDTOs<OutputParcela>(stream, new ImportConfig());

                            context.DeleteAll<Parcela>();
                            context.SaveChanges();

                            x.ForEach(y =>
                            {
                                var z = new Parcela();
                                z.FromDTO(y, context.Tarlale.ToList());
                                context.Parcele.Attach(z);

                            });
                            context.SaveChanges();
                        }

                        return Ok("Asswipe");
                    }

                }
            };

            return BadRequest("File required");
        }
    }
}
