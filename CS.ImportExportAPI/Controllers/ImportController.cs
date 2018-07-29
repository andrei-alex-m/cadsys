using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS.Excel;

namespace CS.ImportExportAPI.Controllers
{
    [Route("api/import")]
    public class ImportController:Controller
    {
        public ImportController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> ReadFileHeaders(IFormFile file)
        {
            if (file != null)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var x = await Importer.Persoane(stream, new ImportConfig());
                    return Ok(x); 
                }

            }

            return BadRequest("File required");
        }
    }
}
