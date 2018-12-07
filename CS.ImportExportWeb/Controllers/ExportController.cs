using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CS.EF;
using CS.Excel;
using CS.CadGen;
using CS.DXF;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CS.ImportExportWeb.Models;
using Microsoft.AspNetCore.SignalR;
using CS.ImportExportWeb.Hubs;

namespace CS.ImportExportWeb.Controllers
{
    public class ExportController : Controller
    {
        CadSysContext context;
        IExcelConfigurationRepo excelConfiguration;
        IDXFRepo dXFRepo;
        IServiceBuilder serviceBuilder;
        IExporter cadGenExporter;
        IHubContext<MessagesHub> hubContext;

        public ExportController(CadSysContext _context, IExcelConfigurationRepo _excelConfiguration, IServiceBuilder _serviceBuilder, IDXFRepo _dxfRepo, IExporter _cadGenExporter, IHubContext<MessagesHub> _hubContext)
        {
            context = _context;
            excelConfiguration = _excelConfiguration;
            serviceBuilder = _serviceBuilder;
            dXFRepo = _dxfRepo;
            cadGenExporter = _cadGenExporter;
            hubContext = _hubContext;

        }

        // GET: /<controller>/
        public  IActionResult GetFile (ExportFile file)
        {

            if(file.ClassName.Contains("Proprietar", StringComparison.InvariantCultureIgnoreCase))
            {
                var wbk = Excel.Exporter.CycleProprietari(context, excelConfiguration.Get(1, "Proprietar"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var stream = new MemoryStream())
                {
                    wbk.Write(stream);
                    fileContents = stream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ProprietariValidati.xls");
                }
            }

            if (file.ClassName.Contains("ActProprietate", StringComparison.InvariantCultureIgnoreCase))
            {

                var wbk = Excel.Exporter.CycleActeProprietate(context, excelConfiguration.Get(1, "ActProprietate"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var stream = new MemoryStream())
                {
                    wbk.Write(stream);
                    fileContents = stream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ActeProprietateValidate.xls");
                }
            }

            if (file.ClassName.Contains("Parcel", StringComparison.InvariantCultureIgnoreCase))
            {

                var wbk = Excel.Exporter.CycleParcele(context, excelConfiguration.Get(1, "Parcela"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var stream = new MemoryStream())
                {
                    wbk.Write(stream);
                    fileContents = stream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ParceleValidate.xls");
                }
            }

            if (file.ClassName.Contains("Inscrie", StringComparison.InvariantCultureIgnoreCase))
            {

                var wbk = Excel.Exporter.CycleInscrieri(context, excelConfiguration.Get(1, "Inscriere"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var propStream = new MemoryStream())
                {
                    wbk.Write(propStream);
                    fileContents = propStream.ToArray();
                    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "CentralizatorValidat.xls");
                }
            }

            if (file.ClassName.Contains("dxf", StringComparison.InvariantCultureIgnoreCase))
            {
                var dxfFullPath = dXFRepo.GetFullPath(file.Display);

                var stream = DXF.Exporter.Get(dxfFullPath, cadGenExporter, (x) => hubContext.Clients.All.SendAsync("receivemessage", x));

                return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, file.Display);
            }

            return new NotFoundResult();

        }
    }
}
