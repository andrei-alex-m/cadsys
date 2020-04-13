using System;
using System.Collections.Generic;
using System.Linq;
using Caly.Dropbox;
using CS.EF;
using CS.ImportExportWeb.Models;
using CS.Services;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CS.ImportExportWeb.Controllers
{
    public class ProcessController : Controller
    {
        string excelIn = "/CadGen/Exceluri";
        string excelOut = "/CadGen/Exceluri/Validari";
        string dxfIn = "/CadGen/Teste/DXF";
        string dxfOut = "/CadGen/Teste";
        DropBoxBase dropBox;
        CadSysContext context;
        IExcelConfigurationRepo excelConfiguration;
        IRepo repo;
        IFileRepo dxfRepo;
        IFileRepo excelRepo;

        public ProcessController(DropBoxBase _dropBox, CadSysContext _context, IRepo _repo, IExcelConfigurationRepo _excelConfiguration, ServiceBuilder serviceBuilder)
        {
            dropBox = _dropBox;
            context = _context;
            repo = _repo;
            excelConfiguration = _excelConfiguration;
            dxfRepo = (IFileRepo)serviceBuilder.GetService("DXFRepo");
            excelRepo = (IFileRepo)serviceBuilder.GetService("ExcelRepo");
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var pvm = new ProcessViewModel();

            pvm.ExcelFiles = dropBox.ListFolder(excelIn, false, true, ".xls");
            pvm.DXFFiles = dropBox.ListFolder(excelIn, false, true, ".dxf");

            return View("~/Views/Home/Process.cshtml", pvm);
        }

        public IActionResult ProcessAllExcels()
        {
            var excels = dropBox.ListFolder(excelIn, false, true, ".xls");

            excelRepo.ClearAll();
            excels.ToList().ForEach(x => dropBox.Download(excelIn,x,excelRepo.DirPath,x));

            var orderedExcels = OrderUploadedExcelFiles(excels);

            return Ok();
        }

        public IActionResult ProcessDXF(string filename)
        {
            return Ok();
        }

        public IActionResult ValidateExcel(string filename)
        {
            return Ok();
        }

        /// <summary>
        /// Centralizator vine ultimul
        /// </summary>
        /// <returns>The uploaded files.</returns>
        private IEnumerable<string> OrderUploadedExcelFiles(IEnumerable<string> files)
        {
            var fileCollection = new List<string>();

            fileCollection.AddRange(files.Where(x => !x.Contains("centraliz", StringComparison.InvariantCultureIgnoreCase)));
            fileCollection.AddRange(files.Where(x => x.Contains("centraliz", StringComparison.InvariantCultureIgnoreCase)));

            return fileCollection;
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

            dxfRepo.ClearAll();
        }

    }
}
