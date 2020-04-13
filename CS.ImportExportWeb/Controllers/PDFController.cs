using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caly.Dropbox;
using CS.EF;
using CS.ImportExportWeb.Models;
using Microsoft.AspNetCore.Mvc;
using CS.PDF;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CS.ImportExportWeb.Controllers
{
    public class PDFController : Controller
    {

        DropBoxBase dropBoxBase;
        CadSysContext context;

        public PDFController(CadSysContext _context,  DropBoxBase _dropBoxBase)
        {
            dropBoxBase = _dropBoxBase;
            context = _context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new PDFViewModel());
        }

        [HttpPost]
        public ActionResult Generate(PDFViewModel model)
        {
            CS.PDF.Exporter.Export(context, dropBoxBase, model.ActeIdentitateDirectory, model.ActeProprietateDirectory, model.FiseDirectory, model.OutputDirectory);
            return View("Views/Home/PDF.cshtml", model);
        }
    }
}
