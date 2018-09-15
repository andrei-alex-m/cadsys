using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CS.EF;
using CS.Excel;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public  ActionResult GetFile (string file)
        {
            //var wbk = Exporter.CycleProprietari(context, excelConfiguration.Get(1, "Proprietar"), "NoContext,InSet,Context");
            //byte[] fileContents = null;
            //using (var propStream = new MemoryStream())
            //{
            //    wbk.Write(propStream);
            //    fileContents = propStream.ToArray();
            //    return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ProprietariValidati.xlsx");
            //}

            var wbk = Exporter.CycleActeProprietate(context, excelConfiguration.Get(1, "ActProprietate"), "NoContext,InSet,Context");
            byte[] fileContents = null;
            using (var propStream = new MemoryStream())
            {
                wbk.Write(propStream);
                fileContents = propStream.ToArray();
                return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, "ActeProprietateValidate.xlsx");
            }

        }
    }
}
