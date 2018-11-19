using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CS.Services.Interfaces
{
    public interface IDXFRepo
    {
        void ClearAll();
        List<string> GetAll(bool fullPath = false);
        string GetFullPath(string fileName);
        Task Store(MemoryStream stream, string filename);
    }
}