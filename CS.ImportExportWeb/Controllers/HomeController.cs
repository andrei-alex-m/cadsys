using System.Diagnostics;
using System.Linq;
using CS.ImportExportWeb.Models;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.ImportExportWeb.Controllers
{
    public class HomeController : Controller
    {
        IExcelConfigurationRepo excelConfig;
        IDXFRepo dXFRepo;

        public HomeController(IExcelConfigurationRepo _excelConfig, IDXFRepo _dXFRepo)
        {
            excelConfig = _excelConfig;
            dXFRepo = _dXFRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Download()
        {
            var evm = new ExportViewModel
            {
                Files = excelConfig.GetAll(1).ConvertAll(x => new ExportFile { Display = x.File, ClassName = x.Type })
                                   .Union(dXFRepo.GetAll(false).ConvertAll(x=> new ExportFile{Display = x, ClassName="dxf"}))
            };

            return View(evm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
