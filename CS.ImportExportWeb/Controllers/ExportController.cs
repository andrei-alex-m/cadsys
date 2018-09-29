using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CS.EF;
using CS.Excel;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.ImportExportWeb.Controllers
{
    public class ExportController : Controller
    {
        CadSysContext context;
        IExcelConfigurationRepo excelConfiguration;

        public ExportController(CadSysContext _context, IExcelConfigurationRepo _excelConfiguration)
        {
            context = _context;
            excelConfiguration = _excelConfiguration;
        }

        // GET: /<controller>/
        public  IActionResult GetFile (string file)
        {

            if(file.Contains("Proprietar", StringComparison.InvariantCultureIgnoreCase))
            {
                var wbk = Exporter.CycleProprietari(context, excelConfiguration.Get(1, "Proprietar"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var stream = new MemoryStream())
                {
                    wbk.Write(stream);
                    fileContents = stream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ProprietariValidati.xlsx");
                }
            }

            if (file.Contains("ActProprietate", StringComparison.InvariantCultureIgnoreCase))
            {

                var wbk = Exporter.CycleActeProprietate(context, excelConfiguration.Get(1, "ActProprietate"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var stream = new MemoryStream())
                {
                    wbk.Write(stream);
                    fileContents = stream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ActeProprietateValidate.xlsx");
                }
            }

            if (file.Contains("Parcel", StringComparison.InvariantCultureIgnoreCase))
            {

                var wbk = Exporter.CycleParcele(context, excelConfiguration.Get(1, "Parcela"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var stream = new MemoryStream())
                {
                    wbk.Write(stream);
                    fileContents = stream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ParceleValidate.xlsx");
                }
            }

            if (file.Contains("Inscrie", StringComparison.InvariantCultureIgnoreCase))
            {

                var wbk = Exporter.CycleInscrieri(context, excelConfiguration.Get(1, "Inscriere"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var propStream = new MemoryStream())
                {
                    wbk.Write(propStream);
                    fileContents = propStream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "CentralizatorValidat.xlsx");
                }
            }

            return new OkResult();

        }
    }
}
