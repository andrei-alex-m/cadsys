using System;
using System.IO;
using System.Threading.Tasks;

namespace Caly.Common
{
    public static class IOExtensions
    {
        public static void ClearDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }

        public static async Task Store(MemoryStream stream, string filename, bool closeMemStream = false )
        {
            stream.Seek(0, SeekOrigin.Begin);

            using (var fs = File.Create(filename))
            {
                var data = stream.ToArray();
                await stream.WriteAsync(data, 0, data.Length);
            }

            if (closeMemStream)
            {
                stream.Close();
            }
        }
    }
}
