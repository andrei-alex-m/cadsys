using System.Collections.Generic;
using System.Threading.Tasks;
using Caly.Common;
using CS.EF;

namespace CS.CadGen
{
    public interface IExporter
    {
        Task<string[]> Export(int indexImobil, IEnumerable<Point> coords, double suprafata, string nrCadGeneral, string sector, string nrCadastral);
    }
}