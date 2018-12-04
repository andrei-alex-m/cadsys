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
        public Text nrCadastral { get; set; }
    }

    public static class Exporter
    {

        //needs to go to cadgen exporter with a list of polylines and attributes
        public static MemoryStream Get(string fileName, IExporter cadGenExporter)
        {
            bool isBinary;
            var ver = DxfDocument.CheckDxfFileVersion(fileName, out isBinary);
            DxfDocument doc  = DxfDocument.Load(fileName);
            var docSector = doc.Texts.FirstOrDefault(x => string.Equals(x.Layer.Name, "Sector", StringComparison.InvariantCultureIgnoreCase));

            var polys = GetPolys(doc);


            //Parallel.ForEach(polys, p =>
            foreach (var p in polys)
            {
                p.poly.XData.Clear();
                var points = p.poly.ToPoints();
                var area = VectorExtensions.Area(points);

                //CF - INDEX
                int index;
                if (int.TryParse(p.index.Value.Trim(), out index))
                {
                    var cg = cadGenExporter.Export(index, points, area, p.nrCadGeneral?.Value, docSector?.Value, p.nrCadastral?.Value);

                    if (cg.Length > 0)
                    {
                        var xD = new XData(new ApplicationRegistry("TOPO"));
                        xD.XDataRecord.Add(new XDataRecord(XDataCode.ControlString, "{"));
                        foreach (var line in cg)
                        {
                            var xDR = new XDataRecord(XDataCode.String, line);

                            xD.XDataRecord.Add(xDR);
                        }
                        xD.XDataRecord.Add(new XDataRecord(XDataCode.ControlString, "}"));
                        p.poly.XData.Add(xD);
                    }
                }
            }//);

            var stream = new MemoryStream();
            doc.Save(stream);
            stream.Position = 0;
            return stream;
        }

        static IEnumerable<match> GetPolys(DxfDocument doc)
        {
            var matches = new ConcurrentBag<match>();

            var docPolys = doc.LwPolylines.Where(x => string.Equals(x.Layer.Name, "TERENURI", StringComparison.InvariantCultureIgnoreCase));
            var docIndexes = doc.Texts.Where(x => string.Equals(x.Layer.Name, "Index", StringComparison.InvariantCultureIgnoreCase));
            var docNrCadGeneral = doc.Texts.Where(x => string.Equals(x.Layer.Name, "NrCadGeneral", StringComparison.InvariantCultureIgnoreCase));
            var docNrCadastral = doc.Texts.Where(x => string.Equals(x.Layer.Name, "NrCadastral", StringComparison.InvariantCultureIgnoreCase));

            Parallel.ForEach(docPolys, x =>
            {
                match currentMatch = new match()
                {
                    poly = x
                };

                foreach (var i in docIndexes.Except(matches.Select(y => y.index)))
                {
                    if (IsPointInPolygon(x.Vertexes, i.Position))
                    {
                        currentMatch.index = i;
                        break;
                    }
                }

                foreach (var n in docNrCadGeneral.Except(matches.Select(y => y.nrCadGeneral)))
                {
                    if (IsPointInPolygon(x.Vertexes, n.Position))
                    {
                        currentMatch.nrCadGeneral = n;
                        break;
                    }
                }

                foreach (var n in docNrCadastral.Except(matches.Select(y => y.nrCadastral)))
                {
                    if (IsPointInPolygon(x.Vertexes, n.Position))
                    {
                        currentMatch.nrCadastral = n;
                        break;
                    }
                }

                if (currentMatch.index != null)
                {
                    matches.Add(currentMatch);
                }

            });

            return matches;
        }

        static bool IsPointInPolygon(List<LwPolylineVertex> polygon, Vector3 testPoint)
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

            var newP = points.ToList();
            newP.Add(points[0]);

            return Math.Abs(newP.Take(newP.Count() - 1)
   .Select((p, i) => (newP[i + 1].X - p.X) * (newP[i + 1].Y + p.Y))
   .Sum() / 2);

        }
    }
}
