using System;
using System.Collections.Generic;

namespace CS.ImportExportWeb.Models
{
    public class ProcessViewModel
    {
        public IEnumerable<string> ExcelFiles { get; set; } = new List<string>();

        public IEnumerable<string> DXFFiles { get; set; } = new List<string>();

        public bool ShowValidation { get; set; } = true;

        public bool ShowProcess { get; set; } = true;

    }
}
