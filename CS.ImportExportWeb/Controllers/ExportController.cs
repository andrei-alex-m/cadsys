using System;
using System.IO;
using CS.EF;
using CS.CadGen;
using Caly.Dropbox;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CS.ImportExportWeb.Models;
using Microsoft.AspNetCore.SignalR;
using CS.ImportExportWeb.Hubs;
using CS.Services;
using System.Threading.Tasks;
using System.Linq;

namespace CS.ImportExportWeb.Controllers
{
    public class ExportController : Controller
    {
        CadSysContext context;
        IExcelConfigurationRepo excelConfiguration;
        IFileRepo dXFRepo;
        IExporter cadGenExporter;
        IHubContext<MessagesHub> hubContext;
        DropBoxBase dropBoxBase;

        public ExportController(CadSysContext _context, IExcelConfigurationRepo _excelConfiguration, ServiceBuilder _serviceBuilder, IExporter _cadGenExporter, IHubContext<MessagesHub> _hubContext, DropBoxBase _dropBoxBase)
        {
            context = _context;
            excelConfiguration = _excelConfiguration;
            dXFRepo = (IFileRepo)_serviceBuilder.GetService("DXFRepo");
            cadGenExporter = _cadGenExporter;
            hubContext = _hubContext;
            dropBoxBase = _dropBoxBase;

        }

        // GET: /<controller>/
        public  void GetFile (ExportFile file)
        {

            if(file.ClassName.Contains("Proprietar", StringComparison.InvariantCultureIgnoreCase))
            {
                var wbk = Excel.Exporter.CycleProprietari(context, excelConfiguration.Get(1, "Proprietar"), "NoContext,InSet,Context");
                byte[] fileContents = null;
                using (var stream = new MemoryStream())
                {
                    wbk.Write(stream);
                    fileContents = stream.ToArray();

                    //return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ProprietariValidati.xls");
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
                    //return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ActeProprietateValidate.xls");
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
                    //return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ParceleValidate.xls");
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
                    //return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "CentralizatorValidat.xls");
                }
            }

            if (file.ClassName.Contains("dxf", StringComparison.InvariantCultureIgnoreCase))
            {
                var dxfFullPath = dXFRepo.GetFullPath(file.Display);

                var stream = DXF.Exporter.Get(dxfFullPath, cadGenExporter, (x) => hubContext.Clients.All.SendAsync("receivemessage", x));
                stream.Seek(0, SeekOrigin.Begin);
                var success = dropBoxBase.Upload(@"/CadGen/DXFResults", file.Display, stream);
            }
            //return new NotFoundResult();

        }

        public ActionResult ImportNrCadGen(ExportFile file)
        {
            if (!file.ClassName.Contains("dxf", StringComparison.InvariantCultureIgnoreCase))
            {
                return new  NotFoundResult();
            }

            var dxfFullPath = dXFRepo.GetFullPath(file.Display);
            foreach (var p in DXF.Exporter.GetPolys(dxfFullPath))
            {
                var imobil = context.Imobile.FirstOrDefault(x => x.Parcele.Any(y => y.Index == p.index));

                if (imobil == null)
                {
                    continue;
                }

                imobil.NumarCadGeneral = p.nrCadGeneral;
                imobil.SectorCadastral = p.sector;
                imobil.NumarCadastral = p.nrCadastral;

                context.Update(imobil);

            }
            context.SaveChanges();

            return new RedirectToActionResult("Download","Home", null);
        }
    }
}
