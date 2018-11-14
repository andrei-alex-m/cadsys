using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caly.Common;

namespace CS.Services.Interfaces
{
    public class DXFRepo : IDXFRepo
    {
        private readonly string directoryName = "DXF";
        private readonly string dirPath;
        public DXFRepo(string contentRoot)
        {
            dirPath = contentRoot + Path.DirectorySeparatorChar.ToString() + directoryName;
            Directory.CreateDirectory(dirPath);
        }

        public void ClearAll()
        {
            IOExtensions.ClearDirectory(dirPath);
        }

        public List<string> GetFileNames(bool fullPath=false)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);

            return fullPath
                ? di.EnumerateFiles("*.dxf").Select(x => x.FullName).ToList()
                : di.EnumerateFiles("*.dxf").Select(x => x.Name).ToList();
        }

        public string GetFullPath(string fileName)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);

            return di.EnumerateFiles()
                     .FirstOrDefault(x => string.Equals(x.Name, fileName, StringComparison.InvariantCultureIgnoreCase))?.FullName;
        }

        public async Task Store(MemoryStream stream, string filename)
        {
            await IOExtensions.Store(stream, filename);
        }

    }
}
