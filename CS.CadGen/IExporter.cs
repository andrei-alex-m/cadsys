using System.Collections.Generic;
using Caly.Common;

namespace CS.CadGen
{
    public interface IExporter
    {
        string[] Export(int indexImobil, IEnumerable<Point> coords, double suprafata, string nrCadGeneral, string sector, string nrCadastral);
    }
}