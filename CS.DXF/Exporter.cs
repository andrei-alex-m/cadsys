using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CS.Data.DTO.DXF;
using netDxf;
using netDxf.Entities;
using CS.CadGen;
using Caly.Common;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using netDxf.Tables;

namespace CS.DXF
{
    struct match
    {
        public LwPolyline poly { get; set; }
        public Text index { get; set; }
        public Text nrCadGeneral { get; set; }
    }

    public class Exporter
    {
        IExporter cadGenExporter;

        public Exporter(IExporter _cadGenExporter)
        {
            cadGenExporter = _cadGenExporter;
        }

        //needs to go to cadgen exporter with a list of polylines and attributes
        public MemoryStream Get(string fileName)
        {
            DxfDocument doc = DxfDocument.Load(fileName);
            var docSector = doc.Texts.FirstOrDefault(x => string.Equals(x.Layer.Name, "Index", StringComparison.InvariantCultureIgnoreCase));

            Parallel.ForEach(GetPolys(doc), p =>
            {
                var points = p.poly.ToPoints();
                var area = VectorExtensions.Area(points);
                var cg = cadGenExporter.Export(int.Parse(p.index.Value), points, area, p.nrCadGeneral?.Value, docSector?.Value);

                if (cg.Length > 0)
                {
                    var xD = new XData(new ApplicationRegistry("TOPO"));

                    foreach (var line in cg)
                    {
                        var xDR = new XDataRecord(XDataCode.String, line);
                        xD.XDataRecord.Add(xDR);
                    }
                    p.poly.XData.Add(xD);

                }
            });

            var stream = new MemoryStream();
            doc.Save(stream);
            stream.Position = 0;
            return stream;
        }

         IEnumerable<match> GetPolys(DxfDocument doc)
        {
            var matches = new ConcurrentBag<match>();

            var docPolys = doc.LwPolylines.Where(x => string.Equals(x.Layer.Name, "TERENURI", StringComparison.InvariantCultureIgnoreCase));
            var docIndexes = doc.Texts.Where(x => string.Equals(x.Layer.Name, "Index", StringComparison.InvariantCultureIgnoreCase));
            var docNrCadGeneral = doc.Texts.Where(x => string.Equals(x.Layer.Name, "NrCadGeneral", StringComparison.InvariantCultureIgnoreCase));

            Parallel.ForEach(docPolys, x =>
            {
                match currentMatch = new match()
                {
                    poly = x
                };

                foreach (var i in docIndexes.Except(matches.Select(y => y.index)))
                {
                    currentMatch.index = i;
                    break;
                }

                foreach (var n in docNrCadGeneral.Except(matches.Select(y => y.nrCadGeneral)))
                {
                    currentMatch.nrCadGeneral = n;
                    break;
                }

                if (currentMatch.index != null)
                {
                    matches.Add(currentMatch);
                }

            });

            return matches;
        }

        private static bool IsPointInPolygon(List<LwPolylineVertex> polygon, Vector3 testPoint)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Position.Y < testPoint.Y && polygon[j].Position.Y >= testPoint.Y || polygon[j].Position.Y < testPoint.Y && polygon[i].Position.Y >= testPoint.Y)
                {
                    if (polygon[i].Position.X + (testPoint.Y - polygon[i].Position.Y) / (polygon[j].Position.Y - polygon[i].Position.Y) * (polygon[j].Position.X - polygon[i].Position.X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

    }

    public static class VectorExtensions
    {
        public static Caly.Common.Point ToPoint(this Vector2 vector)
        {
            return new Caly.Common.Point()
            {
                X = vector.X,
                Y = vector.Y
            };
        }

        public static Caly.Common.Point ToPoint(this Vector3 vector)
        {
            return new Caly.Common.Point()
            {
                X = vector.X,
                Y = vector.Y
            };
        }

        public static Caly.Common.Point[] ToPoints(this LwPolyline lw)
        {
            var result = lw.Vertexes.Select(x => x.Position.ToPoint());

            while (result.First().Equals(result.Last()))
            {
                result = result.SkipLast(1);
            }

            return result.ToArray();
        }

        public static double Area(Caly.Common.Point[] points)
        {
            if (points.Length < 3)
            {
                return 0;
            }

            var newP = points;
            newP.Append(newP[0]);

            return Math.Abs(newP.Take(newP.Length - 1)
   .Select((p, i) => (newP[i + 1].X - p.X) * (newP[i + 1].Y + p.Y))
   .Sum() / 2);

        }
    }
}
