using System;
using netDxf;
using netDxf.Blocks;
using netDxf.Collections;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Objects;
using netDxf.Tables;
using netDxf.Units;
namespace CS.DXF
{
    public class Class1
    {
        public Class1()
        {
            DxfDocument doc = new DxfDocument();
            var poly = new Polyline();
            poly.Vertexes.Add(new PolylineVertex(0, 0, 0));
            poly.Vertexes.Add(new PolylineVertex(10, 10, 0));
            poly.Vertexes.Add(new PolylineVertex(10, 20, 0));
            poly.Vertexes.Add(new PolylineVertex(20, 0, 0));
            poly.IsClosed = true;
            var xd = new XDataRecord(XDataCode.String, "kroko");
            var x = new XData(new ApplicationRegistry("mimi"));
            x.XDataRecord.Add(xd);
            poly.XData.Add(x);
            doc.AddEntity(poly);
            doc.Save("abc.dxf");
        }

    }
}
