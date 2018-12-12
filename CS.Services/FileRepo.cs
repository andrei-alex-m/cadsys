using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caly.Common;

namespace CS.Services.Interfaces
{
    public class FileRepo : IFileRepo
    {
        private readonly string dirPath;
        private readonly string extension;
        public FileRepo(string contentRoot, string directory, string _extension)
        {
            extension = _extension;
            dirPath = contentRoot + Path.DirectorySeparatorChar.ToString() + directory;
            Directory.CreateDirectory(dirPath);
        }

        public string DirPath => dirPath;

        public void ClearAll()
        {
            IOExtensions.ClearDirectory(dirPath);
        }

        public List<string> GetAll(bool fullPath = false)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);

            return fullPath
                ? di.EnumerateFiles(extension).Select(x => x.FullName).ToList()
                : di.EnumerateFiles(extension).Select(x => x.Name).ToList();
        }

        public string GetFullPath(string fileName)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);

            return di.EnumerateFiles()
                     .FirstOrDefault(x => string.Equals(x.Name, fileName, StringComparison.InvariantCultureIgnoreCase))?.FullName;
        }

        public async Task Store(MemoryStream stream, string filename)
        {
            await IOExtensions.Store(stream, dirPath + Path.DirectorySeparatorChar.ToString() + filename);
        }



    }
}
