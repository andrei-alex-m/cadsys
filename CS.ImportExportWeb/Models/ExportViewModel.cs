using System;
using System.Collections.Generic;

namespace CS.ImportExportWeb.Models
{
    public class ExportViewModel
    {
        public List<ExportFile> Files
        {
            get;
            set;
        }

        public ExportViewModel()
        {
            Files = new List<ExportFile>();
        }
    }

    public class ExportFile
    {
        public string Display
        {
            get;
            set;
        }
        public string ClassName
        {
            get;
            set;
        }
    }
}
