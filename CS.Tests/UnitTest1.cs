using System;
using System.Collections.Generic;
using Xunit;
using CS.DXF;
using CS.Templating;
using Caly.Common;
using Caly.Dropbox;

namespace CS.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void DXFTest()
        {
            var DXF = new Class1();
        }

        [Fact]
        public void TestXml()
        {
            var x = new XmlTeste();
        }

        [Fact]
        public void MatcherTest()
        {
            var matcher = new Matcher();
            var address = matcher.Root.AddChild(new Classification() { Name = "Address", Order = 1 });
            var strType = address.AddChild(new Classification() { Name = "StreetType", Order = 2 });
            var str = strType.AddChild(new Classification() { Name = "Strada", Order = 3 });
            str.AddChild("Str,Strada,Strd");
            var blvd = strType.AddChild(new Classification() { Name = "Bulevard", Order = 3 });
            blvd.AddChild("Blvd,Bulevard,Bulevardul,Blv");

            var res = matcher.Match(new List<Classification>() { new Classification() { Name = "root", Order = 0 }/*, new Classification() { Name = "Address", Order = 1 }*/ }, "Blv", new MatchMaker());

        }

        public class MatchMaker : IMatchProcessor
        {
            private char[] splitters = { ',', ';' };

            public bool Process(params object[] prm)
            {
                var find = prm[0].ToString();
                var leaf = prm[1].ToString();
                return leaf.Match(find, splitters);
            }
        }
        [Fact]
        public void DropboxTest()
        {
            var db = new Caly.Dropbox.DropBoxBase();
            db.List("/CadGen");
        }
    }
}
