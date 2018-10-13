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

        public HomeController(IExcelConfigurationRepo excelConfigurator)
        {
            excelConfig = excelConfigurator;
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
                Files = excelConfig.GetAll(1).ConvertAll(x=> new ExportFile { Display = x.File, ClassName=x.Type })
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
