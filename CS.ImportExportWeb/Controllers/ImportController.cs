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
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {
            foreach (var file in Request.Form.Files)
            {
                if (file != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        if (file.FileName.Contains("Proprietar", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var x = await Importer.GetDTOs<OutputProprietar>(stream, new ImportConfig());

                            context.DeleteAll<Proprietar>();
                            context.SaveChanges();

                            x.ForEach(y =>
                            {
                                var z = new Proprietar();
                                context.Proprietari.Add(z);
                                z.FromDTO(y);

                            });
                            context.SaveChanges();
                        }

                        if (file.FileName.Contains("Acte", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var x = await Importer.GetDTOs<OutputActProprietate>(stream, new ImportConfig());

                            context.DeleteAll<ActProprietate>();
                            context.SaveChanges();
                            x.ForEach(y =>
                                {
                                    var z = new ActProprietate();
                                    context.ActeProprietate.Add(z);
                                    z.FromDTO(y, context.TipuriActProprietate.ToList());

                                });


                            context.SaveChanges();
                        }

                        if (file.FileName.Contains("Parcel", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var x = await Importer.GetDTOs<OutputParcela>(stream, new ImportConfig());

                            context.DeleteAll<Parcela>();
                            context.SaveChanges();

                            x.ForEach(y =>
                            {
                                var z = new Parcela();
                                context.Parcele.Add(z);
                                z.FromDTO(y, context.Tarlale.ToList());
                            });
                            context.SaveChanges();
                        }

                        if (file.FileName.Contains("Centraliz", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var x = await Importer.GetGroupedDTOs<OutputInscriereDetaliu>(stream, new ImportConfig());

                            context.DeleteAll<InscriereAct>();
                            context.DeleteAll<InscriereImobil>();
                            context.DeleteAll<InscriereProprietar>();
                            context.DeleteAll<Inscriere>();
                            context.DeleteAll<InscriereDetaliu>();

                            context.SaveChanges();

                            x.ForEach(y =>
                            {
                                var z = new InscriereDetaliu();
                                context.InscrieriDetaliu.Add(z);
                                z.FromDTO(y, context.Proprietari, context.ActeProprietate, context.Parcele);
                                //context.InscrieriDetaliu.Attach(z);
                                context.SaveChanges();
                            });

                        }
                    }


                }
            };
            return Ok("Asswipe");
            //return BadRequest("File required");
        }
    }
}
