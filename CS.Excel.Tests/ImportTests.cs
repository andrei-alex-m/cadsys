using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS.Excel.Tests
{
    [TestClass]
    public class ImportTests
    {
        [TestMethod]
        public void ImportTest()
        {
            MemoryStream ms = new MemoryStream();
            using (FileStream file = new FileStream("/Users/andrei/downloads/cad. sistem/Proprietari.xls", FileMode.Open, FileAccess.Read))
                file.CopyTo(ms);
            var results = Importer.Persoane(ms, new ImportConfig()).Result;

        }
    }
}
